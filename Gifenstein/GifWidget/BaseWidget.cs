using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Gifenstein.GifWidget
{
    public class BaseWidget
    {
        public virtual Point GetDimensions(Point incomingDimensions)
        {
            return incomingDimensions;    
        }

        public virtual void DrawBackground(Graphics gfx)
        {
        }

        public virtual IEnumerable<ConcurrentGifsCommand.Frame> GetFrames(int endOfLastFrame)
        {
            return new ConcurrentGifsCommand.Frame[0];
        }

        public virtual void DrawFrame(ConcurrentGifsCommand.Frame frame, Graphics gfx)
        {
        }
    }
}
