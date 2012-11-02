using System;
using ImageResizer.Configuration;
using ImageResizer.Plugins;
using ImageResizer.Resizing;

namespace Gifenstein.ImageResizerExtensions
{
    public class NoopExtension : BuilderExtension, IPlugin
    {
        protected Config c;
        public IPlugin Install(Config c)
        {
            c.Plugins.add_plugin(this);
            this.c = c;
            return this;
        }

        public bool Uninstall(Config c)
        {
            c.Plugins.remove_plugin(this);
            return true;
        }

        private int i = 0;

        protected override RequestedAction PostRenderEffects(ImageState s)
        {
            Console.WriteLine("PostLayoutEffects " + ++i);
            
            return base.PostLayoutEffects(s);
        }
    }
}
