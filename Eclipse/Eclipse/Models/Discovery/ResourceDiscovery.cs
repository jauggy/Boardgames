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

        public ResourceDiscovery(PopulationType type, int amount)
        {
            _popType = type;
            _amount = amount;
            Html = "ResourceDiscovery.html";
        }

        public override void ExecuteDiscovery()
        {
            base.ExecuteDiscovery();
        }
    }
}