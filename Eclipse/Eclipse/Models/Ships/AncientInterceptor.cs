using Eclipse.Models.Playerboards;
using Eclipse.Models.Tech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Ships
{
    public class AncientInterceptor:Ship
    {
        public AncientInterceptor()
        {
            this.IsAncient = true;
            this.Name = ShipNames.INTERCEPTOR;
            var part = new ShipPart();
            part.Initiative = 2;
            part.Computer = 1;
            part.Hull = 1;
            part.CannonDamage = new List<int> { 1,1 };
            var print = new ShipBlueprint();
            print.AddShipPart(part);

            this._getBlueprintFunc = () => { return print; };
        }
    }
}