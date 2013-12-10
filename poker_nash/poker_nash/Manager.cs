using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using common;
using input;
using bot;
using output;

namespace poker_nash
{
    public class Manager
    {
        private IInput input;
        private IOutput output;
        private IBot bot;

        public Manager(IInput input, IOutput output, IBot bot)
        {
            this.input = input;
            this.output = output;
            this.bot = bot;
        }

        public void Run()
        {
            var input = this.input.GetState();

            var activity = this.bot.Process(input);

            this.output.Act(activity);
        }
    }
}
