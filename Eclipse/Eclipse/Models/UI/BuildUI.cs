using Eclipse.Models.Hexes;
using Eclipse.Models.Playerboards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.UI
{
    public class BuildUI
    {
        public IBuildable[] Buildables { get; set; }
        public String StorageMessage { get; set; }
        public String ProductionMessage { get; set; }//Materials Storage: 5 //Materials Production: 1
        private Hex _hex;
        private Player _currentPlayer;
        
        public BuildUI(int x, int y)
        {
            _hex = HexBoard.GetInstance().FindHex(x, y,true);
            _currentPlayer = GameState.GetCurrentPlayer();
            Buildables = _currentPlayer.GetAvailableBuildables().ToArray();
            SetMessages();
        }

        public void ActionOnBuild(String buildableName)
        {
            var build = Buildables.Where(x => x.Name == buildableName).First();
            build.ActionOnBuild(_hex);
            SetMessages();
        }

        private void SetMessages()
        {
            StorageMessage = String.Format("Materials Storage: {0}", _currentPlayer.PlayerBoard.MaterialsStorage);
            ProductionMessage = String.Format("Materials Production: {0}", _currentPlayer.PlayerBoard.GetProduction(PopulationType.Materials));
        }
    }
}