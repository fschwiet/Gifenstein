using System;
using System.Drawing;

namespace Gifenstein.GifWidget
{
    public class AlrightStep : BaseWidget
    {
        public int VerticalOffset;
        Image _image;
        string _templatePath;

        public AlrightStep(string templatePath)
        {
            _templatePath = templatePath;
        }

        public override Point GetDimensions(Point incomingDimensions)
        {
            _image = Image.FromStream(this.GetType().Assembly.GetManifestResourceStream(_templatePath));

            VerticalOffset = incomingDimensions.Y;

            return new Point(Math.Max(incomingDimensions.X, _image.Width), incomingDimensions.Y + _image.Height);
        }

        public override void DrawBackground(Graphics gfx)
        {
            gfx.DrawImageUnscaledAndClipped(_image, new Rectangle(0, VerticalOffset, _image.Width, _image.Height));
        }
    }
}