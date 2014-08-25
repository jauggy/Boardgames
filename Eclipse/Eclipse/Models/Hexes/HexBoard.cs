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
        private Hex _lastClickedHex;
        
        public void Setup()
        {
            var startHex = new Hex();
            startHex.AxialCoordinates = new Point(0, 0);
            var firstRing = startHex.GetRing(1).ToHex();

            var secondRing = startHex.GetRing(2).ToHex();

            Hexes = new List<Hex>();
            Hexes.Add(startHex);
           /* Hexes.AddRange(firstRing);
            Hexes.AddRange(secondRing);*/

            var startingHexes = GetStartingHexes();
            foreach(var player in GameState.GetInstance().Players)
            {
                var freeHex = startingHexes.First(x => x.PopulationSquares.Count == 0);
                player.UniqueMethods.PopulateStartingHex(freeHex);
                freeHex.Controller = player;
                freeHex.Ships.Add(new Interceptor(player));
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


            return result;
        }

        public Hex GetCenterHex()
        {
            return FindHex(new Point(0, 0));
        }

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
            _lastClickedHex = nearestHex;
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

        public Hex FindHex(Point p)
        {
            return Hexes.FirstOrDefault(x => x.AxialCoordinates.Equals(p));
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
            GetCenterHex().AddBrownPlanet(2, 1).AddPinkPlanet(2, 1).AddGrayPlanet(2).Ships.Add(new GalacticCenter());
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
    
        }
   

        public void PopulateLevel1Hex(Hex hex)
        {
            var normalPop = RandomGenerator.GetInt(new List<int>{1,3,5});
            var advancedPop = RandomGenerator.GetInt(new List<int> { 2, 5, 2 });
            hex.AddRandomPopSquare(normalPop, false);
            hex.AddRandomPopSquare(advancedPop, true);
            hex.AddRandomAncientShips();
        }

        public void PopulateLevel2Hex(Hex hex)
        {
            var normalPop = RandomGenerator.GetInt(new List<int> {2,5,3,1});
            var advancedPop = RandomGenerator.GetInt(new List<int> { 6, 3, 2});
            hex.AddRandomPopSquare(normalPop, false);
            hex.AddRandomPopSquare(advancedPop, true);
            hex.AddRandomAncientShips();
        }



        public void PopulateLevel3Hex(Hex hex)
        {
            var normalPop = RandomGenerator.GetInt(new List<int> { 2, 11, 4 });
            var advancedPop = RandomGenerator.GetInt(new List<int> { 9, 8 });
            hex.AddRandomPopSquare(normalPop, false);
            hex.AddRandomPopSquare(advancedPop, true);
            hex.AddRandomAncientShips();
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
            _lastClickedHex = hex;
            return hex.GetExploreToHexes(GameState.GetInstance().CurrentPlayer);
        }

        public Hex ExploreTo(Point point)
        {
            var hex = GetOrCreateHex(point);
            PopulateHex(hex);
            return hex;
        }

    }
}