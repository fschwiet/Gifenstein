using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

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
            List<Frame> frames = new List<Frame>();


            throw new NotImplementedException();
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
