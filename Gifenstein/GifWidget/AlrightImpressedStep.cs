using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Gifenstein.GifWidget
{
    public class AlrightImpressedStep : AlrightUnimpressedStep
    {
        int _startTime;

        public AlrightImpressedStep(string animationGifPath) : base(animationGifPath)
        {
            _animagedGifTemplateLocation = new Rectangle(5, 6, 270, 203);
        }

        public override IEnumerable<ConcurrentGifsCommand.Frame> GetFrames(int endOfLastFrame)
        {
            var result = base.GetFrames(endOfLastFrame);
            _startTime = result.First().Start;
            return result;
        }

        public override void DrawFrame(ConcurrentGifsCommand.Frame currentFrame, Graphics gfx)
        {
            if (currentFrame.Start < _startTime)
                return;

            var newBackground = Image.FromStream(this.GetType().Assembly.GetManifestResourceStream(
                "Gifenstein.Resources.AllRightGentlemen_impressed.png"));

            gfx.DrawImageUnscaledAndClipped(newBackground, new Rectangle(0, VerticalOffset, newBackground.Width, newBackground.Height));


            base.DrawFrame(currentFrame, gfx);
        }
    }
}
