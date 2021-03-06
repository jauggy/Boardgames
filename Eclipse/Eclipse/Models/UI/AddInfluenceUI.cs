﻿using Eclipse.Models.Discovery;
using Eclipse.Models.Hexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.UI
{
    public class AddInfluenceUI
    {
        public String HexDescription { get; set; }
        public String UpkeepDescription { get; set; }
        public String Log { get; set; }
        public Hex Hex { get; set; }
        public DiscoveryToken DiscoveryToken { get; set; }
        public AddInfluenceUI()
        {
            var hex = GameState.GetInstance().HexBoard.LastSelectedHex;
            HexDescription = hex.GetDescription();
            Log = GameState.GetLog();
            UpkeepDescription = GetUpkeepDescription();
            Hex = hex;

        }

        private String GetUpkeepDescription()
        {
            var board = GameState.GetInstance().CurrentPlayer.PlayerBoard;
            var msg = "Current Upkeep: {0}</br>Next Upkeep: {1}</br>Money Production: {2}</br>Money Storage: {3}";
            return String.Format(msg, board.GetUpkeep(), board.GetNextUpkeep(),  board.GetProduction(PopulationType.Money),board.MoneyStorage);
        }
    }
}