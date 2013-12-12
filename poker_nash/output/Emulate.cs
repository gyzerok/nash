using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

using common;

namespace output
{
    public class Emulate : IOutput
    {        	
        private const UInt32 MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const UInt32 MOUSEEVENTF_LEFTUP = 0x0004;
        private Random rnd;

        public Emulate()
        {
            this.rnd = new Random();
        }

        public void Act(Activity activity)
        {
            if (activity == null) return;

            int x, y;
            switch (activity.Type)
            {
                case Decision.Fold:
                    x = this.rnd.Next(722, 806);
                    y = this.rnd.Next(663, 702);
                    this.Click(new Point(x, y));
                    break;
                case Decision.Call:
                    x = this.rnd.Next(883, 981);
                    y = this.rnd.Next(657, 696);
                    this.Click(new Point(x, y));
                    break;
                case Decision.Raise:
                    x = this.rnd.Next(1049, 1151);
                    y = this.rnd.Next(657, 699);
                    this.Click(new Point(x, y));
                    break;
            }
        }

        private void Click(Point point)
        {
            Cursor.Position = point;
            
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, new IntPtr());
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, new IntPtr());

            Cursor.Position = new Point(0, 0);
        }

        [DllImport("user32.dll")]
        private static extern void mouse_event(UInt32 dwFlags, UInt32 dx, UInt32 dy, UInt32 dwData, IntPtr dwExtraInfo);
    }
}
