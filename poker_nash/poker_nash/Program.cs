using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using common;
using input;
using output;
using bot;

namespace poker_nash
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());

            var input = new Capture();
            var output = new Emulate();
            var bot = new BSSBot();

            var manager = new Manager(input, output, bot);
            manager.Run();
        }
    }
}
