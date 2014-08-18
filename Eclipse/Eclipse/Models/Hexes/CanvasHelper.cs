using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Hexes
{
    public static class CanvasHelper
    {
        //0,0 is center of board. To go in 1,0 direction move right 1.5R then move down 1R where R is half the width of the hexagon
        //To move 0,1 then move down 2R
        //Each point can be expressed as A*1,0 + B*0,1

        public static Point GetCenterCanvasPoint()
        {
            throw new NotImplementedException();
        }

        public static Point HexToCanvasPoint(Hex hex)
        {
            var point = GetCenterCanvasPoint();
            var R = GetHexRadius();
            point.X += (int) 1.5 * hex.AxialCoordinates.X *  R;
            point.Y += (int)1 * R * hex.AxialCoordinates.X;

            point.Y += (int)2 * R * hex.AxialCoordinates.Y;

            return point;
        }

        //This should be an event number so we can divide by 2.
        public static int GetHexRadius() 
        { 
            return 80;
        }













    
    }
}