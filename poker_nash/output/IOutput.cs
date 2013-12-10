using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using common;

namespace output
{
    public interface IOutput
    {
        void Act(common.Activity action);
    }
}
