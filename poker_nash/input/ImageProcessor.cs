using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace input
{
    public static class ImageProcessor
    {
        public static Bitmap Snapshot()
        {
            Bitmap image = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(image as Image);
            g.CopyFromScreen(0,0,0,0,image.Size);
            return image;
        }

        public static Bitmap Crop(Bitmap source, Rectangle rect)
        {
            return source.Clone(rect, PixelFormat.DontCare);
        }

        public static Bitmap DetectBet(Bitmap image)
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
                    if (image.GetPixel(j, i) == Color.FromArgb(255, 255, 246, 207))
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
                maxY += 2;
                maxX += 2;
                return image.Clone(new Rectangle(minX, minY, maxX - minX, maxY - minY), PixelFormat.DontCare);
            }
            return null;
        }

    }
}
