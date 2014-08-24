using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Hexes
{
    public class AngleHelper
    {
        public static double ToRadians(double degrees)
        {
            var radians = degrees * Math.PI / 180;
            return radians;
        }
    }
}