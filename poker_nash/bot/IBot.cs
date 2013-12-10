using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using common;

namespace bot
{
    public interface IBot
    {
        Activity Process(State state);
    }
}
