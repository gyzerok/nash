﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common
{
    public class Hand
    {
        public List<Card> Cards;

        public override string ToString()
        {
            string ret = "";

            ret = this.Cards[0].ToString() + this.Cards[1].ToString();
            if (this.Cards[0].Suit == this.Cards[1].Suit)
                ret += "s";
            else
                ret += "o";

            return ret;
        }
    }
}
