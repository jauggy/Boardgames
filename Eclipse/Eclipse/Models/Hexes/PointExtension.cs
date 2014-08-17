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
            var rnd = new Random();
            return target.OrderBy(x => rnd.Next()).Take(numNeeded).ToList();

        }
    }
}