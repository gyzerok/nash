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

        public static Card FromString(string str)
        {
            var card = new Card();

            switch (str[0])
            {
                case '2':
                    card.Value = Value._2;
                    break;
                case '3':
                    card.Value = Value._3;
                    break;
                case '4':
                    card.Value = Value._4;
                    break;
                case '5':
                    card.Value = Value._5;
                    break;
                case '6':
                    card.Value = Value._6;
                    break;
                case '7':
                    card.Value = Value._7;
                    break;
                case '8':
                    card.Value = Value._8;
                    break;
                case '9':
                    card.Value = Value._9;
                    break;
                case 'T':
                    card.Value = Value._T;
                    break;
                case 'J':
                    card.Value = Value._J;
                    break;
                case 'Q':
                    card.Value = Value._Q;
                    break;
                case 'K':
                    card.Value = Value._K;
                    break;
                default:
                    card.Value = Value._A;
                    break;
            }

            switch (str[1])
            {
                case 's':
                    card.Suit = Suit.S;
                    break;
                case 'c':
                    card.Suit = Suit.C;
                    break;
                case 'd':
                    card.Suit = Suit.D;
                    break;
                default:
                    card.Suit = Suit.H;
                    break;
            }

            return card;
        }
    }
}
