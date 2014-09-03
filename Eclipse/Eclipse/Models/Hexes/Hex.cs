using Eclipse.Models.Ships;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Eclipse.Models.Hexes
{
    public enum HexView { Military, Resource}

    public class Hex
    {
        private int _freeIndexMilitary = 0;
        private int _freeIndexResource = 0;

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
        public bool IsVisible { get; set; }
        public int Radius { get; set; }
        public List<HexSide> Sides { get; set; }
        public Player Controller { get; set; }
        public bool HasAncient { get { return Ships.Any(x => x.IsAncient); } }
        public bool HasDiscoveryToken { get; private set; }
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

            PopulationSquares = new List<PopulationSquare>();
            Ships = new List<Ship>();


        }

        public Hex(int x, int y)
            : this(new Point(x, y))
        {
            
        }

        public List<Point> ComponentCanvasLocations { get; set; }

        //begins at 0
        public Point GetFreeCanvasLocation(HexView viewType )
        {
            int i =0;
            if (viewType == HexView.Military)
            {
                i = _freeIndexMilitary;
                _freeIndexMilitary++;
            }
            else
            {
                i = _freeIndexResource;
                _freeIndexResource++;
            }

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
            ship.CanvasLocation = GetFreeCanvasLocation(HexView.Military);
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

        public List<Hex> GetNeighbourHexes()
        {
            var directions = Direction.GetDirectionsAsPoints();
            var neighbourPoints = directions.Select(x => x.AddPoint(this.AxialCoordinates));
            var neighbourHexes = HexBoard.GetInstance().GetHexes(neighbourPoints);

            return neighbourHexes;
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

        /// <summary>
        /// Get list of hexes that we have a wormhole facing to
        /// </summary>
        /// <returns></returns>
        public List<Hex> GetWormholeFacingHexes()
        {
            var directions = Sides.Where(x => x.HasWormHole).Select(x => x.PointDirection).ToList();
            var neighbourPoints = directions.Select(x => x.AddPoint(this.AxialCoordinates));
            var neighbourHexes = HexBoard.GetInstance().GetHexes(neighbourPoints);
            return neighbourHexes.ToList();
        }


        public bool HasWormHoleAtDirection(Point p)
        {
            return false;
        }

        public void RotateHexToCenter()
        {
            var iBest = 0;
            var scoreBest = 6 * 3;
            for(int i =0; i <Sides.Count;i++)
            {
                Rotate();
                var hexes = GetAccessibleHexes();
                var score = hexes.Sum(x => x.GetRingLevel());
                if (score < scoreBest)
                    iBest = i;
            }

            for(int i =0; i<=iBest;i++)
            {
                Rotate();
            }
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
                fromHex = HexBoard.GetInstance().LastExploredFromHex;
            //rotate but there must be a wormhole that connects to the from hex.
            var wormHoleDirection = fromHex.AxialCoordinates.SubtractPoint(this.AxialCoordinates);
            
            Rotate();
            int counter = 0;
            while (!Sides.Where(x => x.PointDirection.Equals(wormHoleDirection)).Any(x => x.HasWormHole))
            {
                if (counter > 6)
                    throw new Exception("Rotate was called with illegal from hex");
                Rotate();
                counter++;

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
                square.CanvasLocation = GetFreeCanvasLocation(HexView.Resource);
                PopulationSquares.Add(square);
            }
        }

        public void AddRandomPopSquare(int number, bool isAdvanced)
        {
            for(int i = 0; i<number;i++)
            {
                var square = new PopulationSquare(GetRandomPopType());
                square.IsAdvanced = isAdvanced;
                square.CanvasLocation = GetFreeCanvasLocation(HexView.Resource);
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
                var ship  = new AncientInterceptor();
               
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

        public void AddPopulation(PopulationType popType, Player player, bool isAdvanced)
        {
            
            var popSquare = PopulationSquares.FirstOrDefault(x => x.Type == popType && !x.IsOccupied && x.IsAdvanced==isAdvanced);
            if(popSquare!=null)
            {
                player.PlayerBoard.RemovePop(popType);
                popSquare.Owner = player;
            }
            player.PopulatesRemaining--;

        }

        public List<String> GetPopulatableTypes(bool ignoreController)
        {
            var player = GameState.GetInstance().CurrentPlayer;
            if (Ships.Any(x => x.Owner != player))
                return new List<String>();

            if (player == this.Controller || ignoreController)
            {
                return PopulationSquares.Where(x=>!x.IsOccupied && x.IsTechSufficient(player)).Select(x => x.ToString()).ToList();
            }
            else
            {
                return new List<String>();
            }
        }

        public bool IsInfluenceToPossible(Player currentplayer)
        {

            if (Controller != null || !IsVisible || HasAncient)
                return false;
            if(Ships.Any(x=>x.Owner!=currentplayer))
                return false;

            if (IsAdjacentToFriendlyShipOrHex(currentplayer))
                return true;

            if (HasFriendlyShip(currentplayer))
                return true;

            return false;
        }

        public bool HasFriendlyShip(Player player)
        {
            return Ships.Any(x => x.Owner == player);
        }

        private bool IsAdjacentToFriendlyShipOrHex(Player player)
        {
            var hexes = GetNeighbourHexes();
            if (hexes.Any(x => x.Controller == player || x.HasFriendlyShip(player)))
                return true;
            else
                return false;
        }

        public String GetDescription()
        {
            var array = PopulationSquares.Select(x => x.GetDescription()).OrderBy(y => y.ToString());
            return String.Join("</br>", array);
        }

        public void AddInfluence()
        {
            var player = GameState.GetInstance().CurrentPlayer;
            this.Controller = player;
            player.PlayerBoard.RemoveInfluenceDisk();
        }

        public Hex RemoveInfluence()
        {
            this.Controller = null;
            var player = GameState.GetInstance().CurrentPlayer;
            player.PlayerBoard.AddInfluenceDisk();

            PopulationSquares.ForEach(x =>
                {
                    if (x.Owner != null)
                    {
                        x.Owner.PlayerBoard.AddPopulationCube(x.Type);
                        x.Owner = null;
                    }
                });
            return this;
        }

        public void AddDiscoveryToken(int discoveryTokens)
        {
            HasDiscoveryToken = discoveryTokens > 0;
        }

        public void ConvertGreyPop(PopulationType type)
        {
            PopulationSquares.First(x => x.Type == PopulationType.Unknown && !x.IsOccupied).Type = type;
        }
        public void PopulateByString(IEnumerable<String> args)
        {
            foreach(var arg in args)
            {
                PopulateByString(arg);
            }
        }
        /// <summary>
        /// Money, Advanced Money, Unknown Money
        /// </summary>
        /// <param name="name"></param>
        public void PopulateByString(String name)
        {
            var player = GameState.GetInstance().CurrentPlayer;
            var poptype = GetPopulationType(name);
            bool isAdvanced = false;
            if(name.Contains("Advanced"))
            {
                isAdvanced=true;
            }
            if(name.Contains("Unknown"))
            {

                ConvertGreyPop(poptype);
                
            }

            AddPopulation(poptype, player, isAdvanced);
        }

        private PopulationType GetPopulationType(String s)
        {
            if (s.Contains("Money"))
                return PopulationType.Money;
            else if (s.Contains("Science"))
                return PopulationType.Science;
            else if (s.Contains("Materials"))
                return PopulationType.Materials;
            else if (s.Contains("Unknown"))
                return PopulationType.Unknown;
            else
                throw new NotImplementedException();
        }
    }
}