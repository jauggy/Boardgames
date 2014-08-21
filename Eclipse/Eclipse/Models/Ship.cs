using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using Eclipse.Models.Hexes;

namespace Eclipse.Models
{
    public class Ship
    {
        public int Id { get; set; }
        public Point CurrentCoordinates { get; set; }
        private Point LastCoordinates { get; set; }
        public void ResetLastHex()
        {
            LastCoordinates = null;
        }

        public Hex GetCurrentHex()
        {
            return null;
        }

        public List<GameState> GetAllGameStatesOnMove(bool avoidLastHex)
        {
            var list = new List<GameState>();
           
            var possible = this.GetCurrentHex().GetAccessibleHexes();
            int i = 0;
            foreach (var hex in possible)
            {
                var copy = GameState.SetCopyAsInstance();
                var copyShip = copy.GetShip(this);
                copyShip.MoveTo(hex.AxialCoordinates);
                list.Add(copy);
                i++;

            }

            GameState.RevertInstanceToOriginal();
            //Step1 copy the game state
            //Move the ship in the game state...
            return null;
        }

        public void MoveTo(Point point)
        {
            var hex = HexBoard.GetInstance().GetHex(point); //in this line we make sure we are in the same universe
            LastCoordinates = CurrentCoordinates;
            GetCurrentHex().Ships.Remove(this);
            CurrentCoordinates = hex;
            GetCurrentHex().Ships.Add(this);
        }
    }
}