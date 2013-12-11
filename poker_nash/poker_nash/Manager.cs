using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using common;
using input;
using bot;
using output;
using System.Threading;

namespace poker_nash
{
    public class Manager
    {
        private IInput input;
        private IOutput output;
        private IBot bot;
        private Timer timer;

        public Manager(IInput input, IOutput output, IBot bot)
        {
            this.input = input;
            this.output = output;
            this.bot = bot;
        }

        private void Step(Object state)
        {
            if (this.input.Ready())
            {
                var input = this.input.GetState();

                var activity = this.bot.Process(input);

                this.output.Act(activity);
            }
        }

        public void Run()
        {
            this.timer = new Timer(this.Step, null, 1000, Timeout.Infinite); 
        }
    }
}
