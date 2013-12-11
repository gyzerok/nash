using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

using common;

namespace input
{
    public class Capture : IInput
    {
        private Bitmap imageState;
        private int dealerPos;
        private Dictionary<string, string> cards = new Dictionary<string, string>();
        private List<Player> players = new List<Player>();

        private List<Rectangle> betsRects = new List<Rectangle>()
        {
            new Rectangle(614,391,80,20),
            new Rectangle(455,400,80,20),
            new Rectangle(379,316,80,20),
            new Rectangle(402,230,80,20),
            new Rectangle(527,194,80,20),
            new Rectangle(749,190,80,20),
            new Rectangle(831,234,80,20),
            new Rectangle(877,307,80,20),
            new Rectangle(763,395,80,20),
        };

        private List<Rectangle> handsRects = new List<Rectangle>()
        {
            new Rectangle(398,379,30,30),
            new Rectangle(310,286,30,30),
            new Rectangle(406,174,30,30),
            new Rectangle(484,140,30,30),
            new Rectangle(858,138,30,30),
            new Rectangle(937,174,30,30),
            new Rectangle(1029,288,30,30),
            new Rectangle(923,380,30,30),
        };

        private List<Rectangle> dealerRects = new List<Rectangle>()
        {
            new Rectangle(661,425,30,25),
            new Rectangle(475,427,30,25),
            new Rectangle(316,334,30,25),
            new Rectangle(340,212,30,25),
            new Rectangle(522,140,30,25),
            new Rectangle(826,140,30,25),
            new Rectangle(1002,213,30,25),
            new Rectangle(1022,332,30,25),
            new Rectangle(874,428,30,25),
        };

        public State GetState()
        {
            this.imageState = ImageProcessor.Snapshot();
            Dealer();
            Bets();
            return null;
        }

        //Loads data
        //TODO: implement dealer and bets loading
        private void LoadConfig()
        {
            var result = new Dictionary<string, string>();
            using (FileStream fs = File.OpenRead("cards.cfg"))
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
            cards = result;
        }

        //Returns TRUE if it is your turn on board
        public bool Ready()
        {
            return true;
        }

        //Method that recognizes cards on board
        private void Cards()
        {
            
        }

        //Method that recognizes bank and bets on table
        private void Bets()
        {
            Rectangle value;
            Bitmap targetBitmap;
            double recognizedValue;
            for (int i = 0; i < betsRects.Count; i++)
            {
                value = betsRects[i];
                
                targetBitmap = ImageProcessor.Crop(imageState, value);
                targetBitmap = ImageProcessor.DetectBet(targetBitmap);
                if (targetBitmap != null)
                {
                    recognizedValue = DigitOcr.Recognize(targetBitmap);
                    this.players.Add(new Player(new Activity(Decision.Unknown, recognizedValue)));
                }
            }
        }

        //Method that detects dealer on the table
        private void Dealer()
        {
            Rectangle value;
            Bitmap targetBitmap;
            Color dealerColor = Color.FromArgb(255, 169, 23, 13);
            bool flag = false;
            for (int i = 0; i < dealerRects.Count; i++)
            {
                value = dealerRects[i];
                targetBitmap = ImageProcessor.Crop(imageState, value);
                for (int j = 0; j < value.Width; j++)
                {
                    for (int k = 0; k < value.Height; k++)
                    {
                        if (targetBitmap.GetPixel(j, k) == dealerColor)
                        {
                            dealerPos = i;
                            flag = true;
                            break;
                        }
                    }
                    if (flag) break;
                }
                if (flag) break;
            }
        }

        private int NumberOfPlayersAfter(int dealerPos)
        {
            int result = 0;
            Point value;
            Color handColor = Color.FromArgb(255, 247, 219, 219);
            return result;
        }

