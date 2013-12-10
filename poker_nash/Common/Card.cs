using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace common
{
    public enum Suit
    {
        D = 1, //Diamonds
        H,     //Hearts
        S,     //Spades
        C,     //Clubs
    }

    public enum Value
    {
        _2 = 2,
        _3,
        _4,
        _5,
        _6,
        _7,
        _8,
        _9,
        _T,
        _J,
        _Q,
        _K,
        _A,
    }

    public class Card
    {
        public string Hash { get; set; }
        public Suit Suit { get; set; }
        public Value Value { get; set; }

        public override string ToString()
        {
            switch (this.Value)
            {
                case Value._2:
                    return "2";
                case Value._3:
                    return "3";
                case Value._4:
                    return "4";
                case Value._5:
                    return "5";
                case Value._6:
                    return "6";
                case Value._7:
                    return "7";
                case Value._8:
                    return "8";
                case Value._9:
                    return "9";
                case Value._T:
                    return "T";
                case Value._J:
                    return "J";
                case Value._Q:
                    return "Q";
                case Value._K:
                    return "K";
                case Value._A:
                    return "A";
                default:
                    return "";
            }
        }
    }
}
