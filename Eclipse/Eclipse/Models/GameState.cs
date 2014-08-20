using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eclipse.Models.Hexes;

namespace Eclipse.Models
{
    public class GameState
    {
        private Box Box { get; set; }
        public List<Player> CurrentPlayers { get; set; }

        public static GameState GetInstance()
        {
            var gameState = HttpContext.Current.Session["GameState"];
            if (gameState == null)
                HttpContext.Current.Session["GameState"] = new GameState();

            return (GameState)gameState;
        }

        private GameState()
        {
            CurrentPlayers = new List<Player>();
        }

        public void Init(int numPlayers)
        {
            NumberPlayers = numPlayers;
            for (int i = 0; i < numPlayers; i++)
            {
                AddRandomPlayer();
            }
        }

        public void AddRandomPlayer()
        {
            var player = new Player();
            CurrentPlayers.Add(player);
            HexBoard.GetInstance().AddStartingPlayerHex(player);
        }

        public int NumberPlayers { get; set; }

        public void CopyBoardState()
        {

        }
       
    }
}