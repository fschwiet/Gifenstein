using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using ImageResizer;
using ImageResizer.Plugins;

namespace Gifenstein
{
    class ImageResizerUtil
    {
        public static void ProcessImage(IEnumerable<IPlugin> extensions, object source, object target = null, ResizeSettings resizeSettings = null)
        {
            resizeSettings = resizeSettings ?? new ResizeSettings() {};

            target = target ?? new MemoryStream();

            var builderConfig = new ImageResizer.Configuration.Config();

            foreach (var extension in extensions)
                extension.Install(builderConfig);

            var imageBuilder = new ImageBuilder(builderConfig.Plugins.ImageBuilderExtensions, builderConfig.Plugins,
                builderConfig.Pipeline, builderConfig.Pipeline);

            Console.WriteLine("processing " + source);
            if (source is string && source.ToString().StartsWith("http"))
            {
                source = new MemoryStream(new WebClient().DownloadData(source.ToString()));
            }

            imageBuilder.Build(source, target, resizeSettings, false);
        }
    }
}
