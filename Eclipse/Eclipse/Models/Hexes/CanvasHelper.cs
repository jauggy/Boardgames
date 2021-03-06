﻿using System;
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
            var size = GetCanvasDimensions();
            var point = new Point();
            point.X = size.Width / 2;
            point.Y = size.Height / 2;
            return point;
        }

        public static Size GetCanvasDimensions()
        {
            return new Size(1400, 1400);

        }

        public static Point HexToCanvasPoint(Hex hex)
        {
            var point = GetCenterCanvasPoint();
            var R = GetHexRadius();
            var H = GetHexHeight();
            point.X += (int) (1.5 * hex.AxialCoordinates.X *  R);
            point.Y += (int)(1 * H * hex.AxialCoordinates.X);

            point.Y += (int)(2 * H * hex.AxialCoordinates.Y);

            return point;
        }

        //This should be an event number so we can divide by 2.
        public static int GetHexRadius() 
        { 
            return 80;
        }

        public static double GetHexHeight()
        {
            return Math.Sqrt(3) / 2 * GetHexRadius();
        }

        public static double GetComponentSize()
        {
            return 10;
        }












    
    }
}