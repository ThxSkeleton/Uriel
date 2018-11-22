using System;

namespace Uriel
{
    public class FrameTracker
    {
        int frameCount;

        DateTime start;

        public float averageFramePerSecond;

        public void StartFrame()
        {
            if (frameCount % 100 == 0)
            {
                start = DateTime.Now;
            }

            frameCount++;
        }

        public void EndFrame()
        {
            if (frameCount % 100 == 0)
            {
                averageFramePerSecond = 100 / (float) (DateTime.Now - start).TotalSeconds;
            }
        } 
    }
}
