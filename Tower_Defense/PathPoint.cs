using System.Drawing;

namespace Tower_Defense
{
    public class PathPoint
    {
        public PointF point;
        public string direction;

        public PathPoint(PointF point , string direction)
        {
            this.direction = direction;
            this.point = point;
        }
    }
}