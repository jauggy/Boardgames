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
        public Point WormholeCanvasLocation { get; set; }
        public double WormholeStartAngle { get; set; }
        public double WormholeEndAngle { get; set; }
        //i must start with 1.
        
        public HexSide(int i, Hex owner )
        {
            RadiansAngle = GetRadiansAngle(i);
            HasWormHole = false;
            PointDirection = GetDirection(i);
            WormholeCanvasLocation = owner.CanvasLocation.GetRelativePoint(CanvasHelper.GetHexHeight(), RadiansAngle);
            WormholeStartAngle = RadiansAngle - 0.5 * Math.PI;
            WormholeEndAngle = WormholeStartAngle + Math.PI;
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