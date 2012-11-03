using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using ManyConsole;

namespace Gifenstein
{
    public class AlrightGentlemenCommand : GifWritingCommand
    {
        public List<Frame> Frames = new List<Frame>();
        public int ImageHeight;

        public AlrightGentlemenCommand()
        {
            this.IsCommand("alright-gentlemen", "Builder for 'Alright, Gentlemen'");

            ImageHeight = 0;

            this.HasOption("m=", "Gif animation to show as the next mild frame", v =>
            {
                var image = Image.FromStream(
                    this.GetType().Assembly.GetManifestResourceStream("Gifenstein.Resources.AlrightGentlemen_unimpressed.png"));

                Frames.Add(new Frame()
                {
                    Image = image,
                    VerticalOffset = ImageHeight,
                    Height = image.Height,
                    Source = v
                });

                ImageHeight += image.Height;
            });

            this.HasOption("w=", "Gif animation to show as the next wild frame", v =>
            {
                var image = Image.FromStream(
                    this.GetType().Assembly.GetManifestResourceStream("Gifenstein.Resources.AlrightGentlemen_unimpressed.png"));

                Frames.Add(new Frame()
                {
                    Image = image,
                    VerticalOffset = ImageHeight,
                    Height = image.Height,
                    Source = v
                });

                ImageHeight += image.Height;
            });
        }

        public override int? OverrideAfterHandlingArgumentsBeforeRun(string[] remainingArguments)
        {
            if (Frames.Count() == 0)
                throw new ConsoleHelpAsException("Must specify at least one frame with u or w parameter.");

            return base.OverrideAfterHandlingArgumentsBeforeRun(remainingArguments);
        }

        public override int Run(string[] remainingArguments)
        {
            var backgroundImage = new Bitmap(593, ImageHeight);
            using(var gfx = Graphics.FromImage(backgroundImage))
            foreach(var frame in Frames)
            {
                gfx.DrawImageUnscaledAndClipped(frame.Image, new Rectangle(0, frame.VerticalOffset, 593, frame.Height));
            }

            backgroundImage.Save(Output);

            var frames = ConcurrentGifsCommand.GetFramesForSequentialAnimations(Frames.Select(f => f.Source));

            ConcurrentGifsCommand.WriteBackgroundForFrames(backgroundImage, frames, Output);

            return 0;
        }

        public class Frame
        {
            public int VerticalOffset;
            public int Height;
            public Image Image;
            public string Source;
        }
    }
}
