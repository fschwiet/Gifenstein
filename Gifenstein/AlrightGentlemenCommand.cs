using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Gifenstein.ImageResizerExtensions;
using ManyConsole;

namespace Gifenstein
{
    public class AlrightGentlemenCommand : GifWritingCommand
    {
        public string Text;
        public List<AlrightStep> Steps = new List<AlrightStep>();
        public int ImageHeight;
        private Dictionary<ConcurrentGifsCommand.Frame, AlrightStep> _frameToStep = new Dictionary<ConcurrentGifsCommand.Frame, AlrightStep>();

        public AlrightGentlemenCommand()
        {
            this.IsCommand("alright-gentlemen", "Builder for 'Alright, Gentlemen'");
            this.HasOption("t=", "Text to use", v => Text = v);

            ImageHeight = 0;

            var introImage = Image.FromStream(
                this.GetType().Assembly.GetManifestResourceStream("Gifenstein.Resources.AlrightGentlemen_top.png"));

            Steps.Add(new AlrightStep()
            {
                Image = introImage,
                VerticalOffset = ImageHeight,
                Height = introImage.Height,
            });

            ImageHeight += introImage.Height;

            this.HasOption("m=", "Gif animation to show as the next mild frame", v =>
            {
                var image = Image.FromStream(
                    this.GetType().Assembly.GetManifestResourceStream("Gifenstein.Resources.AlrightGentlemen_unimpressed.png"));

                Steps.Add(new AlrightStep()
                {
                    Image = image,
                    VerticalOffset = ImageHeight,
                    Height = image.Height,
                    Source = v,
                    Target = new Rectangle(4, ImageHeight + 5, 269, 200)
                });

                ImageHeight += image.Height;
            });

            this.HasOption("w=", "Gif animation to show as the next wild frame", v =>
            {
                var image = Image.FromStream(
                    this.GetType().Assembly.GetManifestResourceStream("Gifenstein.Resources.AlrightGentlemen_wow.png"));

                Steps.Add(new AlrightStep()
                {
                    Image = image,
                    VerticalOffset = ImageHeight,
                    Height = image.Height,
                    Source = v,
                    Target = new Rectangle(5, ImageHeight + 6, 269, 200)
                });

                ImageHeight += image.Height;
            });
        }

        public override int? OverrideAfterHandlingArgumentsBeforeRun(string[] remainingArguments)
        {
            if (Steps.Count() == 0)
                throw new ConsoleHelpAsException("Must specify at least one frame with u or w parameter.");

            return base.OverrideAfterHandlingArgumentsBeforeRun(remainingArguments);
        }

        public override int Run(string[] remainingArguments)
        {
            var backgroundImage = new Bitmap(593, ImageHeight);
            using(var gfx = Graphics.FromImage(backgroundImage))
            foreach(var frame in Steps)
            {
                gfx.DrawImageUnscaledAndClipped(frame.Image, new Rectangle(0, frame.VerticalOffset, 593, frame.Height));
            }

            backgroundImage.Save(Output);

            List<ConcurrentGifsCommand.Frame> animationFrames = new List<ConcurrentGifsCommand.Frame>();

            foreach(var step in Steps)
            {
                if (string.IsNullOrEmpty(step.Source))
                    continue;

                var lastPosition = 0;
                
                if (animationFrames.Any())
                    lastPosition = animationFrames.Last().End;

                var newFrames = ConcurrentGifsCommand.GetFramesForSequentialAnimations(new[] {step.Source}, lastPosition);

                animationFrames.AddRange(newFrames);

                foreach(var frame in newFrames)
                {
                    _frameToStep[frame] = step;
                }
            }

            ConcurrentGifsCommand.WriteBackgroundForFrames(backgroundImage, animationFrames, Output);

            var currentFrame = 0;

            AnimationVisitorExtension.Visit(Output, (bitmap, graphics, delay) =>
            {
                var animationFrame = animationFrames[currentFrame];
                graphics.DrawString(Text, new Font(FontFamily.GenericSansSerif, 12), new SolidBrush(Color.Black),
                    new RectangleF(24, 60, 305, 110));
                graphics.DrawImage(animationFrame.Image, _frameToStep[animationFrame].Target);
                currentFrame++;
            }, output:Output);

            return 0;
        }

        public class AlrightStep
        {
            public int VerticalOffset;
            public int Height;
            public Image Image;
            public string Source;
            public Rectangle Target;
        }
    }
}
