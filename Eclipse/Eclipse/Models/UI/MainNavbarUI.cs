using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.UI
{
    public class MainNavbarUI
    {
        public bool HasDoneMainAction { get; set; } //Do  not show Explore or influence on hexboard
        public String CurrentPlayerName { get; set; }
        public String CurrentPlayerColor { get; set; }
        public int PopulatesRemaining { get; set; }
        public bool HasPassed { get; set; }
        public String Log { get; set; }

        public MainNavbarUI()
        {
            var gameState = GameState.GetInstance();
            HasDoneMainAction = gameState.HasDoneMainAction;
            HasPassed = gameState.CurrentPlayer.HasPassed;
            CurrentPlayerName = gameState.CurrentPlayer.Name;
            CurrentPlayerColor = gameState.CurrentPlayer.Color;
            PopulatesRemaining = gameState.CurrentPlayer.PopulatesRemaining;
            Log = GameState.GetLog();
        }
    }
}