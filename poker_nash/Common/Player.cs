using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common
{
    public enum PreflopPosition
    {
        BB = 1,
        SB,
        D,
        CO,
        MP3,
        MP2,
        MP1,
        UTG2,
        UTG1,
    }

    public enum Position
    {
        Early = 1,
        Middle,
        Late,
        Blind,
    }

    public class Player
    {
        public int Bank { get; set; }
        public Activity Activity { get; set; }
    }
}
