using Eclipse.Models.Discovery;
using Eclipse.Models.Hexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Eclipse.Models.UI
{
    public class DiscoveryUI
    {
        public DiscoveryToken DiscoveryToken { get; set; }
        public String NextHtml { get; set; }
   
        public Hex Hex { get; set; }
        public DiscoveryUI()
        {
            DiscoveryToken = Discovery.DiscoveryTokenFactory.CreateRandomDiscovery();
            if(DiscoveryToken.GetType()==typeof(Discovery.ShipPartDiscovery))
            {
                NextHtml = "UpgradeView.html";
            }

            Hex = HexBoard.GetInstance().LastSelectedHex;
        }
    }
}