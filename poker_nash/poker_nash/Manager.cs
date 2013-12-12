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
        private MainForm form;
        public event EventHandler step;
        int counter = 0;

        public Manager(IInput input, IOutput output, IBot bot, MainForm form)
        {
            this.form = form;
            this.input = input;
            this.output = output;
            this.bot = bot;
        } 

        private void Step(Object state)
        {
            int asdsad;
            //this.form.ShowText(counter.ToString());
            if (this.input.Ready())
            {
                counter++;
                if (counter == 2)
                {
                    asdsad = 23;
                }

                var input = this.input.GetState();

                var activity = this.bot.Process(input);

                this.output.Act(activity);
            }
        }

        public void Run()
        {
            this.timer = new Timer(this.Step, null, 10, 1000); 
        }
    }
}
