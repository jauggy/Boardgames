using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Hexes
{
    public class HexSide
    {
        public Point Direction { get; set; }
        public bool HasWormHole { get; set; }

        public HexSide(Point p)
        {
            Direction = p;
            HasWormHole = false;
        }

        public HexSide Copy()
        {
            var hexSide = new HexSide(Direction);
            hexSide.HasWormHole = HasWormHole;
            return hexSide;
        }
    }
}