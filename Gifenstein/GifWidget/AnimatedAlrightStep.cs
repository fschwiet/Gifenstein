using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Gifenstein.GifWidget
{
    public class AnimatedAlrightStep : AlrightStep
    {
        readonly string _animatedGifPath;
        readonly Rectangle _animagedGifTemplateLocation;
        List<ConcurrentGifsCommand.Frame> _frames;

        public AnimatedAlrightStep(string templatePath, string animatedGifPath, Rectangle animagedGifTemplateLocation) : base(templatePath)
        {
            _animatedGifPath = animatedGifPath;
            _animagedGifTemplateLocation = animagedGifTemplateLocation;
        }

        public override IEnumerable<ConcurrentGifsCommand.Frame> GetFrames(int endOfLastFrame)
        {
            var timePlayedMS = 0;

            List<ConcurrentGifsCommand.Frame> result = new List<ConcurrentGifsCommand.Frame>(); 

            while (timePlayedMS < 1000)
            {
                var lastPosition = endOfLastFrame;

                var newFrames = ConcurrentGifsCommand.GetFramesForSequentialAnimations(new[] { _animatedGifPath }, lastPosition);

                var timePlayedMs = (newFrames.Last().End - newFrames.First().Start);
                timePlayedMS += timePlayedMs;

                result.AddRange(newFrames);
            }

            _frames = result;
            return result;
        }

        public override void DrawFrame(ConcurrentGifsCommand.Frame frame, Graphics gfx)
        {
            if (!_frames.Contains(frame))
                return;

            gfx.DrawImage(frame.Image, 
                new Rectangle(
                    _animagedGifTemplateLocation.X, _animagedGifTemplateLocation.Y + VerticalOffset,
                    _animagedGifTemplateLocation.Width, _animagedGifTemplateLocation.Height));
        }
    }
}