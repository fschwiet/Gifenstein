using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using ImageResizer.Configuration;
using ImageResizer.Plugins;
using ImageResizer.Plugins.AnimatedGifs;
using ImageResizer.Plugins.PrettyGifs;
using ImageResizer.Resizing;

namespace Gifenstein.ImageResizerExtensions
{
    public class AnimationVisitorExtension : BuilderExtension, IPlugin
    {
        private readonly Action<Bitmap, Graphics, int> _visitor;
        private int[] _delays;
        private int _visitIndex;
        protected Config c;

        public AnimationVisitorExtension(Action<Bitmap, Graphics, int> visitor)
        {
            _visitor = visitor;
        }

        public IPlugin Install(Config c)
        {
            c.Plugins.add_plugin(this);
            this.c = c;
            return this;
        }

        public bool Uninstall(Config c)
        {
            c.Plugins.remove_plugin(this);
            return true;
        }

        protected override RequestedAction PostRenderImage(ImageState s)
        {
            _delays = _delays ?? GetDelays(s.sourceBitmap);

            _visitor((Bitmap)s.destBitmap.Clone(), s.destGraphics, _delays[_visitIndex++]);

            return base.PostRenderImage(s);
        }

        int[] GetDelays(Bitmap image)
        {
            List<int> results = new List<int>();

            var frameDimension = image.FrameDimensionsList.Single();
            var frameCount = image.GetFrameCount(new FrameDimension(frameDimension));

            for (var i = 0; i < frameCount; i++)
            {
                image.SelectActiveFrame(new FrameDimension(frameDimension), i);

                results.Add(image.DelayMS());
            }

            return results.ToArray();
        }

        public static void Visit(object input, Action<Bitmap, Graphics, int> visitor, object output = null)
        {
            ImageResizerUtil.ProcessImage(new IPlugin[]
            {
                new PrettyGifs(),
                new AnimatedGifs(),
                new AnimationVisitorExtension(visitor)
            }, input, output ?? new MemoryStream());
        }
    }
}
