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
            var frames = GetFramesForSequentialAnimations();

            var background = Image.FromFile(BackgroundImage);

            WriteBackgroundForFrames(background, frames);

            int position = 0;

            AnimationVisitorExtension.Visit(Output, (bitmap, graphics, delay) =>
            {
                graphics.DrawImageUnscaled(frames[position].Image, 0, 0);
                position++;
            }, output:Output);

            return 0;
        }

        private void WriteBackgroundForFrames(Image background, Frame[] frames)
        {
            using (var output = File.OpenWrite(Output))
            using (var outputWriter = new GifEncoder(output))
            {
                foreach (var frame in frames)
                {
                    outputWriter.FrameDelay = TimeSpan.FromMilliseconds(frame.Duration);
                    outputWriter.AddFrame(background);
                }
            }
        }

        private Frame[] GetFramesForSequentialAnimations()
        {
            List<Frame> frames = new List<Frame>();

            int currentTime = 0;
            foreach (var input in Inputs)
            {
                AnimationVisitorExtension.Visit(input, (bitmap, graphic, duration) =>
                {
                    frames.Add(new Frame()
                    {
                        Start = currentTime,
                        Duration = duration,
                        Image = bitmap
                    });

                    currentTime += duration;
                });
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
