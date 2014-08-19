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
        public Hex CenterHex { get; set; }

        
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


            
        }

        public void AddStartingPlayerHex(Player player)
        {
            var hex = player.UniqueMethods.CreateStartingHex();
            Hexes.Add(hex);
            hex.AxialCoordinates = GetFreeStartingPoint();
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

        public List<Hex> GetExplorableHexVariants(Hex approachHex, Point direction)
        {
            Hex hex = null;
            
            var list = new List<Hex>();
   
            for (int i = 0; i < 6; i++)
            {
                if (hex == null)
                    hex = CreateNewHex(approachHex.AxialCoordinates.AddPoint(direction));
                else
                    hex = hex.CopyAndRotate();

                if (hex.HasWormHoleAtDirection(direction.Opposite()))
                {
                    list.Add(hex);
                }
            }

            return list;
        }

        private Hex CreateNewHex(Point p)
        {
            var hex = new Hex(p);
            return hex;
        }

        public static HexBoard GetInstance()
        {
            return null;
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