using Eclipse.Models.Discovery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.UI
{
    public class DiscoveryUI
    {
        public DiscoveryToken DiscoveryToken { get; set; }

        public DiscoveryUI()
        {
            DiscoveryToken = new TechnologyDiscovery();
        }
    }
}