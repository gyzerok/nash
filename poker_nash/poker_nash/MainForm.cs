using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using common;
using input;
using output;
using bot;


namespace poker_nash
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            
        }

        private void Space(object sender, KeyEventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void ShowText(string text)
        {
            this.label1.Text = text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var input = new Capture();
            var output = new Emulate();
            var bot = new BSSBot();

            var manager = new Manager(input, output, bot, this);
            manager.Run();
        }
    }
}
