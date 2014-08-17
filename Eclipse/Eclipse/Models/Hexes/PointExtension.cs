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

        public static List<T> GetRandom<T>(this List<T> target, int numNeeded)
        {
            var rnd = new Random();
            return target.OrderBy(x => rnd.Next()).Take(numNeeded).ToList();

        }
    }
}