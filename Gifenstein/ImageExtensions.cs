using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Gifenstein
{
    // See GdiPlusImaging.h
    public static class ImageExtensions
    {
        public static int DelayMS(this Image image)
        {
            return BitConverter.ToInt16(image.GetPropertyItem(0x5100).Value, 0) * 10;
        }

        public static int LoopCount(this Image image)
        {
            return BitConverter.ToInt16(image.GetPropertyItem(0x5101).Value, 0);
        }
    }
}
