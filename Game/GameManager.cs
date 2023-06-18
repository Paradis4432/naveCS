﻿using System;
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
        Cuerpo shipCuerpo; // cuerpo de ship
        public GameStage stage;
        public int secsCounter = 0;
        public List<Meteors> meteors = new List<Meteors>();
        public List<Bullet> bulletsShoot = new List<Bullet>();
        public int points = 0;
        public Boolean gameover = false;

        static public System.Threading.Timer timer;
        static public System.Threading.Timer checkColls;

        public static GameManager gameManager = new GameManager();

        public static GameManager GetGameManager()
        {
            Engine.Debug("test");
            Console.WriteLine(gameManager);
            Engine.Debug(gameManager == null);
            return gameManager;
        }

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
            Console.WriteLine("debug0.1");

            if (secsCounter >= 30 && !ship.exploded)
            {
                Console.WriteLine("debug0.2");

                stage = GameStage.Win;
                this.gameover = true;
            }
            //Engine.Debug(mets.Count);
            Console.WriteLine("debug1.1");

            shipCuerpo = ship.Nave;
            Console.WriteLine("debug1.2");
            Update();
            Console.WriteLine("debug2");
            Draw();
            Console.WriteLine("debug3");

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
            Console.WriteLine("debug4.1");

            Engine.Draw(Engine.GetTexture("background.png"), 0, 0);
            Console.WriteLine("debug4.2");

            if (ship.exploded) Engine.Draw(Engine.GetTexture("lost.png"), 300, 100);

            Console.WriteLine("debug4.3");
            ship.Draw();
            Console.WriteLine("debug4.4");
            foreach (var met in meteors.ToList())
            {
                met.Draw();
            }

            Engine.Show();
        }

    }
}
