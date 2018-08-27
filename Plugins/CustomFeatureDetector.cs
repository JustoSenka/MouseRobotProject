using System.Collections.Generic;
using System.Drawing;

namespace RobotRuntime.Graphics
{
    public class CustomFeatureDetector : FeatureDetector
    {
        public override string Name
        {
            get
            {
                return "Test";
            }
        } 

        public override IEnumerable<Point[]> FindImageMultiplePos(Bitmap sampleImage, Bitmap observedImage)
        {
            yield return new[] { new Point(50, 100), new Point(100, 100), new Point(100, 50), new Point(50, 50) };
        }

        public override Point[] FindImagePos(Bitmap sampleImage, Bitmap observedImage)
        {
            return new[] { new Point(50, 100), new Point(100, 100), new Point(100, 50), new Point(50, 50) };
        }
    }
}
