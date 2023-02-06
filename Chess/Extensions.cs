using System;
using System.Drawing;

namespace Chess
{
    public static class Extensions
    {
        public static Point Abs(this Point point)
        {
            point.X = Math.Abs(point.X);
            point.Y = Math.Abs(point.Y);

            return point;
        }

        public static float Length(this Point point)
        {
            float x = point.X * point.X;
            float y = point.Y * point.Y;
            
            return (float)Math.Sqrt(x + y);
        }

        public static float Length(this PointF point)
        {
            var x = point.X * point.X;
            var y = point.Y * point.Y;
            
            return (float)Math.Sqrt(x + y);
        }

        public static PointF Normalize(this Point point)
        {
            var result = new PointF();
            
            var length = point.Length();
            result.X = point.X / length;
            result.Y = point.Y / length;

            return result;
        }

        public static bool NearlyEquals(this PointF point, PointF other, float tolerance = 0.0001f)
        {
            return (point - new SizeF(other)).Length() < tolerance;
        }
        
        public static int Count(this Size size)
        {
            return size.Width * size.Height;
        }

        public static Point Invert(this Point point)
        {
            return Point.Empty - new Size(point);
        }

        public static Point Relative(this Point point, Point to)
        {
            return point - new Size(to);
        }
    }
}
