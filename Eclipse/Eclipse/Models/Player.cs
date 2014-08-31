using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eclipse.Models.Hexes;
using Eclipse.Models.Unique;
using System.Xml.Serialization;
using System.Web.Script.Serialization;
using Eclipse.Models.Tech;

namespace Eclipse.Models
{
    public class Player
    {
        [ScriptIgnore]
        public UniqueMethods UniqueMethods { get; set; }
        public String Color { get; set; }
         [ScriptIgnore]
        public PlayerBoard PlayerBoard { get; set; }
        public String Name { get; set; }
        public int PopulatesRemaining { get; set; }
        public Player(UniqueMethods uniqueMethods, string color)
        {
            UniqueMethods = uniqueMethods;
            Color = color;
            PlayerBoard = new PlayerBoard();
            PopulatesRemaining = 2;
            Name = FirstCharToUpper(color) + " " + uniqueMethods.GetName();
        }

        public Player():this(UniqueHelper.GetNewRandomUnique(), UniqueHelper.GetRandomColor())
        {
            
        }

        public void PreturnSetup()
        {
            PopulatesRemaining = 2;
            PlayerBoard.Log = "";
        }

        public void SetupBoard()
        {
            UniqueMethods.SetupPlayerboard(PlayerBoard);
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

        public int GetTechnologyDiscount(TechnologyType type)
        {
            return PlayerBoard.GetDiscount(type);
        }

        public bool HasTechnology(String techName)
        {
            return PlayerBoard.Technologies.Any(x => x.Name.Equals(techName, StringComparison.OrdinalIgnoreCase));
        }

        public bool HasAdvancedTechnology(PopulationType type)
        {
            return HasTechnology("Advanced " + type.ToString());
        }
        

    }
}