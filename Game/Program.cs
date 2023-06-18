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

        // public static GameManager gameManager = GameManager.GetGameManager();

        public static GameManager gameManager;
        public static bool debug = false;

        public static GameManager GetGameManager()
        {
            if (debug) Engine.Debug("test");
            if (gameManager == null) gameManager = new GameManager();
            if (debug) Console.WriteLine(gameManager);
            if (debug) Engine.Debug(gameManager == null);

            return gameManager;
        }

        static void Main(string[] args)
        {
            Engine.Initialize();

            while (!GetGameManager().gameover)
            {
                switch (GetGameManager().getStage())
                {
                    case GameStage.Menu:
                        GetGameManager().updateMenu();
                        if (!(Engine.GetKey(Keys.E))) continue;
                        GetGameManager().resetValues();
                        break;

                    case GameStage.Gameplay:
                        if (Engine.GetKeyDown(Keys.R) && GetGameManager().getShip().exploded)
                        {
                            //stage = GameStage.Menu;
                            GetGameManager().setStage(GameStage.Menu);

                            continue;
                        }
                        GetGameManager().updateGameplay();

                        break;
                    case GameStage.Win:
                        GetGameManager().updateWin();


                        break;

                }
            }
        }
    }
}