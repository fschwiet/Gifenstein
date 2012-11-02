using System;
using System.Drawing;

namespace Gifenstein.Hmm
{
    public interface IFrameBuilder
    {
        IFrameBuilder usingBackground(Image magebackgroundImage);
        IFrameBuilder including(IFramePositions firstFrames);
        IFrameBuilder processingEach(Action<IFrame> action);
    }

    public interface IAnimation
    {
        IFramePositions frameDelays { get; set; }
        IImage frameAt(object o);
    }

    public interface IImage

    {
    }

    public interface IFramePositions
    {
        bool HaveStartedBy(IFrame frame);
        int timeStart { get; set; }
        int timeTotal { get; set; }
    }

    public interface IFrame
    {
        int timeCurrent { get; set; }
        void draw(IImage subframe, FramePosition position);
    }
}