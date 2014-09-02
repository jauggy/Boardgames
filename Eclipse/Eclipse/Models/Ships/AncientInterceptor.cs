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
            var print = new ShipBlueprint();
            print.AddShipPart(part);

            this._getBlueprintFunc = () => { return print; };
        }
    }
}