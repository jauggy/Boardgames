﻿using Eclipse.Models.Tech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Discovery
{
    public class TechnologyDiscovery:DiscoveryToken
    {
        public Technology[] Technologies { get; set; }
        public TechnologyDiscovery()
        {
            Name = "Technology Discovery";
            Html = "TechnologyDiscovery.html";
            Technologies = GameState.GetInstance().SupplyBoard.GetCheapestTechnologies();
        }

        public override void ExecuteDiscovery(string args)
        {
            GameState.GetCurrentPlayer().PlayerBoard.Technologies.Add(Technologies.First(x => x.Name == args));
        }
    }
}