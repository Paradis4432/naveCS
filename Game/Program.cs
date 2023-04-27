using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Threading;
using System.Timers;

namespace Game
{
    /*
     * TODO:
     * agujero negro
     * agujero de gusano
     * retroseso al disprar
     * cooldown para disparar
     * spawn random de met
     */
    public class Program
    {
        static Ship ship;
        static Cuerpo CS; // cuerpo de ship
        static private GameStage stage;
        static private int secsCounter = 0;
        static List<Meteors> mets = new List<Meteors>();
        static public List<Bullet> bulletsShoot = new List<Bullet>();
        static private int points = 0;

        static private System.Threading.Timer timer;
        static private System.Threading.Timer checkColls;

        static void Main(string[] args)
        {
            Engine.Initialize();
            stage = GameStage.Menu;

            //ship = new Ship(new Vector2(375, 275));
            ship = Ship.GetInstance();

            timer = new System.Threading.Timer(OnTimedEvent, null, 0, 1000);
            checkColls = new System.Threading.Timer(CheckColls, null, 0, 100);

           

            while (true)
            {
                switch (stage) {
                    case GameStage.Menu:
                        Engine.Clear();
                        Engine.Draw(Engine.GetTexture("menu.png"), 0, 0, 1, 1, 0, 0, 0);
                        Engine.Show();
    
                        if (!(Engine.GetKey(Keys.E))) continue;
                        stage = GameStage.Gameplay;
                        ship.Reset();
                        mets = new List<Meteors>();
                        secsCounter = 0;
                        break;
                    case GameStage.Gameplay:

                        if (Engine.GetKeyDown(Keys.R) && ship.exploded)
                        {
                            stage = GameStage.Menu;

                            continue;
                        }

                        if (secsCounter >= 30 && !ship.exploded) {
                            stage = GameStage.Win;
                            break;
                        }
                        Engine.Debug(mets.Count);

                        CS = ship.Nave;
                        Update();
                        Draw();
                        
                        break;
                    case GameStage.Win:
                        Engine.Clear();

                        Engine.Debug(points);
                        Engine.Draw(Engine.GetTexture("win.png"), 0, 0);

                        Engine.Show();

                        if (Engine.GetKey(Keys.R)) stage = GameStage.Menu;


                        break;
                        
                }

            }
        }

        private static void OnTimedEvent(Object stateInfo) {
            if (stage != GameStage.Gameplay) return;

            mets.Add(new Meteors(CS.pos, new Vector2(50, 1)));
            secsCounter++;
        }

        private static void CheckColls(Object stateInfo) {
            // update every 100 mils
            foreach (var met in mets.ToList()) {
                // if any met is colliding with ship.pos
                //Vector2 shipRealPos = new Vector2(0,0);
                //shipRealPos.x = CS.pos.x + (ship.imgW / 2);
                //shipRealPos.y = CS.pos.y + (ship.imgH / 2);

                if (!met.met.alive) mets.Remove(met);

                foreach (var bull in bulletsShoot.ToList()) {

                    if (!bull.Bala.alive) bulletsShoot.Remove(bull);


                    if (Vector2.Colliding(met.met.pos, bull.Bala.pos, met.met.rad - 4, bull.Bala.rad - 3)) {
                        bulletsShoot.Remove(bull);
                        mets.Remove(met);
                        points++;
                    }


                }

                if (Vector2.Colliding(met.met.pos, CS.pos, met.met.rad - 4, CS.rad - 6)) {
                    //stage = GameStage.Lost;
                    ship.exploded = true;
                }
            }
        }

        //static Vector2 tempMetLostPos;

        static void Update() {
            ship.Update();

            foreach (var met in mets.ToList()) met.Update();
            foreach (var bull in bulletsShoot.ToList()) bull.Update();

            //Engine.Debug("---");
        }

        static void Draw() {
            Engine.Clear();

            Engine.Draw(Engine.GetTexture("background.png"), 0, 0);

            ship.Draw();
            foreach (var met in mets.ToList()) {
                met.Draw();
            }

            Engine.Show();
        }

        public static void SetStage(GameStage s) {
            stage = s;
        }
    }
}