using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            imageBuilder.Build(source, target, resizeSettings, false);
        }
    }
}
