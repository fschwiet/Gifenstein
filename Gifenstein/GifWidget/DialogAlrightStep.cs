using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Gifenstein.GifWidget
{
    public class DialogAlrightStep : AlrightStep
    {
        readonly string _text;

        public DialogAlrightStep(string text) : base("Gifenstein.Resources.AlrightGentlemen_top.png")
        {
            _text = text;
        }

        public override void DrawBackground(System.Drawing.Graphics gfx)
        {
            gfx.DrawString(_text, new Font(FontFamily.GenericSansSerif, 12), new SolidBrush(Color.Black),
                new RectangleF(24, VerticalOffset + 60, 305, 110));

            base.DrawBackground(gfx);
        }
    }
}
