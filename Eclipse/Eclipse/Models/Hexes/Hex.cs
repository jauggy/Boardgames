using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Hexes
{
    public class Hex
    {
        public List<Ship> Ships { get; set; }
        public List<Planet> Planets { get; set; }

        private Point _axialCordinates;
        public Point AxialCoordinates
        {
            get { return _axialCordinates; }
            set { 
                _axialCordinates = value;
                Radius = CanvasHelper.GetHexRadius();
                CanvasLocation = CanvasHelper.HexToCanvasPoint(this);
            }
        }
        public Point CanvasLocation { get; private set; }
        public bool IsPlaceholder { get; set; }
        public int Radius { get; set; }
        public List<HexSide> Sides { get; set; }

        public Hex Copy()
        {
            var hex = new Hex(this.AxialCoordinates);
            hex.CanvasLocation = this.CanvasLocation;
            hex.IsPlaceholder = this.IsPlaceholder;
            hex.Ships = Ships.Select(x => x.Copy()).ToList();
            hex.Planets = Planets.Select(x => x.Copy()).ToList();
            hex.Sides = Sides.Select(x => x.Copy()).ToList();
            return hex;
        }

        public Hex()
            : this(new Point(0,0))
        {

        }

        //Base constructor
        public Hex(Point p)
        {
            AxialCoordinates = p;
        }

        public Hex(int x, int y)
            : this(new Point(x, y))
        {
            
        }

        public int GetRingLevel()
        {
            return this.AxialCoordinates.GetDistanceToCenter();
        }

        public Hex CopyAndRotate()
        {
            var result = this.Copy();
            result.Rotate();
            return result;
        }

        public void InitSides()
        {
            var list = new List<HexSide>();
            for(int i = 0; i < 6 ; i ++)
            {
                list.Add(new HexSide(i));
            }

            Sides = list;
        }

        public void AddWormHoles(int number)
        {
            var randomSides = Sides.GetRandom(number);
            randomSides.ForEach(x => x.HasWormHole = true);
        }

        public List<Hex> GetAccessibleHexes()
        {
            var directions = Sides.Where(x => x.HasWormHole).Select(x => x.PointDirection).ToList();
            var neighbourPoints = directions.Select(x => x.AddPoint(this.AxialCoordinates));
            var neighbourHexes = HexBoard.GetInstance().GetHexes(neighbourPoints);
            var list = new List<Hex>();
            for (int i = 0; i < directions.Count; i++)
            {
                var opp = directions[i].Opposite();
                var hex = neighbourHexes[i];
                if(hex.HasWormHoleAtDirection(opp))
                {
                    list.Add(hex);
                }
            }
            return list;
        }

        public bool HasWormHoleAtDirection(Point p)
        {
            return false;
        }

        public void Rotate()
        {
            var temp = Sides[0].HasWormHole;

            for(int i = 0; i<Sides.Count-1;i++)
            {
                Sides[i].HasWormHole = Sides[i + 1].HasWormHole;
            }

            Sides[Sides.Count - 1].HasWormHole = temp;
        }

        public Hex GetRelativeHex(Compass compass, int distance)
        {
            Point p = compass.ToPoint();
            return HexBoard.GetInstance().GetHex(p.AddPoint(this.AxialCoordinates));
        }

        public void Explore(Compass direction)
        {

        }

        public void SetNeighbour(Compass compass, Hex hex)
        {
        }

        private List<Point> GetNeighbourPoints(Point p)
        {
            var points = Direction.GetDirectionsAsPoints();

            return points.Select(x => AddPoint(x, p)).ToList();
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
            foreach (var pList in pointsArray) 
            { 
                if(pList!=null)
                    points.AddRange(pList); 
            }

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
                    rings[0] = new List<Point> { this.AxialCoordinates };
                else
                {
                    //get neighbours of previous set
                    var neighbours = GetNeighbourPoints(rings);
                    rings[i] = neighbours;
                }
            }

            return rings[level];

        }

        public void AddOrangePlanet(int size = 1, int advanced=0)
        {

        }

        public void AddBrownPlanet(int size = 1, int advanced=0)
        {

        }

        public void AddPinkPlanet(int size =1, int advanced=0)
        {

        }

        public void AddGrayPlanet(int size = 1, int advanced = 0)
        {

        }
    }
}