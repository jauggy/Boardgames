using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Hexes
{
    public static class Direction
    {
        public static List<Point> GetDirectionsAsPoints()
        {
            var list = new List<Point>();
            list.Add(new Point(1, 0)); //SE
            list.Add(new Point(0, 1)); //S
            list.Add(new Point(-1, 1));//SW
            list.Add(new Point(-1, 0)); //NW
            list.Add(new Point(0, -1)); //N
            list.Add(new Point(1, -1)); //NE
            
           
            
           


            return list;
        }


    }
}