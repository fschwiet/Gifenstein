using System;
using System.Drawing;

namespace Gifenstein.GifWidget
{
    public class AlrightStep : BaseWidget
    {
        public AlrightStep(string templatePath)
        {
            _templatePath = templatePath;
        }

        public Image Image;
        public int VerticalOffset;
        public int Height;
        string _templatePath;

        public override Point GetDimensions(Point incomingDimensions)
        {
            Image = Image.FromStream(this.GetType().Assembly.GetManifestResourceStream(_templatePath));

            VerticalOffset = incomingDimensions.Y;
            Height = Image.Height;

            return new Point(Math.Max(incomingDimensions.X, Image.Width), incomingDimensions.Y + Image.Height);
        }

        public override void DrawBackground(Graphics gfx)
        {
            gfx.DrawImageUnscaledAndClipped(Image, new Rectangle(0, VerticalOffset, Image.Width, Image.Height));
        }
    }
}