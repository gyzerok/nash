using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common
{
    public enum Street
    {
        Preflop = 1,
        Flop,
        Turn,
        River,
    }

    public class Board
    {
        public int Dealer { get; set; }
        public List<Card> Cards { get; set; }
        public List<Player> Players { get; set; }
        public int Bank { get; set; }
        public Street Street 
        { 
            get
            {
                switch (this.Cards.Count)
                {
                    case 0:
                        return Street.Preflop;
                    case 3:
                        return Street.Flop;
                    case 4:
                        return Street.Turn;
                    default:
                        return Street.River;
                }
            }
            set;
        }
    }
}
