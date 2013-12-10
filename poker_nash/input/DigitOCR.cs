using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using tessnet2;

namespace input
{
    public class DigitOcr
    {
        public static double Recognize(Bitmap bmp)
        {
            var ocr = new Tesseract();
            ocr.SetVariable("tessedit_char_whitelist", "0123456789,$");
            ocr.Init(null, "eng", false);
            List<Word> result = ocr.DoOCR(bmp, Rectangle.Empty);

            return Convert.ToDouble(result[0].Text.Replace("$", ""));
        }
    }
}
