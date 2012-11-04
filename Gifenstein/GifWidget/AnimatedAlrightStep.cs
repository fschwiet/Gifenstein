using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Gifenstein.GifWidget
{
    public class AnimatedAlrightStep : AlrightStep
    {
        readonly string _animatedGifPath;
        protected Rectangle _animagedGifTemplateLocation;
        List<ConcurrentGifsCommand.Frame> _frames;
        const int MinIntroTrime = 1500;
        const int MaxIntroTime = 2500;

        public AnimatedAlrightStep(string templatePath, string animatedGifPath, Rectangle animagedGifTemplateLocation) : base(templatePath)
        {
            _animatedGifPath = animatedGifPath;
            _animagedGifTemplateLocation = animagedGifTemplateLocation;
        }

        public override IEnumerable<ConcurrentGifsCommand.Frame> GetFrames(int endOfLastFrame)
        {
            var timePlayedMS = 0;

            List<ConcurrentGifsCommand.Frame> result = new List<ConcurrentGifsCommand.Frame>(); 

            while (timePlayedMS < MinIntroTrime)
            {
                var lastPosition = endOfLastFrame;

                var newFrames = ConcurrentGifsCommand.GetFramesForSequentialAnimations(new[] { _animatedGifPath }, lastPosition);

                var timePlayedMs = (newFrames.Last().End - newFrames.First().Start);
                timePlayedMS += timePlayedMs;

                result.AddRange(newFrames);
            }

            _frames = result;
            
            return result.Where(r => r.Start - endOfLastFrame < MaxIntroTime);
        }

        public override void DrawFrame(ConcurrentGifsCommand.Frame currentFrame, Graphics gfx)
        {
            if (_frames.Contains(currentFrame))
            {
                DrawPositionFrame(currentFrame, gfx);
            }
            else
            {
                var widgetStart = _frames.First().Start;
                var widgetDuration = _frames.Last().End - widgetStart;

                if (currentFrame.Start > widgetStart)
                {
                    var relativePosition = (currentFrame.Start - widgetStart) % widgetDuration;
                    var relativeFrame = _frames.Where(f => f.Start <= widgetStart + relativePosition).LastOrDefault();
                    
                    DrawFrame(relativeFrame, gfx);
                }
            }

        }

        void DrawPositionFrame(ConcurrentGifsCommand.Frame frame, Graphics gfx)
        {
            gfx.DrawImage(frame.Image,
                          new Rectangle(
                              _animagedGifTemplateLocation.X, _animagedGifTemplateLocation.Y + VerticalOffset,
                              _animagedGifTemplateLocation.Width, _animagedGifTemplateLocation.Height));
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", this.GetType().Name, _animatedGifPath);
        }
    }
}