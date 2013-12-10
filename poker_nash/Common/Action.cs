using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common
{
    public enum Actions
    {
        Fold = 1,
        Check,
        Raise,
    }

    public class Action
    {
        public Actions Type { get; set; }
        public int Bet { get; set; }
    }
}
