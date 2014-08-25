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
        public bool IsAncient { get; set; }
        public Player Owner { get; set; }
        public List<int> CannonDamage { get; set; }
        public int Computers { get; set; }
        public String Code { get; set; }
        public Point CanvasLocation { get; set; }

        public Ship()
        {
            CannonDamage = new List<int>();
        }

        public void AddCannon(int damage)
        {
            CannonDamage.Add(damage);
        }
      /*  public void MoveTo(Point point)
        {
            var hex = HexBoard.GetInstance().GetHex(point); //in this line we make sure we are in the same universe
            LastCoordinates = CurrentCoordinates;
            GetCurrentHex().Ships.Remove(this);
          //  CurrentCoordinates = hex;
            GetCurrentHex().Ships.Add(this);
        }*/
    }
}