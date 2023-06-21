using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{

    public class GameManager
    {

        public Ship ship;
        public Ship getShip() { return ship; }
        // Cuerpo shipCuerpo; // cuerpo de ship // ya no es necesario tener cuerpo, ship hereda de cuerpo
        public GameStage stage;
        public int secsCounter = 0;
        public List<Meteors> meteors = new List<Meteors>();
        // public List<Bullet> bulletsShoot = new List<Bullet>();
        public int points = 0;
        public Boolean gameover = false;

        static public System.Threading.Timer timer;
        static public System.Threading.Timer checkColls;
        static public BulletPool bulletPool = new BulletPool();

        public GameManager()
        {

            ship = Ship.GetInstance();
            timer = new System.Threading.Timer(OnTimedEvent, null, 0, 1000);
            checkColls = new System.Threading.Timer(CheckColls, null, 0, 100);

        }

        public void updateMenu()
        {
            Engine.Clear();
            Engine.Draw(Engine.GetTexture("menu.png"), 0, 0, 1, 1, 0, 0, 0);

            Engine.Show();
        }

        public void updateWin()
        {
            Engine.Clear();

            Engine.Debug(points);
            Engine.Draw(Engine.GetTexture("win.png"), 0, 0);

            Engine.Show();

            if (Engine.GetKey(Keys.R)) stage = GameStage.Menu;
        }
        public void resetValues()
        {
            Console.WriteLine("debug0");
            stage = GameStage.Gameplay;
            Console.WriteLine("debug1");
            ship.Reset();
            Console.WriteLine("debug2");
            meteors = new List<Meteors>();
            Console.WriteLine("debug3");
            secsCounter = 0;
            Console.WriteLine("debug4");
        }

        public void updateGameplay()
        {
            if (Program.debug) Console.WriteLine("debug0.1");

            if (secsCounter >= 30 && !ship.exploded)
            {
                if (Program.debug) Console.WriteLine("debug0.2");

                stage = GameStage.Win;
                this.gameover = true;
            }
            //Engine.Debug(mets.Count);
            if (Program.debug) Console.WriteLine("debug1.1");

            if (Program.debug) Console.WriteLine("debug1.2");
            Update();
            if (Program.debug) Console.WriteLine("debug2");
            Draw();
            if (Program.debug) Console.WriteLine("debug3");

        }

        public void setStage(GameStage newStage)
        {
            this.stage = newStage;
        }

        public GameStage getStage()
        {
            return this.stage;
        }


        private void OnTimedEvent(Object stateInfo)
        {
            if (stage != GameStage.Gameplay) return;

            // meteors.Add(new Meteors(ship.pos, new Vector2(50, 1)));

            // spawn a meteor of random type
            int rand = new Random().Next(3);

            meteors.Add(MeteorFactory.CreateMeteor(ship.transform.getPosition(), new Vector2(50, 1), (MeteorType) rand));
            secsCounter++;
        }

        private void CheckColls(Object stateInfo)
        {
            List<Meteors> copy;
            copy = new List<Meteors>(meteors);
            // update every 100 mils
            foreach (var met in copy)
            {
                // if any met is colliding with ship.transform.getPosition()
                //Vector2 transform.shipRealgetPosition() = new Vector2(0,0);
                //transform.shipRealgetPosition().x = CS.transform.getPosition().x + (ship.imgW / 2);
                //transform.shipRealgetPosition().y = CS.transform.getPosition().y + (ship.imgH / 2);

                if (!met.alive) meteors.Remove(met);

                List<Bullet> bulletsToReturn = new List<Bullet>();
                foreach (var bull in bulletPool.activeBullets.ToList())
                {

                    // if (!bull.alive) bulletsShoot.Remove(bull);
                    if (!bull.alive) bulletsToReturn.Add(bull);


                    if (Vector2.Colliding(met.transform.getPosition(), bull.transform.getPosition(), met.rad * 7 - 4, bull.rad * 7 - 3))
                    {
                        // bulletsShoot.Remove(bull);
                        bulletsToReturn.Add(bull);
                        meteors.Remove(met);
                        points++;
                    }


                }

                if (Vector2.Colliding(met.transform.getPosition(), ship.transform.getPosition(), met.rad - 4, ship.rad - 6))
                {
                    //stage = GameStage.Lost;
                    ship.exploded = true;
                }


                foreach (var bull in bulletsToReturn)
                {
                    bulletPool.Return(bull);
                }
            }


        }

        void Update()
        {
            ship.Update();

            foreach (var met in meteors.ToList()) met.Update();
            foreach (var bull in bulletPool.activeBullets.ToList()) bull.Update();

            //Engine.Debug("---");
        }

        void Draw()
        {
            Engine.Clear();
            if (Program.debug) Console.WriteLine("debug4.1");

            Engine.Draw(Engine.GetTexture("background.png"), 0, 0);
            if (Program.debug) Console.WriteLine("debug4.2");

            if (ship.exploded) Engine.Draw(Engine.GetTexture("lost.png"), 300, 100);

            if (Program.debug) Console.WriteLine("debug4.3");
            ship.Draw();
            if (Program.debug) Console.WriteLine("debug4.4");
            foreach (var met in meteors.ToList())
            {
                met.Draw();
            }
            foreach (var bull in bulletPool.activeBullets.ToList())
            {
                bull.Draw();
            }
            Engine.Show();
        }

    }
}