        //public Rectangle rect = new Rectangle(650, 460, 15, 40);
        //public Rectangle rect2 = new Rectangle(671, 465, 15, 40);
        //public Rectangle flopRect1 = new Rectangle(533, 227, 15, 40);
        //public Rectangle flopRect2 = new Rectangle(598, 227, 15, 40);
        //public Rectangle flopRect3 = new Rectangle(663, 227, 15, 40);
        //public Rectangle turnRect = new Rectangle(728, 227, 15, 40);
        //public Rectangle riverRect = new Rectangle(793, 227, 15, 40);
        //public Rectangle betRect1 = new Rectangle(500, 185, 120,35);

        //public AnchorPlacer(Form1 parent)
        //{
        //    this.Parent = parent;
        //}

        //public Bitmap PrintScreen(Rectangle rect)
        //{
        //    Bitmap bitmap = new Bitmap(
        //        rect.Width, rect.Height);

        //    using (Graphics bmpGraphics = Graphics.FromImage(bitmap))
        //        bmpGraphics.CopyFromScreen(rect.X, rect.Y, 0, 0,
        //            new Size(rect.Width, rect.Height));
        //    return bitmap;
        //}

        //public Form1()
        //{
        //    InitializeComponent();
        //}

        //private void Form1_Load(object sender, EventArgs e)
        //{
        //    aPlacer = new AnchorPlacer(this);
        //    cards = Read("saved");
        //    Timer t = new Timer();
        //    t.Interval = 20;
        //    t.Tick += new EventHandler(t_Tick);
        //    t.Start();
        //    Timer playerStateTimer = new Timer();
        //    playerStateTimer.Interval = 2000;
        //    playerStateTimer.Tick += playerStateTimer_Tick;
        //    playerStateTimer.Start();
        //}

        //void playerStateTimer_Tick(object sender, EventArgs e)
        //{
        //    Bitmap temp;
        //    label3.Text = "";
        //    temp = aPlacer.PrintScreen(aPlacer.betRect1);
        //    pictureBox8.Image = DetectBet(temp);
        //    temp = aPlacer.PrintScreen(aPlacer.rect);
        //    string tempString = "";
        //    string hash = GetHash(temp);
        //    if (cards.ContainsKey(hash))
        //    {
        //        tempString += cards[hash] + ' ';
        //        this.pictureBox1.Image = temp;
        //    }
        //    temp = aPlacer.PrintScreen(aPlacer.rect2);
        //    hash = GetHash(temp);
        //    if (cards.ContainsKey(hash))
        //    {
        //        tempString += cards[hash] + ' ';
        //        this.pictureBox2.Image = temp;
        //    }
        //    temp = aPlacer.PrintScreen(aPlacer.flopRect1);
        //    hash = GetHash(temp);
        //    if (cards.ContainsKey(hash))
        //    {
        //        tempString += cards[hash] + ' ';
        //        this.pictureBox3.Image = temp;
        //    }
        //    temp = aPlacer.PrintScreen(aPlacer.flopRect2);
        //    hash = GetHash(temp);
        //    if (cards.ContainsKey(hash))
        //    {
        //        tempString += cards[hash] + ' ';
        //        this.pictureBox4.Image = temp;
        //    }
        //    temp = aPlacer.PrintScreen(aPlacer.flopRect3);
        //    hash = GetHash(temp);
        //    if (cards.ContainsKey(hash))
        //    {
        //        tempString += cards[hash] + ' ';
        //        this.pictureBox5.Image = temp;
        //    }
        //    temp = aPlacer.PrintScreen(aPlacer.turnRect);
        //    hash = GetHash(temp);
        //    if (cards.ContainsKey(hash))
        //    {
        //        tempString += cards[hash] + ' ';
        //        this.pictureBox6.Image = temp;
        //    }
        //    temp = aPlacer.PrintScreen(aPlacer.riverRect);
        //    hash = GetHash(temp);
        //    if (cards.ContainsKey(hash))
        //    {
        //        tempString += cards[hash] + ' ';
        //        this.pictureBox7.Image = temp;
        //    }
        //    label3.Text = tempString;

        //}

        

        //void t_Tick(object sender, EventArgs e)
        //{
        //    label1.Text = MousePosition.X.ToString();
        //    label2.Text = MousePosition.Y.ToString();
        //}


