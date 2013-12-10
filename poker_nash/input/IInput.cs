using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using common;

namespace input
{
    public interface IInput
    {
        State GetState();
        bool Ready();
    }
}
