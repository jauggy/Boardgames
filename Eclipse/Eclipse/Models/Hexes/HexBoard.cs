using Eclipse.Models.Ships;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Hexes
{
    public class HexBoard
    {
        public List<Hex> Hexes { get; set; }

        public  Hex LastExploredFromHex { get; private set; }
        public Hex LastSelectedHex { get; private set;}
        public Ship LastSelectedShip { get; set; }
        public void Setup()
        {
            var startHex = new Hex();
            startHex.AxialCoordinates = new Point(0, 0);
            
            Hexes = new List<Hex>();
            Hexes.Add(startHex);
           /* Hexes.AddRange(firstRing);
            Hexes.AddRange(secondRing);*/

            var startingHexes = GetStartingHexes();
            foreach(var player in GameState.GetInstance().Players)
            {
                var freeHex = startingHexes.First(x => x.PopulationSquares.Count == 0);
                player.UniqueMethods.PopulateStartingHex(freeHex);
                freeHex.AddInfluence(player);
                freeHex.AddShip(player.GetInterceptor());
                freeHex.AddWormHoles(4);
                freeHex.AddPopulation(PopulationType.Materials, player, false);
                freeHex.AddPopulation(PopulationType.Money, player, false);
                freeHex.AddPopulation(PopulationType.Science, player, false);
                freeHex.RotateHexToCenter();
            }


            PopulateCenterHex();
            
        }

        public List<Hex> GetStartingHexes()
        {
            var center = GetCenterHex();
            var result = new List<Hex>();
            var allCompass = Direction.GetCompassSix();
            var numPlayers = GameState.GetInstance().NumberPlayers;
            if(numPlayers < 6)
            {
                allCompass.Remove(Compass.S);
            }
            if(numPlayers < 5 )
            {
                allCompass.Remove(Compass.N);
            }
            if(numPlayers==3)
            {

                allCompass.Clear();
                allCompass.Add(Compass.SE);
                allCompass.Add(Compass.SW);
                allCompass.Add(Compass.N);
            }
            if(numPlayers==2)
            {
                allCompass.Clear();
                allCompass.Add(Compass.N);
                allCompass.Add(Compass.S);
            }

            foreach(var compass in allCompass)
            {
                result.Add(center.GetRelativeHex(compass, 2));
            }

            result.ForEach(x => x.IsVisible = true);
            GetCenterHex().IsVisible = true;
            return result;
        }

        public Hex GetCenterHex()
        {
            return FindHex(new Point(0, 0));
        }

        //Deprecated - used when mouse click canvas
        public Hex GetNearestHexbyCanvasLocation(int x, int y)
        {
            var point = new Point(x, y);
            Hex nearestHex = null;
            double shortestDistance = 0;
            foreach (var hex in Hexes)
            {
                var dist = hex.CanvasLocation.GetCanvasDistance(point);
                if(dist <= hex.Radius)
                {
                    if(nearestHex==null || shortestDistance > dist )
                    {
                        shortestDistance = dist;
                        nearestHex = hex;
                    }
                }
            }
            LastSelectedHex = nearestHex;
            return nearestHex;

           
        }


        private Point GetFreeStartingPoint()
        {
            var list = GetStartingPoints();
            foreach (var point in list)
            {
                if (!Hexes.Any(x => x.AxialCoordinates.Equals(point)))
                {
                    return point;
                   
                }

            }

            throw new NotImplementedException();
        }

        public List<Hex> GetMoveFromHexes()
        {
            
            var hexes = Hexes.Where(x => x.HasFriendlyShip(GameState.GetCurrentPlayer()) == true).ToList();
            return hexes;
        }

        public List<Hex> GetMoveToHexes(int x, int y, String shipName)
        {
            var hex = FindHex(x, y, true);
            LastSelectedShip = hex.Ships.First(q => q.Name == shipName);
            return hex.GetAccessibleHexes();
        }

        public Hex MoveTo(int x, int y)
        {
            LastSelectedHex.Ships.Remove(LastSelectedShip);

            var hex = FindHex(x, y, false);
            hex.Ships.Add(LastSelectedShip);

            return hex;

        }

        private List<Point> GetStartingPoints()
        {
            var numPlayers = GameState.GetInstance().NumberPlayers;
            if (numPlayers == 1)
            {
            }

            return null;
        }

        private Hex CreateNewHex(Point p)
        {
            var hex = new Hex(p);
            return hex;
        }

        public static HexBoard GetInstance()
        {
            return GameState.GetInstance().HexBoard;
        }

        public List<Hex> GetHexes(IEnumerable<Point> p)
        {
            return p.Select(x => GetOrCreateHex(x)).ToList();
        }

        public Hex FindHex(int x, int y, bool setAsSelected = false)
        {
            return FindHex(new Point(x, y), setAsSelected);
        }

        /// <summary>
        /// Use Find hex(x,y,setasselected) instead
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="setAsSelected"></param>
        /// <returns></returns>
        [Obsolete]
        public Hex FindHex(int x, int y)
        {
            return FindHex(new Point(x, y), false);
        }
        public Hex FindHex(Point p, bool setAsSelected = false)

        {

            var hex= Hexes.FirstOrDefault(x => x.AxialCoordinates.Equals(p));
            if (setAsSelected)
                LastSelectedHex = hex;
            return hex;
        }

        public Hex GetOrCreateHex(Point p)
        {
            var result = FindHex(p);
            if(result==null)
            {
                result = new Hex(p);
                Hexes.Add(result);
            }
            return result;
        }


        private void PopulateCenterHex()
        {
            GetCenterHex().AddBrownPlanet(2, 1).AddPinkPlanet(2, 1).AddGrayPlanet(2).AddShip(new GalacticCenter());
            GetCenterHex().AddWormHoles(6);
        }

        public void PopulateHex(Hex hex)
        {
            var ringLevel = hex.GetRingLevel();
            if(ringLevel==1)
            {
                PopulateLevel1Hex(hex);
            }
            else if (ringLevel == 2)
            {
                PopulateLevel2Hex(hex);
            }
            else
            {
                PopulateLevel3Hex(hex);
            }
            hex.AddRandomAncientShips();
            hex.AddRandomWormholes();
    
        }
   

        public void PopulateLevel1Hex(Hex hex)
        {
            var normalPop = RandomGenerator.GetInt(new List<int>{1,3,5});
            var advancedPop = RandomGenerator.GetInt(new List<int> { 2, 5, 2 });
            hex.AddRandomPopSquare(normalPop, false);
            hex.AddRandomPopSquare(advancedPop, true);
            var discoveryTokens = RandomGenerator.GetInt(new List<int> {4,4 });
            hex.AddDiscoveryToken(discoveryTokens);
        }

        public void PopulateLevel2Hex(Hex hex)
        {
            var normalPop = RandomGenerator.GetInt(new List<int> {2,5,3,1});
            var advancedPop = RandomGenerator.GetInt(new List<int> { 6, 3, 2});
            hex.AddRandomPopSquare(normalPop, false);
            hex.AddRandomPopSquare(advancedPop, true);
            var discoveryTokens = RandomGenerator.GetInt(new List<int> { 5, 6 });
            hex.AddDiscoveryToken(discoveryTokens);
        }



        public void PopulateLevel3Hex(Hex hex)
        {
            var normalPop = RandomGenerator.GetInt(new List<int> { 2, 11, 4 });
            var advancedPop = RandomGenerator.GetInt(new List<int> { 9, 8 });
            hex.AddRandomPopSquare(normalPop, false);
            hex.AddRandomPopSquare(advancedPop, true);
            var discoveryTokens = RandomGenerator.GetInt(new List<int> { 8, 9 });
            hex.AddDiscoveryToken(discoveryTokens);
        }

        public List<Hex> GetExploreFromHexes()
        {
            var newList = Hexes.ToList();
            var currentPlayer = GameState.GetInstance().CurrentPlayer;
            var result =  newList.Where(x => x.IsExploreFromHex(currentPlayer)).ToList();
            return result;
        }

        public List<Hex> GetExploreToHexes(Hex hex)
        {
            LastSelectedHex = hex;
            LastExploredFromHex = hex;
            return hex.GetExploreToHexes(GameState.GetInstance().CurrentPlayer);
        }

        public Hex ExploreTo( Point point)
        {
            var hex = GetOrCreateHex(point);
            LastSelectedHex = hex;
            PopulateHex(hex);
            hex.Rotate(LastExploredFromHex);
            hex.IsVisible = true;
            return hex;
        }


    }
}