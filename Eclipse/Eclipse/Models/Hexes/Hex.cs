using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Hexes
{
    public class Hex
    {
        public List<Point> WormholeLocations { get; set; }
        public Point PointLocation { get; set; }
        public Dictionary<Compass, Hex> Neighbours { get; set; }




        public void Explore(Compass direction)
        {

        }

        public void SetNeighbour(Compass compass, Hex hex)
        {
        }

        public List<Compass> GetExplorableDirections()
        {
            var result = Neighbours.Where(x => x.Value == null).Select(x => x.Key).ToList();
            return result;
        }

        public List<Point> GetNeighbourPoints()
        {
            /*neighbors = [
   [+1,  0], [+1, -1], [ 0, -1],
   [-1,  0], [-1, +1], [ 0, +1]*/
            var list = new List<Point>();
            list.Add(new Point(1, 0));
            list.Add(new Point(1, -1));
            list.Add(new Point(0, -1));
            list.Add(new Point(-1, 0));
            list.Add(new Point(-1, 1));
            list.Add(new Point(0, 1));


            return list;
        }


    }
}