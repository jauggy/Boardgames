using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eclipse.Models.Hexes;
using Eclipse.Models.Unique;
using System.Xml.Serialization;
using System.Web.Script.Serialization;

namespace Eclipse.Models
{
    public class Player
    {
        public UniqueMethods UniqueMethods { get; set; }
        public String Color { get; set; }
        [ScriptIgnore]
        public PlayerBoard PlayerBoard { get; set; }
        public String Name { get; set; }
        public Player(UniqueMethods uniqueMethods, string color)
        {
            UniqueMethods = uniqueMethods;
            Color = color;
            PlayerBoard = new PlayerBoard();
            PlayerBoard.AddStartingTechs(uniqueMethods.GetStartingTechnolyNames());
            Name = FirstCharToUpper(color) + uniqueMethods.GetName();
        }

        public Player():this(UniqueHelper.GetNewRandomUnique(), UniqueHelper.GetRandomColor())
        {
            
        }

        public List<Ship> GetShips()
        {
            return null;
        }

        public string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToUpper() + String.Join("", input.Skip(1));
        }
    }
}