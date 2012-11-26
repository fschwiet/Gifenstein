using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using BumpKit;
using Gifenstein.ImageResizerExtensions;

namespace Gifenstein
{
    public class ConcurrentGifsCommand : GifWritingCommand
    {
        public string BackgroundImage;
        public List<string> Inputs = new List<string>(); 

        public ConcurrentGifsCommand()
        {
            this.IsCommand("concurrent-gifs", "Just trying out merging gifs");
            this.HasRequiredOption("b=", "Background image", v => BackgroundImage = v);
            this.HasOption("n=", "Image file to include", v => Inputs.Add(v));
        }

        public override int Run(string[] remainingArguments)
        {
            var frames = GetFramesForSequentialAnimations(this.Inputs);

            var background = Image.FromFile(BackgroundImage);

            WriteBackgroundForFrames(background, frames, Output);

            int position = 0;

            ImageResizerUtil.ProcessImage(
                new PluginList().WithAnimatedGifExtensions().WithPlugin(new AnimationVisitorExtension((bitmap, graphics, delay) =>
                    {
                        graphics.DrawImageUnscaled(frames[position].Image, 0, 0);
                        position++;
                    })).Plugins, 
                    source: Output, target: Output);

            return 0;
        }

        public static void WriteBackgroundForFrames(Image background, IEnumerable<Frame> frames, string outputPath)
        {
            using (var output = File.OpenWrite(outputPath))
            using (var outputWriter = new GifEncoder(output))
            {
                foreach (var frame in frames)
                {
                    outputWriter.FrameDelay = TimeSpan.FromMilliseconds(frame.Duration);
                    outputWriter.AddFrame(background);
                }
            }
        }

        public static Frame[] GetFramesForSequentialAnimations(IEnumerable<string> inputs, int startTime = 0)
        {
            List<Frame> frames = new List<Frame>();

            int currentTime = startTime;
            foreach (var input in inputs)
            {
                ImageResizerUtil.ProcessImage(
                    new PluginList().WithAnimatedGifExtensions().WithPlugin(new AnimationVisitorExtension((bitmap, graphic, duration) =>
                        {
                            frames.Add(new Frame()
                                {
                                    Start = currentTime,
                                    Duration = duration,
                                    Image = bitmap
                                });

                            currentTime += duration;
                        })).Plugins, 
                        input);
            }
            return frames.ToArray();
        }

        public class Frame
        {
            public int Start;
            public int Duration;
            public Bitmap Image;

            public int End { get { return Start + Duration; } }
        };
    }
}
