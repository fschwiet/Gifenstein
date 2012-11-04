using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Gifenstein.GifWidget
{
    public interface BaseWidget
    {
        Point GetDimensions(Point incomingDimensions);

    }
}
