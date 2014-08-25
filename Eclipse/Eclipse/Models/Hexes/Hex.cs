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
        public List<PopulationSquare> PopulationSquares { get; set; }

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
        public bool IsVisible { get { return PopulationSquares.Count > 0; } }
        public int Radius { get; set; }
        public List<HexSide> Sides { get; set; }
        public Player Controller { get; set; }
        public bool HasAncient { get { return Ships.Any(x => x.IsAncient); } }
        public Hex()
            : this(new Point(0,0))
        {

        }

        //Base constructor
        public Hex(Point p)
        { 
            AxialCoordinates = p;
            InitSides();
            //AddWormHoles();
            ComponentCanvasLocations = new List<Point>();
            PopulationSquares = new List<PopulationSquare>();
            Ships = new List<Ship>();
            for (int i = 0; i < 24; i++)
            {
                ComponentCanvasLocations.Add(GetFreeCanvasLocation(i));
            }

        }

        public Hex(int x, int y)
            : this(new Point(x, y))
        {
            
        }

        public List<Point> ComponentCanvasLocations { get; set; }

        //begins at 0
        public Point GetFreeCanvasLocation(int i )
        {
            var hexHeight = CanvasHelper.GetHexHeight();
            var compSize = CanvasHelper.GetComponentSize();
            var dist = 2 / 3.0 * hexHeight;
            var angle = i * 30;
            if (i <= 6)
            {
                dist = 1 / 3.0 * hexHeight;
                angle = i * 60 - 90;
            }

            return this.CanvasLocation.GetRelativePoint(dist,  AngleHelper.ToRadians(angle));
        }

        public void AddShip(Ship ship)
        {
            ship.CanvasLocation = GetFreeCanvasLocation(Ships.Count);
            Ships.Add(ship);
        }

        public int GetRingLevel()
        {
            return this.AxialCoordinates.GetDistanceToCenter();
        }

        public void InitSides()
        {
            var list = new List<HexSide>();
            for(int i = 0; i < 6 ; i ++)
            {
                list.Add(new HexSide(i, this));
            }

            Sides = list;
        }

        public void AddRandomWormholes()
        {
            var num = RandomGenerator.GetInt(new List<int> { 0, 0, 11, 14, 9, 3 });
            AddWormHoles(num);
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

        private void Rotate()
        {
            var temp = Sides[0].HasWormHole;

            for(int i = 0; i<Sides.Count-1;i++)
            {
                Sides[i].HasWormHole = Sides[i + 1].HasWormHole;
            }

            Sides[Sides.Count - 1].HasWormHole = temp;
        }

        public void Rotate(Hex fromHex)
        {
            if (fromHex == null)
                fromHex = HexBoard.GetInstance().LastSelectedHex;
            //rotate but there must be a wormhole that connects to the from hex.
            var wormHoleDirection = fromHex.AxialCoordinates.SubtractPoint(this.AxialCoordinates);
            
            Rotate();
            while (!Sides.Where(x => x.PointDirection.Equals(wormHoleDirection)).Any(x => x.HasWormHole))
            {
                Rotate();
            }

        }

        public Hex GetRelativeHex(Compass compass, int distance)
        {
            Point p = compass.ToPoint();
            p.X *= distance;
            p.Y *= distance;
            var result= HexBoard.GetInstance().GetOrCreateHex(p.AddPoint(this.AxialCoordinates));
            return result;
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

        public Hex AddOrangePlanet(int size = 1, int advanced=0)
        {
            AddPlanet(size, advanced, PopulationType.Money);
            return this;
        }

        public Hex AddBrownPlanet(int size = 1, int advanced = 0)
        {
            AddPlanet(size, advanced, PopulationType.Materials);
            return this;
        }

        public Hex AddPinkPlanet(int size = 1, int advanced = 0)
        {
            AddPlanet(size, advanced, PopulationType.Science);
            return this;
        }

        public Hex AddGrayPlanet(int size = 1, int advanced = 0)
        {
            AddPlanet(size, advanced, PopulationType.Unknown);
            return this;
        }

        public void AddPlanet(int size, int advanced, PopulationType type)
        {
            for (int i = 0; i < size; i++)
            {
                var square = new PopulationSquare(type);

                square.IsAdvanced = i < advanced;
                square.CanvasLocation = GetFreeCanvasLocation(PopulationSquares.Count);
                PopulationSquares.Add(square);
            }
        }

        public void AddRandomPopSquare(int number, bool isAdvanced)
        {
            for(int i = 0; i<number;i++)
            {
                var square = new PopulationSquare(GetRandomPopType());
                square.IsAdvanced = isAdvanced;
                square.CanvasLocation = GetFreeCanvasLocation(PopulationSquares.Count);
                PopulationSquares.Add(square);
            }

        }

        private PopulationType GetRandomPopType()
        {
            Array values = Enum.GetValues(typeof(PopulationType));
            var random = RandomGenerator.GetInt(new List<int> { 15, 13, 11, 4 });
            return (PopulationType)values.GetValue(random);
        }

        public void AddAncientShip(int num)
        {
            for(int i = 0;i<num;i++)
            {
                var ship  = new Ship();
                ship.IsAncient = true;
                this.AddShip(ship);
            }
        }

        public void AddRandomAncientShips()
        {
            var num = RandomGenerator.GetInt(new List<int> {12,5,2 });
            AddAncientShip(num);
        }

        public List<Hex> GetExploreToHexes(Player player)
        {
            var directions = Sides.Where(x => x.HasWormHole).Select(x => x.PointDirection).ToList();
            var neighbourPoints = directions.Select(x => x.AddPoint(this.AxialCoordinates));
            var neighbourHexes = HexBoard.GetInstance().GetHexes(neighbourPoints);
            var result =  neighbourHexes.Where(x => !x.IsVisible).ToList();
            return result;

        }

        public bool IsExploreFromHex(Player player)
        {
            if (this.IsVisible && Ships.Any(x => x.Owner == player))
            {
                return GetExploreToHexes(player).Count > 0;
            }

            return false;
        }
    }
}