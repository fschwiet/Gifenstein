using System;
using System.Drawing;

namespace Gifenstein.Hmm
{
    public class DronePandemic
    {
        public void Build()
        {
            var backgroundImage = hasImage("pathToBusinessMeeting");
            var lethal = hasAnimation("pathToAnimation");
            var artificialIntelligence = hasAnimation("pathToAnimation");
            var beesAnimation = hasAnimation("beesPath");;

            var firstFrames = lethal.frameDelays;
            var secondFrames = artificialIntelligence.frameDelays;
            var thirdFrames = beesAnimation.frameDelays;

            var lethalAnimationPosition = new FramePosition() { X = 10, Y = 10, Width = 50, Height = 50};
            var artificialIntelligencePosition = new FramePosition() { X = 10, Y = 60, Width = 50, Height = 50};
            var beesPosition = new FramePosition() { X = 10, Y = 110, Width = 50, Height = 50};

            buildFrames(builder => 
                builder.usingBackground(backgroundImage).including(firstFrames).including(secondFrames).including(thirdFrames).processingEach(frame =>
                {
                    if (firstFrames.HaveStartedBy(frame))
	                {
		                var subframe = lethal.frameAt((frame.timeCurrent - firstFrames.timeStart) % firstFrames.timeTotal);
		                frame.draw(subframe, lethalAnimationPosition);
	                }
	
	                if (secondFrames.HaveStartedBy(frame))
	                {
		                var subframe = artificialIntelligence.frameAt((frame.timeCurrent - secondFrames.timeStart) % secondFrames.timeTotal);
                        frame.draw(subframe, artificialIntelligencePosition);
	                }
	
	                if (thirdFrames.HaveStartedBy(frame))
	                {
		                var subframe = beesAnimation.frameAt((frame.timeCurrent - thirdFrames.timeStart) % secondFrames.timeTotal);
                        frame.draw(subframe, beesPosition);
    	            }	
                }));
        }

        void buildFrames(Action<IFrameBuilder> func)
        {
            throw new NotImplementedException();
        }

        public IAnimation hasAnimation(string path)
        {
            throw new NotImplementedException();
        }

        public Image hasImage(string path)
        {
            throw new NotImplementedException();
        }

    }
}
