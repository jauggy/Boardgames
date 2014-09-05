using Eclipse.Models.Hexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Discovery
{
    public class CruiserDiscovery:DiscoveryToken
    {
        public CruiserDiscovery()
        {
            Name = "Cruiser Discovery";
            Html = "CruiserDiscovery.html";
        }

        public override void ExecuteDiscovery(string args)
        {
            HexBoard.GetInstance().LastSelectedHex.AddShip(GameState.GetCurrentPlayer().GetCruiser());
        }
    }
}