﻿using System;
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
        Unknown,
    }

    public class Activity
    {
        private double bet;

        public Activity(Decision decision, double bet = 0)
        {
            this.Type = decision;
            this.Bet = bet;
        }

        public Decision Type { get; private set; }
        public double Bet 
        {
            get
            {
                if (this.Type != Decision.Raise) return -1;
                return this.bet;
            }
            private set
            {
                 this.bet = value;
            }
        }
    }
}
