using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Gifenstein.GifWidget
{
    public class DialogAlrightStep : AlrightStep
    {
        string _text;
        int _startTime;
        const int waitBeforeText = 800;
        const int waitAfterText = 800;

        public DialogAlrightStep(string text) : base("Gifenstein.Resources.AlrightGentlemen_top.png")
        {
            _text = text;
        }

        public override IEnumerable<ConcurrentGifsCommand.Frame> GetFrames(int endOfLastFrame)
        {
            _startTime = endOfLastFrame + waitBeforeText;

            return new ConcurrentGifsCommand.Frame[]
            {
                new ConcurrentGifsCommand.Frame()
                {
                    Start = endOfLastFrame,
                    Duration = waitBeforeText,
                },
                new ConcurrentGifsCommand.Frame()
                {
                    Start = endOfLastFrame + waitBeforeText,
                    Duration = waitAfterText
                }
            };
        }

        public override void DrawBackground(System.Drawing.Graphics gfx)
        {
            base.DrawBackground(gfx);
        }

        public override void DrawFrame(ConcurrentGifsCommand.Frame frame, Graphics gfx)
        {
            if (frame.Start >= _startTime)
            {
                gfx.DrawString(_text, new Font(FontFamily.GenericSansSerif, 12), new SolidBrush(Color.Black),
                    new RectangleF(24, VerticalOffset + 60, 305, 110));
            }
            
            base.DrawFrame(frame, gfx);
        }

        public override string ToString()
        {
            return String.Format("{0} ({1})", this.GetType().Name, _text);
        }
    }
}
