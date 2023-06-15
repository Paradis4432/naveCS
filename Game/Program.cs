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

        private static GameManager gameManager = new GameManager();
        static void Main(string[] args)
        {
            Engine.Initialize();
            gameManager = gameManager.getInstance();

            while (!gameManager.gameover)
            {
                switch (gameManager.getStage()) {
                    case GameStage.Menu:
                        gameManager.updateMenu();
                        if (!(Engine.GetKey(Keys.E))) continue;
                        gameManager.resetValues();
                        break;
                        
                    case GameStage.Gameplay:
                        if (Engine.GetKeyDown(Keys.R) && gameManager.getShip().exploded)
                        {
                            //stage = GameStage.Menu;
                            gameManager.setStage(GameStage.Menu);

                            continue;
                        }
                        gameManager.updateGameplay();
                        
                        break;
                    case GameStage.Win:
                        gameManager.updateWin();


                        break;
                        
                }

            }
        }
    }
}