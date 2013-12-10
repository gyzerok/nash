using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ImageParser
{
    class AnchorPlacer
    {
        List<Point> Anchors;
        Form1 Parent;
        public Rectangle rect = new Rectangle(650, 461, 15, 40);
        public Rectangle rect2 = new Rectangle(671, 466, 15, 40);
        public Rectangle flopRect1 = new Rectangle(533, 228, 15, 40);
        public Rectangle flopRect2 = new Rectangle(598, 228, 15, 40);
        public Rectangle flopRect3 = new Rectangle(663, 228, 15, 40);
        public Rectangle turnRect = new Rectangle(728, 228, 15, 40);
        public Rectangle riverRect = new Rectangle(793, 228, 15, 40);

        public AnchorPlacer(Form1 parent)
        {
            this.Parent = parent;
            Anchors = new List<Point> { new Point(650, 450), new Point(670, 450), new Point(650, 500), new Point(670, 500) };
        }

        public Bitmap PrintScreen(Rectangle rect)
        {
            Bitmap bitmap = new Bitmap(
                rect.Width, rect.Height);

            using (Graphics bmpGraphics = Graphics.FromImage(bitmap))
                bmpGraphics.CopyFromScreen(rect.X, rect.Y, 0, 0,
                    new Size(rect.Width, rect.Height));

            return bitmap;
        }    


    }
}