        //protected override void OnKeyDown(KeyEventArgs e)
        //{
        //    base.OnKeyDown(e);
        //    Bitmap temp;
        //    if (e.KeyCode == Keys.Space)
        //    {
        //        temp = aPlacer.PrintScreen(aPlacer.rect);
        //        this.pictureBox1.Image = temp;
        //        this.CardRecognition(temp);
        //        temp = aPlacer.PrintScreen(aPlacer.rect2);
        //        this.pictureBox2.Image = aPlacer.PrintScreen(aPlacer.rect2);
        //        this.CardRecognition(temp);
        //        temp = aPlacer.PrintScreen(aPlacer.flopRect1);
        //        this.pictureBox3.Image = aPlacer.PrintScreen(aPlacer.flopRect1);
        //        this.CardRecognition(temp);
        //        temp = aPlacer.PrintScreen(aPlacer.flopRect2);
        //        this.pictureBox4.Image = aPlacer.PrintScreen(aPlacer.flopRect2);
        //        this.CardRecognition(temp);
        //        temp = aPlacer.PrintScreen(aPlacer.flopRect3);
        //        this.pictureBox5.Image = aPlacer.PrintScreen(aPlacer.flopRect3);
        //        this.CardRecognition(temp);
        //        if (cards.Count == 52)
        //        {
        //            button1.Enabled = true;
        //        }
        //    }
        //}

        //public void CardRecognition(Bitmap card, string name)
        //{
        //    string hash = GetHash(card);
        //    this.cards.Add(hash, name);
        //    card.Save(name+".bmp");
        //}

        //public void CardRecognition(Bitmap card)
        //{
        //    string hash = GetHash(card); 
        //    if ((!cards.ContainsKey(hash))&&(card.GetPixel(0,0) == Color.FromArgb(255,255,255,255)))
        //    {
        //        inputBox = new InputBox(this, card);
        //    }
        //    label3.Text = cards.Count.ToString();
        //}

        //public static void Write(Dictionary<string, string> dictionary, string file)
        //{
        //    using (FileStream fs = File.OpenWrite(file))
        //    using (BinaryWriter writer = new BinaryWriter(fs))
        //    {
        //        // Put count.
        //        writer.Write(dictionary.Count);
        //        // Write pairs.
        //        foreach (var pair in dictionary)
        //        {
        //            writer.Write(pair.Key);
        //            writer.Write(pair.Value);
        //        }
        //    }
        //}

        //public static Dictionary<string, string> Read(string file)
        //{
        //    var result = new Dictionary<string, string>();
        //    using (FileStream fs = File.OpenRead(file))
        //    using (BinaryReader reader = new BinaryReader(fs))
        //    {
        //        // Get count.
        //        int count = reader.ReadInt32();
        //        // Read in all pairs.
        //        for (int i = 0; i < count; i++)
        //        {
        //            string key = reader.ReadString();
        //            string value = reader.ReadString();
        //            result.Add(key, value);
        //        }
        //    }
        //    return result;
        //}

        //public static Dictionary<string, string> LoadData()
        //{
        //    var result = new Dictionary<string, string>();
        //    string[] filePaths = Directory.GetFiles(@"C:\Users\Джордж\Documents\Visual Studio 2012\Projects\ImageParser\ImageParser\bin\Debug\cards");
        //    Bitmap temp;
        //    foreach (string file in filePaths)
        //    {
        //       temp = new Bitmap(file);
        //       result.Add(GetHash(temp), file.Substring(file.Length - 2));
        //    }
        //    return result;
        //}

        //private static string GetHash(Bitmap image)
        //{
        //    // get the bytes from the image
        //    byte[] bytes = null;
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        image.Save(ms, ImageFormat.Bmp); // gif for example
        //        bytes = ms.ToArray();
        //    }
        //    // hash the bytes
        //    MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
        //    byte[] hash = md5.ComputeHash(bytes);
        //    string temp = "";
        //    foreach (byte value in hash)
        //    {
        //        temp += value.ToString();
        //    }
        //    return temp;
        //}

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    //SAVE
        //    Write(cards, "saved");
        //}

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    //LOAD
            
        //}

    }
}
