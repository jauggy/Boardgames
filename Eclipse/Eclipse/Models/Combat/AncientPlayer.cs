using Eclipse.Models.Ships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Combat
{
    public class AncientPlayer : ICombatant
    {
        public static readonly String NAME = "Ancient";
        public String Name { get { return NAME; } }
        public AncientPlayer()
        { }

        public Ship GetShipByName(string shipName)
        {
            if(shipName==ShipNames.INTERCEPTOR)
            {
                return new AncientInterceptor();
            }
            else
            {
                return new GalacticCenter();
            }
        }


        public List<String> GetPossibleShipNames()
        {
            return new List<String>{
                ShipNames.INTERCEPTOR,
                ShipNames.GALACTIC_CENTER

            };
        }
    }
}