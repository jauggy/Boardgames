using Eclipse.Models.Tech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.UI
{
    public class PlayerBoardUI
    {
        public List<PlayerUI> Players { get; private set; }
        public PlayerUI CurrentPlayer { get; private set; } 

        public PlayerBoardUI()
        {
            Players = GameState.GetInstance().Players.Select(x => new PlayerUI(x)).ToList();
            var currentPlayer = GameState.GetInstance().CurrentPlayer;

            CurrentPlayer = Players.First(x => x.Name == currentPlayer.Name);
        }
     
    }
}