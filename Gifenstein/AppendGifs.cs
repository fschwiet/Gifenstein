﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using BumpKit;
using Gifenstein.ImageResizerExtensions;
using ImageResizer.Plugins;
using ImageResizer.Plugins.AnimatedGifs;
using ImageResizer.Plugins.PrettyGifs;
using ManyConsole;

namespace Gifenstein
{
    public class AppendGifs : GifWritingCommand
    {
        private List<string> Inputs = new List<string>();

        public AppendGifs()
        {
            this.IsCommand("append-gifs", "Creates an animated gif by sequencing existing gifs.");
            this.HasOption("n=", "Next input gif to append", v => Inputs.Add(v));
        }

        public override int Run(string[] remainingArguments)
        {
            using(var output = File.OpenWrite(Output))
            using(var outputWriter = new GifEncoder(output))
            {
                foreach(var inputFile in Inputs)
                {
                    ImageResizerUtil.ProcessImage(
                        new PluginList().WithAnimatedGifExtensions().WithPlugin(new AnimationVisitorExtension((bitmap, graphic, delay) =>
                            {
                                outputWriter.FrameDelay = TimeSpan.FromMilliseconds(delay);
                                outputWriter.AddFrame(bitmap);
                            })).Plugins, 
                            inputFile);
                }
            }

            return 0;
        }
    }
}
