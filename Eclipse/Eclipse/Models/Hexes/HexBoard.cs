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

        
        public void Setup()
        {
            var startHex = new Hex();
            startHex.AxialCoordinates = new Point(0, 0);
            var firstRing = startHex.GetRing(1).ToHex();

            var secondRing = startHex.GetRing(2).ToHex();

            Hexes = new List<Hex>();
            Hexes.Add(startHex);
            Hexes.AddRange(firstRing);
            Hexes.AddRange(secondRing);

            var startingHexes = GetStartingHexes();
            foreach(var player in GameState.GetInstance().CurrentPlayers)
            {
                var freeHex = startingHexes.First(x => x.PopulationSquares.Count == 0);
                player.UniqueMethods.PopulateStartingHex(freeHex);
            }
            
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
            return GetHex(new Point(0, 0));
        }

        public Hex GetNearestHexbyCanvasLocation(int x, int y)
        {
            var point = new Point(x, y);
            Hex nearestHex = null;
            double shortestDistance = 0;
            foreach (var hex in Hexes)
            {
                var dist = hex.CanvasLocation.GetDistance(point);
                if(dist < hex.Radius)
                {
                    if(nearestHex==null || shortestDistance > dist )
                    {
                        shortestDistance = dist;
                        nearestHex = hex;
                    }
                }
            }

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
            return null;
        }

        public Hex GetHex(Point p)
        {
            return Hexes.FirstOrDefault(x => x.AxialCoordinates.Equals(p));
        }


        private void PopulateHex(Hex hex)
        {
            //Populates hex based on the level

        }

        private void PopulateCenterHex(Hex hex)
        {
        }



    }
}