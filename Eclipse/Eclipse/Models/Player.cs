using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eclipse.Models.Hexes;
using Eclipse.Models.Unique;

namespace Eclipse.Models
{
    public class Player
    {
        public UniqueMethods UniqueMethods { get; set; }
        public String Color { get; set; }
        public PlayerBoard PlayerBoard { get; set; }
        public Player(UniqueMethods uniqueMethods, string color)
        {
            UniqueMethods = uniqueMethods;
            Color = color;
        }

        public Player():this(UniqueHelper.GetNewRandomUnique(), UniqueHelper.GetRandomColor())
        {
            
        }

        public List<Ship> GetShips()
        {
            return null;
        }
    }
}