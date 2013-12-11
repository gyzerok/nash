using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using common;

namespace common
{
    public class State
    {
        public Board Board { get; set; }
        public Hand Hand { get; set; }
        public State(Board board, Hand hand)
        {
            this.Board = board;
            this.Hand = hand;
        }
    }
}
