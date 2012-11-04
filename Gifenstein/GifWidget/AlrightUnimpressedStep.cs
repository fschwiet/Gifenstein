using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Gifenstein.GifWidget
{
    public class AlrightUnimpressedStep : AnimatedAlrightStep
    {
        public AlrightUnimpressedStep(string animationGifPath)
            : base("Gifenstein.Resources.AllRightGentlemen_unimpressed.png", animationGifPath, new Rectangle(5, 8, 270, 201))
        {
        }
    }
}
