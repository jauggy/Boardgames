using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Hexes
{
    public static class PointExtension
    {
        public static Hex ToHex(this Point p)
        {
            return new Hex(p.X, p.Y);
        }

        public static List<Hex> ToHex(this IEnumerable<Point> p)
        {
            return p.Select(x => x.ToHex()).ToList();
        }


        public static int GetAxialDistance(this Point h1, Point h2)
        {
            var x1 = h1.X;
            var y1 = h1.Y;
            var x2 = h2.X;
            var y2 = h2.Y;
            var z1 = -(x1 + y1);
            var z2 = -(x2 + y2);
            return (Math.Abs(x1 - x2) + Math.Abs(y1 - y2) + Math.Abs(z1 - z2)) / 2;
        }

        public static double GetCanvasDistance(this Point h1, Point h2)
        {
            var x1 = h1.X;
            var x2 = h2.X;
            var y1 = h1.Y;
            var y2 = h2.Y;
            return Math.Sqrt((Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2)));
        }

        public static int GetDistanceToCenter(this Point h1)
        {
            return GetAxialDistance(h1, new Point(0, 0));
        }

        public static Point Opposite(this Point p)
        {
            var point = new Point();
            point.X = p.X * -1;
            point.Y = p.Y * -1;
            return point;
        }

        public static Point AddPoint( this Point p, Point otherPoint)
        {
            var point = new Point();
            point.X = p.X + otherPoint.X;
            point.Y = p.Y + otherPoint.Y;

            return point;
        }

        public static List<T> GetRandom<T>(this List<T> target, int numNeeded)
        {
            var rnd = RandomGenerator.GetRandom();
            return target.OrderBy(x => rnd.Next()).Take(numNeeded).ToList();

        }

        public static Point ToPoint(this Compass compass)
        {
            var point = new Point(0, 0);

            if (compass == Compass.N)
            {
                point = new Point(0, -1);
            }
            else if (compass == Compass.S)
            {
                point = new Point(0, 1);
            }
            else if (compass == Compass.NE)
            {
                point = new Point(1, -1);
            }
            else if (compass == Compass.SE)
            {
                point = new Point(1, 0);
            }
            else if (compass == Compass.SW)
            {
                point = new Point(-1, 1);
            }
            else if (compass == Compass.NW)
            {
                point = new Point(-1, 0);
            }

            return point;
        }

        /// <summary>
        /// Returns relative point
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="angle">In radians</param>
        /// <returns></returns>
        public static Point GetRelativePoint(this Point start, double distance, double angle)
        {
            var result = new Point();
            result.X = (int)(start.X + distance * Math.Cos(angle));
            result.Y = (int)(start.Y + distance * Math.Sin(angle));
            return result;
        }
    }
}