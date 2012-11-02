using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gifenstein.ImageResizerExtensions;
using ImageResizer;
using ImageResizer.Plugins;
using ImageResizer.Plugins.AnimatedGifs;
using ImageResizer.Plugins.PrettyGifs;
using ManyConsole;

namespace Gifenstein
{
    public class NoOpTransformCommand : ConsoleCommand
    {
        public NoOpTransformCommand()
        {
            this.IsCommand("no-op", "Applies a no-op transformation to an image.");
            this.HasAdditionalArguments(2, "<input file> <output file>");
        }
        
        public override int Run(string[] remainingArguments)
        {
            var extensions = new IPlugin[] { new PrettyGifs(), new AnimatedGifs(), new NoopExtension() };
            var source = remainingArguments[0];
            var target = remainingArguments[1];
            
            ImageResizerUtil.ProcessImage(extensions, source, target);

            return 0;
        }
    }
}
