using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Hexes
{
    public class HexSide
    {
        public double RadiansAngle { get; set; }
        public bool HasWormHole { get; set; }
        public Point PointDirection { get; set; }

        //i must start with 1.
        public HexSide(int i )
        {
            RadiansAngle = GetRadiansAngle(i);
            HasWormHole = false;
            PointDirection = GetDirection(i);
        }

        public Point GetDirection(int i)
        {
            var directions = Direction.GetDirectionsAsPoints();
            return directions[i];
        }

        public double GetRadiansAngle(int i)
        {
            double degrees = i * 60 + 30;
            var radians = degrees * Math.PI / 180;
            return radians;
        }

        public HexSide Copy()
        {
            throw new NotImplementedException();
        }
    }
}