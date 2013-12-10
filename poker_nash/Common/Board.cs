using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common
{
    public class Board
    {
        public int Dealer { get; set; }
        public List<Card> Cards { get; set; }
        public List<double> Bets { get; set; }
        public int Bank { get; set; }
    }
}
