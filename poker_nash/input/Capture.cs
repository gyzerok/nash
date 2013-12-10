using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using common;

namespace input
{
    public class Capture : IInput
    {
        private Bitmap imageState;

        public State GetState()
        {
            return null;
        }

        //Returns TRUE if it is your turn on board
        public bool Ready()
        {
            return true;
        }

        //Method that recognizes cards on board
        private void Cards()
        {
            
        }

        //Method that recognizes bank and bets on table
        private void Bets()
        {
            
        }

        //Method that detects dealer on the table
        private void Dealer()
        {
            Color dealerColor = Color.FromArgb(255, 169, 23, 13);
                
        }

    }
}
