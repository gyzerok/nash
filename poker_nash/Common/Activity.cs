using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common
{
    public enum Decision
    {
        Fold = 1,
        Check,
        Raise,
        Call,
        Call20,
    }

    public class Activity
    {
        private int bet;

        public Decision Type { get; set; }
        public int Bet 
        {
            get
            {
                if (this.Type != Decision.Raise) return -1;
                return this.bet;
            }
            set
            {
                 this.bet = value;
            }
        }
    }
}
