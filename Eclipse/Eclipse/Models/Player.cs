using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eclipse.Models.Hexes;
using Eclipse.Models.Unique;
using System.Xml.Serialization;
using System.Web.Script.Serialization;
using Eclipse.Models.Tech;
using Eclipse.Models.Ships;
using Eclipse.Models.Playerboards;

namespace Eclipse.Models
{
    public class Player:ICombatant
    {
        [ScriptIgnore]
        public UniqueMethods UniqueMethods { get; set; }
        public String Color { get; set; }
         [ScriptIgnore]
        public PlayerBoard PlayerBoard { get; set; }
        public String Name { get; set; }
        public int PopulatesRemaining { get; set; }
        public int BuildsRemaining { get; set; }
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
            BuildsRemaining = 2;
            PlayerBoard.ResetLog();
        }

        public void SetupBoard()
        {
            UniqueMethods.SetupPlayerboard(PlayerBoard);
        }

        public List<Ship> GetShips()
        {
            return null;
        }

        public Ship GetShipByName(String shipName)
        {
            if (shipName == ShipNames.INTERCEPTOR)
            {
                return GetInterceptor();
            }
            else if (shipName == ShipNames.CRUISER)
            {
                return GetCruiser();
            }
            else if (shipName == ShipNames.DREADNOUGHT)
            {
                return GetDreadnought();
            }
            else if (shipName == ShipNames.STARBASE)
            {
                return GetStarbase();
            }
            else
                throw new NotImplementedException();
        }

        public Ship GetInterceptor()
        {
            return new Ship(()=>this.PlayerBoard.InterceptorBlueprint, this, "i");
        }

        public Ship GetCruiser()
        {
            return new Ship(()=>this.PlayerBoard.CruiserBlueprint, this, "c");
        }

        public Ship GetDreadnought()
        {
            return new Ship(()=>this.PlayerBoard.DreadnoughtBlueprint, this, "D");
        }

        public Ship GetStarbase()
        {
            return new Ship(()=>this.PlayerBoard.StarbaseBlueprint, this, "s");
        }

        public List<IBuildable> GetAvailableBuildables()
        {
            var list = new List<IBuildable>
            {
                PlayerBoard.InterceptorBlueprint,
                PlayerBoard.CruiserBlueprint, 
                PlayerBoard.DreadnoughtBlueprint
            };

           if(this.HasTechnology(TechnologyNames.STARBASE))
           {
               list.Add(PlayerBoard.StarbaseBlueprint);
           }

           return list;
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
        

        public List<String> GetPossibleShipNames()
        {
            return new List<String> 
            {
                ShipNames.INTERCEPTOR,
                ShipNames.CRUISER,
                ShipNames.DREADNOUGHT,
                ShipNames.STARBASE
            };
        }

    }
}