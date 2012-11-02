using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BumpKit;
using Gifenstein.ImageResizerExtensions;
using ImageResizer.Plugins;
using ImageResizer.Plugins.AnimatedGifs;
using ManyConsole;

namespace Gifenstein
{
    public class AppendGifs : ConsoleCommand
    {
        private List<string> Inputs = new List<string>();
        private string Output;

        public AppendGifs()
        {
            this.IsCommand("append-gifs", "Creates an animated gif by sequencing existing gifs.");
            this.HasRequiredOption("o=", "Output file (gif will be appended if not present)", v => Output = v);
            this.HasOption("n=", "Next input gif to append", v => Inputs.Add(v));
        }

        public override int? OverrideAfterHandlingArgumentsBeforeRun(string[] remainingArguments)
        {
            if (!Output.EndsWith(".gif"))
                Output = Output.TrimEnd('.') + ".gif";

            return base.OverrideAfterHandlingArgumentsBeforeRun(remainingArguments);
        }

        public override int Run(string[] remainingArguments)
        {
            using(var output = File.OpenWrite(Output))
            using(var outputWriter = new GifEncoder(output))
            {
                foreach(var inputFile in Inputs)
                {
                    ImageResizerUtil.ProcessImage(new IPlugin[]
                    {
                        new AnimatedGifs(),
                        new AnimationVisitorExtension( (bitmap,graphic, delay) =>
                        {
                            outputWriter.FrameDelay = TimeSpan.FromMilliseconds(delay);
                            outputWriter.AddFrame(bitmap);
                        })
                    }, inputFile, new MemoryStream());
                }
            }

            return 0;
        }
    }
}
