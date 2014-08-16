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
        public bool IsPlaceholder { get; set; }

        public Hex()
        {

        }



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

        private List<Point> GetNeighbourPoints(Point p)
        {
            var points = GetNeighbourPoints();

            points.Select(x => AddPoint(x, p));

            return points;
        }

        private List<Point> GetNeighbourPoints(IEnumerable<Point> points)
        {
            var multipleLists = points.Select(x => GetNeighbourPoints(x));
            var result = new List<Point>();

            foreach (var list in multipleLists) { result.AddRange(list); }

            var filteredResult = result.Except(points);

            return filteredResult.Distinct().ToList();
        }

        private List<Point> GetNeighbourPoints(IEnumerable<Point>[] pointsArray)
        {
            var points = new List<Point>();
            foreach (var pList in pointsArray) { points.AddRange(pList); }

            var multipleLists = points.Select(x => GetNeighbourPoints(x));
            var result = new List<Point>();

            foreach (var list in multipleLists) { result.AddRange(list); }

            var filteredResult = result.Except(points);

            return filteredResult.Distinct().ToList();
        }

        private Point AddPoint(Point p1, Point p2)
        {
            var result = new Point();
            result.X = p1.X + p2.X;
            result.Y = p1.Y + p2.Y;
            return result;
        }

        //If level is 1 then return neighbour points
        //If level is 2 then return the ring outside of level 1 ring
        public List<Point> GetRing(int level)
        {
            List<Point>[] rings = new List<Point>[level+1];

            for (int i = 0; i <= level; i++)
            {
                if (i == 0)
                    rings[0] = new List<Point> { this.PointLocation };
                else
                {
                    //get neighbours of previous set
                    var neighbours = GetNeighbourPoints(rings);
                    rings[i] = neighbours;
                }
            }

            return rings[level];

        }
    }
}