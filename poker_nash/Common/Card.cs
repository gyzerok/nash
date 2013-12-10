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
        H, //Hearts
        S, //Spades
        C, //Clubs
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
    }
}
