using Eclipse.Models.Tech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Discovery
{
    public class ShipPartDiscovery:DiscoveryToken
    {
        public ShipPart ShipPart {get;set;}

        public ShipPartDiscovery(ShipPart part)
        {
            Name = "Ship Part Discovery";
            part.IsAncient = true;
            ShipPart = part;
            Html = "ShipPartDiscovery.html";
            
        }

        public override void ExecuteDiscovery(string args)
        {
            GameState.GetCurrentPlayer().PlayerBoard.AncientShipParts.Add(ShipPart);
        }
    }
}