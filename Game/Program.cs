using System;
using System.Collections.Generic;
using System.Media;
using System.Threading;
using System.Timers;

namespace Game
{
    /*
     * TODO:
     * singleton para nave
     * balas
     * animaciones
     * UML
     * agujero negro
     * agujero de gusano
     * 
     */
    public class Program
    {
        static Ship ship;
        static Cuerpo CS; // cuerpo de ship
        static private GameStage stage;
        static private int secsCounter = 0;
        static List<Meteors> mets = new List<Meteors>();

        static private System.Threading.Timer timer;


        static void Main(string[] args)
        {
            Engine.Initialize();
            stage = GameStage.Menu;

            ship = new Ship(new Vector2(375, 275));

            timer = new System.Threading.Timer(OnTimedEvent, null, 0, 1000);

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
                        if (secsCounter >= 30) {
                            stage = GameStage.Win;
                            break;
                        }

                        CS = ship.GetNave();
                        Update();
                        Draw();
                        
                        break;
                    case GameStage.Lost:
                        Engine.Clear();

                        //foreach (var met in mets) {
                        //    met.Draw();
                        //}

                        //ship.Draw();

                        //Engine.Draw(Engine.GetTexture("meteorRED.png"), tempMetLostPos.x, tempMetLostPos.y, 1,1,0,12,12);
                        Engine.Draw(Engine.GetTexture("lost.png"), 0, 0);

                        Engine.Show();

                        if (Engine.GetKey(Keys.R)) stage = GameStage.Menu;

                        break;
                    case GameStage.Win:
                        Engine.Clear();

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

        //static Vector2 tempMetLostPos;

        static void Update() {
            ship.Update();
            foreach (var met in mets)
            {
                // if any met is colliding with ship.pos
                //Vector2 shipRealPos = new Vector2(0,0);
                //shipRealPos.x = CS.pos.x + (ship.imgW / 2);
                //shipRealPos.y = CS.pos.y + (ship.imgH / 2);

                if (Vector2.Colliding(met.GetMet().pos, CS.pos, met.GetMet().rad - 4, CS.rad - 6)) {
                    //tempMetLostPos = met.GetMet().pos;  
                    stage = GameStage.Lost;
                }
                met.Update();
            }
            //Engine.Debug("---");
        }

        static void Draw() {
            Engine.Clear();

            //Engine.Draw(Engine.GetTexture("background.png"), 0, 0);

            ship.Draw();
            foreach (var met in mets) {
                met.Draw();
            }

            Engine.Show();
        }

        public static void SetStage(GameStage s) {
            stage = s;
        }
    }
}