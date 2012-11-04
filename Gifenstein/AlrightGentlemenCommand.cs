using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Gifenstein.GifWidget;
using Gifenstein.ImageResizerExtensions;
using ManyConsole;

namespace Gifenstein
{
    public class AlrightGentlemenCommand : GifWritingCommand
    {
        public List<BaseWidget> Steps = new List<BaseWidget>();
        public Point ImageDimensions;
        private Dictionary<ConcurrentGifsCommand.Frame, AlrightStep> _frameToStep = new Dictionary<ConcurrentGifsCommand.Frame, AlrightStep>();

        public AlrightGentlemenCommand()
        {
            this.IsCommand("alright-gentlemen", "Builder for 'Alright, Gentlemen'");
            this.HasOption("t=", "Text to use", v => Steps.Add(new DialogAlrightStep(v)));

            this.HasOption("m=", "Gif animation to show as the next mild frame", v =>
                Steps.Add(new AnimatedAlrightStep("Gifenstein.Resources.AlrightGentlemen_unimpressed.png", v, new Rectangle(4,5,269,200))));

            this.HasOption("w=", "Gif animation to show as the next wild frame", v =>
                Steps.Add(new AnimatedAlrightStep("Gifenstein.Resources.AlrightGentlemen_wow.png", v, new Rectangle(5,6,269,200))));
        }

        public override int? OverrideAfterHandlingArgumentsBeforeRun(string[] remainingArguments)
        {
            if (Steps.Count() == 0)
                throw new ConsoleHelpAsException("Must specify at least one frame with u or w parameter.");

            ImageDimensions = new Point(0,0);

            foreach(var step in Steps)
            {
                ImageDimensions = step.GetDimensions(ImageDimensions);
            }

            return base.OverrideAfterHandlingArgumentsBeforeRun(remainingArguments);
        }

        public override int Run(string[] remainingArguments)
        {
            var backgroundImage = new Bitmap(ImageDimensions.X, ImageDimensions.Y);
            
            using(var gfx = Graphics.FromImage(backgroundImage))
            foreach(var widget in Steps)
            {
                widget.DrawBackground(gfx);
            }

            backgroundImage.Save(Output);

            List<ConcurrentGifsCommand.Frame> animationFrames = new List<ConcurrentGifsCommand.Frame>();

            foreach (var widget in Steps)
            {
                var lastPosition = 0;
                if (animationFrames.Any())
                    lastPosition = animationFrames.Last().End;

                animationFrames.AddRange(widget.GetFrames(lastPosition));
            }

            ConcurrentGifsCommand.WriteBackgroundForFrames(backgroundImage, animationFrames, Output);

            var currentFrame = 0;

            AnimationVisitorExtension.Visit(Output, (bitmap, graphics, delay) =>
            {
                var animationFrame = animationFrames[currentFrame];
                foreach(var widget in Steps)
                {
                    widget.DrawFrame(animationFrame, graphics);
                }

                currentFrame++;
            }, output:Output);

            Console.WriteLine("Created filesize of {0} was {1}k.", Path.GetFullPath(Output), new FileInfo(Output).Length / 1000.0);

            return 0;
        }
    }
}
