using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    
    class GameManager {

        public Ship ship;
        Cuerpo shipCuerpo; // cuerpo de ship
        private GameStage stage;
        private int secsCounter = 0;
        private List<Meteors> meteors = new List<Meteors>();
        private List<Bullet> bulletsShoot = new List<Bullet>();
        private int points = 0;
        public Boolean gameover  = false;

        static private System.Threading.Timer timer;
        static private System.Threading.Timer checkColls;

        private GameManager instance; 

        public Ship getShip() { return ship; }

        public GameManager getInstance()
        {
            return this.instance;
        }

        public GameManager()
        {
            this.instance = this;

            this.ship = Ship.GetInstance();
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
            stage = GameStage.Gameplay;
            ship.Reset();
            meteors = new List<Meteors>();
            secsCounter = 0;
        }

        public void updateGameplay()
        {
            if (secsCounter >= 30 && !ship.exploded)
            {
                stage = GameStage.Win;
                this.gameover = true;
            }
            //Engine.Debug(mets.Count);

            shipCuerpo = ship.Nave;
            Update();
            Draw();

        }

        public void setStage(GameStage newStage) {
            this.stage = newStage;
        }

        public GameStage getStage()
        {
            return this.stage;
        }
            

        private void OnTimedEvent(Object stateInfo)
        {
            if (stage != GameStage.Gameplay) return;

            meteors.Add(new Meteors(shipCuerpo.pos, new Vector2(50, 1)));
            secsCounter++;
        }

        private void CheckColls(Object stateInfo)
        {
            // update every 100 mils
            foreach (var met in meteors.ToList())
            {
                // if any met is colliding with ship.pos
                //Vector2 shipRealPos = new Vector2(0,0);
                //shipRealPos.x = CS.pos.x + (ship.imgW / 2);
                //shipRealPos.y = CS.pos.y + (ship.imgH / 2);

                if (!met.met.alive) meteors.Remove(met);

                foreach (var bull in bulletsShoot.ToList())
                {

                    if (!bull.Bala.alive) bulletsShoot.Remove(bull);


                    if (Vector2.Colliding(met.met.pos, bull.Bala.pos, met.met.rad - 4, bull.Bala.rad - 3))
                    {
                        bulletsShoot.Remove(bull);
                        meteors.Remove(met);
                        points++;
                    }


                }

                if (Vector2.Colliding(met.met.pos, shipCuerpo.pos, met.met.rad - 4, shipCuerpo.rad - 6))
                {
                    //stage = GameStage.Lost;
                    ship.exploded = true;
                }
            }
        }

        void Update()
        {
            ship.Update();

            foreach (var met in meteors.ToList()) met.Update();
            foreach (var bull in bulletsShoot.ToList()) bull.Update();

            //Engine.Debug("---");
        }

        void Draw()
        {
            Engine.Clear();

            Engine.Draw(Engine.GetTexture("background.png"), 0, 0);
            if (ship.exploded) Engine.Draw(Engine.GetTexture("lost.png"), 300, 100);

            ship.Draw();
            foreach (var met in meteors.ToList())
            {
                met.Draw();
            }

            Engine.Show();
        }

    }
}
