using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Gifenstein.GifWidget;
using Gifenstein.ImageResizerExtensions;
using ImageResizer;
using ManyConsole;

namespace Gifenstein
{
    public class AlrightGentlemenCommand : GifWritingCommand
    {
        public List<BaseWidget> Steps = new List<BaseWidget>();
        public Point ImageDimensions;
        public int MaximumWidth = 500;
        private Dictionary<ConcurrentGifsCommand.Frame, AlrightStep> _frameToStep = new Dictionary<ConcurrentGifsCommand.Frame, AlrightStep>();
        private int? RemoveEveryNFrames;

        public AlrightGentlemenCommand()
        {
            this.IsCommand("alright-gentlemen", "Builder for 'Alright, Gentlemen'");
            this.HasOption("t=", "Text spoken in the next frame.", v => Steps.Add(new DialogAlrightStep(v)));

            this.HasOption("m=", "Gif animation drawn within an unexcited frame", v =>
                Steps.Add(new AlrightUnimpressedStep(v)));

            this.HasOption("w=", "Gif animation drawn with an excited frame", v =>
                Steps.Add(new AlrightImpressedStep(v)));

            this.HasOption<int>("maxw=", "Maximum width to draw the image at", v => MaximumWidth = v);

            this.HasOption<int>("r=", "Remove every <r> frames ", v => RemoveEveryNFrames = v);
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

            if (RemoveEveryNFrames.HasValue)
            {
                animationFrames = animationFrames.Where((f, i) => (i + 1) % RemoveEveryNFrames.Value != 0).ToList();
            }

            ConcurrentGifsCommand.WriteBackgroundForFrames(backgroundImage, animationFrames, Output);

            var currentFrame = 0;

            ImageResizerUtil.ProcessImage(
                new PluginList().WithAnimatedGifExtensions()
                                .WithPlugin(new AnimationVisitorExtension((bitmap, graphics, delay) =>
                                    {
                                        var animationFrame = animationFrames[currentFrame];
                                        foreach (var widget in Steps)
                                        {
                                            widget.DrawFrame(animationFrame, graphics);
                                        }

                                        currentFrame++;
                                    })).Plugins,
                source: Output, target: Output);

            ImageResizerUtil.ProcessImage(
                new PluginList().WithAnimatedGifExtensions().Plugins, 
                source:Output, target:Output, 
                resizeSettings: new ResizeSettings(MaximumWidth, int.MaxValue, FitMode.Max, null));

            Console.WriteLine("Created filesize of {0} was {1}k.", Path.GetFullPath(Output), new FileInfo(Output).Length / 1000.0);

            return 0;
        }
    }
}
