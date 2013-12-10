using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Security.Cryptography;

namespace ImageParser
{
    public partial class Form1 : Form
    {
        AnchorPlacer aPlacer;
        SaveFileDialog dialog = new SaveFileDialog();
        Dictionary<string, string> cards = new Dictionary<string, string>();
        InputBox inputBox;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            aPlacer = new AnchorPlacer(this);
            cards = Read("saved");
            Timer t = new Timer();
            t.Interval = 20;
            t.Tick += new EventHandler(t_Tick);
            t.Start();
            Timer playerStateTimer = new Timer();
            playerStateTimer.Interval = 2000;
            playerStateTimer.Tick += playerStateTimer_Tick;
            playerStateTimer.Start();
        }

        void playerStateTimer_Tick(object sender, EventArgs e)
        {
            Bitmap temp;
            label3.Text = "";
            temp = aPlacer.PrintScreen(aPlacer.betRect1);
            pictureBox8.Image = DetectBet(temp);
            temp = aPlacer.PrintScreen(aPlacer.rect);
            string tempString = "";
            string hash = GetHash(temp);
            if (cards.ContainsKey(hash))
            {
                tempString += cards[hash] + ' ';
                this.pictureBox1.Image = temp;
            }
            temp = aPlacer.PrintScreen(aPlacer.rect2);
            hash = GetHash(temp);
            if (cards.ContainsKey(hash))
            {
                tempString += cards[hash] + ' ';
                this.pictureBox2.Image = temp;
            }
            temp = aPlacer.PrintScreen(aPlacer.flopRect1);
            hash = GetHash(temp);
            if (cards.ContainsKey(hash))
            {
                tempString += cards[hash] + ' ';
                this.pictureBox3.Image = temp;
            }
            temp = aPlacer.PrintScreen(aPlacer.flopRect2);
            hash = GetHash(temp);
            if (cards.ContainsKey(hash))
            {
                tempString += cards[hash] + ' ';
                this.pictureBox4.Image = temp;
            }
            temp = aPlacer.PrintScreen(aPlacer.flopRect3);
            hash = GetHash(temp);
            if (cards.ContainsKey(hash))
            {
                tempString += cards[hash] + ' ';
                this.pictureBox5.Image = temp;
            }
            temp = aPlacer.PrintScreen(aPlacer.turnRect);
            hash = GetHash(temp);
            if (cards.ContainsKey(hash))
            {
                tempString += cards[hash] + ' ';
                this.pictureBox6.Image = temp;
            }
            temp = aPlacer.PrintScreen(aPlacer.riverRect);
            hash = GetHash(temp);
            if (cards.ContainsKey(hash))
            {
                tempString += cards[hash] + ' ';
                this.pictureBox7.Image = temp;
            }
            label3.Text = tempString;

        }

        public Bitmap DetectBet(Bitmap image)
        {
            int minX, maxX, minY, maxY;
            minX = image.Width;
            maxX = 0;
            minY = image.Height;
            maxY = 0;
            for (int i = 0; i < image.Height; i++)
            {
                for (int j = 0; j < image.Width; j++)
                {
                    if (image.GetPixel(j,i) == Color.FromArgb(255, 255, 246, 207))
                    {
                        minX = Math.Min(minX, j);
                        maxX = Math.Max(maxX, j);
                        minY = Math.Min(minY, i);
                        maxY = Math.Max(maxY, i);
                        //image.SetPixel(i, j, Color.Black);
                    }
                }
            }
            if (minX < maxX)
            {
                minX--;
                minY--;
                maxY+=2;
                maxX+=2;
                return image.Clone(new Rectangle(minX,minY,maxX-minX, maxY-minY), PixelFormat.DontCare);
            }
            else
                return image;
        }

        void t_Tick(object sender, EventArgs e)
        {
            label1.Text = MousePosition.X.ToString();
            label2.Text = MousePosition.Y.ToString();
        }


        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            Bitmap temp;
            if (e.KeyCode == Keys.Space)
            {
                temp = aPlacer.PrintScreen(aPlacer.rect);
                this.pictureBox1.Image = temp;
                this.CardRecognition(temp);
                temp = aPlacer.PrintScreen(aPlacer.rect2);
                this.pictureBox2.Image = aPlacer.PrintScreen(aPlacer.rect2);
                this.CardRecognition(temp);
                temp = aPlacer.PrintScreen(aPlacer.flopRect1);
                this.pictureBox3.Image = aPlacer.PrintScreen(aPlacer.flopRect1);
                this.CardRecognition(temp);
                temp = aPlacer.PrintScreen(aPlacer.flopRect2);
                this.pictureBox4.Image = aPlacer.PrintScreen(aPlacer.flopRect2);
                this.CardRecognition(temp);
                temp = aPlacer.PrintScreen(aPlacer.flopRect3);
                this.pictureBox5.Image = aPlacer.PrintScreen(aPlacer.flopRect3);
                this.CardRecognition(temp);
                if (cards.Count == 52)
                {
                    button1.Enabled = true;
                }
            }
        }

        public void CardRecognition(Bitmap card, string name)
        {
            string hash = GetHash(card);
            this.cards.Add(hash, name);
            card.Save(name+".bmp");
        }

        public void CardRecognition(Bitmap card)
        {
            string hash = GetHash(card); 
            if ((!cards.ContainsKey(hash))&&(card.GetPixel(0,0) == Color.FromArgb(255,255,255,255)))
            {
                inputBox = new InputBox(this, card);
            }
            label3.Text = cards.Count.ToString();
        }

        public static void Write(Dictionary<string, string> dictionary, string file)
        {
            using (FileStream fs = File.OpenWrite(file))
            using (BinaryWriter writer = new BinaryWriter(fs))
            {
                // Put count.
                writer.Write(dictionary.Count);
                // Write pairs.
                foreach (var pair in dictionary)
                {
                    writer.Write(pair.Key);
                    writer.Write(pair.Value);
                }
            }
        }

        public static Dictionary<string, string> Read(string file)
        {
            var result = new Dictionary<string, string>();
            using (FileStream fs = File.OpenRead(file))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                // Get count.
                int count = reader.ReadInt32();
                // Read in all pairs.
                for (int i = 0; i < count; i++)
                {
                    string key = reader.ReadString();
                    string value = reader.ReadString();
                    result.Add(key, value);
                }
            }
            return result;
        }

        public static Dictionary<string, string> LoadData()
        {
            var result = new Dictionary<string, string>();
            string[] filePaths = Directory.GetFiles(@"C:\Users\Джордж\Documents\Visual Studio 2012\Projects\ImageParser\ImageParser\bin\Debug\cards");
            Bitmap temp;
            foreach (string file in filePaths)
            {
               temp = new Bitmap(file);
               result.Add(GetHash(temp), file.Substring(file.Length - 2));
            }
            return result;
        }

        private static string GetHash(Bitmap image)
        {
            // get the bytes from the image
            byte[] bytes = null;
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Bmp); // gif for example
                bytes = ms.ToArray();
            }
            // hash the bytes
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] hash = md5.ComputeHash(bytes);
            string temp = "";
            foreach (byte value in hash)
            {
                temp += value.ToString();
            }
            return temp;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //SAVE
            Write(cards, "saved");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //LOAD
            
        }
    }
}
