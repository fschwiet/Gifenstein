using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImageResizer;
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
            var builderConfig = new ImageResizer.Configuration.Config();

            new PrettyGifs().Install(builderConfig);
            new AnimatedGifs().Install(builderConfig);
            builderConfig.Plugins.add_plugin(new NoopExtension());

            var imageBuilder = new ImageBuilder(builderConfig.Plugins.ImageBuilderExtensions, builderConfig.Plugins, builderConfig.Pipeline, builderConfig.Pipeline);

            var resizeSettings = new ResizeSettings() { };

            imageBuilder.Build(remainingArguments[0], remainingArguments[1], resizeSettings, false);

            return 0;
        }
    }
}
