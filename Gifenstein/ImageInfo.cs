using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using ManyConsole;

namespace Gifenstein
{
    public class ImageInfo : ConsoleCommand
    {
        public ImageInfo()
        {
            this.IsCommand("info", "Displays information about the image.");
            this.HasAdditionalArguments(1, "<filename>");
        }
        public override int Run(string[] remainingArguments)
        {
            var image = Image.FromFile(remainingArguments[0]);

            foreach(var frameDimension in image.FrameDimensionsList)
            {
                var frameCount = image.GetFrameCount(new FrameDimension(frameDimension));

                Console.WriteLine("For frame dimension {0} have {1} frames", frameDimension, frameCount);

                for(var i = 0; i < frameCount; i++)
                {
                    image.SelectActiveFrame(new FrameDimension(frameDimension), i);

                    Console.WriteLine("    Frame #{0} delay: {1}, loop count: {2}", i, image.Delay(), image.LoopCount());
                }
            }

            return 0;
        }
    }
}
