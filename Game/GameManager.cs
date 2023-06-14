using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    
    class GameManager {
        private GameManager instance; 

        public GameManager getInstance()
        {
            return this.instance;
        }

        public GameManager()
        {
            this.instance = this;
        }





    }
}
