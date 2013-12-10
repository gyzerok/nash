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
        public List<double> Bets { get; set; }
        public int Bank { get; set; }
        public Street Street { get; set; }
    }
}
