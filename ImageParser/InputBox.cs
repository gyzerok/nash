using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageParser
{
    public partial class InputBox : Form
    {
        private Form1 parent;
        private Bitmap card;

        public InputBox(Form1 parent, Bitmap bmp)
        {
            this.parent = parent;
            InitializeComponent();
            this.pictureBox1.Image = bmp;
            card = bmp;
            this.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 2)
            {
                this.parent.CardRecognition(this.card, textBox1.Text);
                this.Close();
            }
        }
    }
}
