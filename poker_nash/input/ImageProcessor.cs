using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace input
{
    public class ImageProcessor
    {
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

        private Bitmap image;

        public ImageProcessor()
        {
            this.image = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
        }

        public void Snapshot()
        {
            Graphics g = Graphics.FromImage(this.image as Image);
            g.CopyFromScreen(0,0,0,0,this.image.Size);
        }

        public Bitmap PrintScreen(Rectangle rect)
        {
            return image.Clone(rect, PixelFormat.DontCare);
        }
    }
}
