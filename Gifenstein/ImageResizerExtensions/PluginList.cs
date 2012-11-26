using System.Collections.Generic;
using ImageResizer.Plugins;
using ImageResizer.Plugins.AnimatedGifs;
using ImageResizer.Plugins.PrettyGifs;

namespace Gifenstein.ImageResizerExtensions
{
    public class PluginList
    {
        private List<IPlugin> _plugins = new List<IPlugin>();

        public IEnumerable<IPlugin> Plugins { get { return _plugins;  }} 

        public PluginList WithAnimatedGifExtensions()
        {
            var result = new PluginList();
            result._plugins.AddRange(Plugins);
            result._plugins.Add(new PrettyGifs());
            result._plugins.Add(new AnimatedGifs());
            return result;
        }

        public PluginList WithPlugin(IPlugin plugin)
        {
            var result = new PluginList();
            result._plugins.AddRange(Plugins);
            result._plugins.Add(plugin);
            return result;
        }
    }
}