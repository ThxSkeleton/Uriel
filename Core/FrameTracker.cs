using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uriel
{
    public class FrameTracker
    {
        int frameCount;

        DateTime start;

        public double averageFramePerSecond;

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
                averageFramePerSecond = 100 / (DateTime.Now - start).TotalSeconds;
            }
        } 
    }
}
