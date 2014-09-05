using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Discovery
{
    public class ResourceDiscovery:DiscoveryToken
    {
        private PopulationType _popType;
        private int _amount;
        public String CurrentStorage { get; set; }
        public String CurrentProduction { get; set; }
        public String Description { get; set; }
        public ResourceDiscovery(PopulationType type, int amount)
        {
            _popType = type;
            _amount = amount;
            Html = "ResourceDiscovery.html";
            Name = "Resource Discovery";

            CurrentStorage = GameState.GetCurrentPlayer().PlayerBoard.GetCurrentStorageDescription(type);
            CurrentProduction = GameState.GetCurrentPlayer().PlayerBoard.GetCurrentProductionDescription(type);
            Description = String.Format("+{0} {1}", amount.ToString(), type.ToString());

        }

        public override void ExecuteDiscovery(String args)
        {
            GameState.GetCurrentPlayer().PlayerBoard.AdjustStorage(_popType, _amount);
        }
    }
}