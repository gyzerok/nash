using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common
{
    public enum Position
    {
        Blind = 1,
        Early,
        Middle,
        Late,
    }

    public class Player
    {
        public int Bank { get; set; }
        public Activity Activity { get; set; }
    }
}
