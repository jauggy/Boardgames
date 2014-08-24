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
        public List<Player> Players { get; set; }
        public HexBoard HexBoard { get; set; }
        public Player CurrentPlayer { get; set; }
        public static GameState GetInstance()
        {
            if (HttpContext.Current.Session["GameState"] == null)
            {
                var gs = new GameState();
                HttpContext.Current.Session["GameState"] = gs;
                gs.Setup();
            }

            return (GameState)HttpContext.Current.Session["GameState"];
        }

        private GameState()
        {

        }

        public void Setup()
        {

            Players = new List<Player>();
            AddRandomPlayer();
            AddRandomPlayer();
            HexBoard = new HexBoard();
            HexBoard.Setup();
            CurrentPlayer = Players[0];
        }

        //I think args should be we copy only hexboard
        public static GameState SetCopyAsInstance()
        {
            //When copying, the hexboard and ships are copied. The cubes need to be copied... The players need to be copied>......
            //Minimize references to classes????
            return null;
        }

        public static void RevertInstanceToOriginal()
        {

        }


        public void AddRandomPlayer()
        {
            var player = new Player();
            Players.Add(player);
            //HexBoard.GetInstance().AddStartingPlayerHex(player);
        }

        public int NumberPlayers { get { return Players.Count; } }

        public GameState Copy()
        {
            return null;
        }

        public Ship GetShips(Ship ship)
        {
            return null;
        }
       
    }
}