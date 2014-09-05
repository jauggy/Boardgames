using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eclipse.Models.Hexes;
using Eclipse.Models.Supply;
using Eclipse.Models.Unique;
using Eclipse.Models.UI;
using Eclipse.Models.Combat;

namespace Eclipse.Models
{
    public class GameState
    {
        private Box Box { get; set; }
        public List<Player> Players { get; set; }
        public HexBoard HexBoard { get; set; }
        public Player CurrentPlayer { get; set; }
        public SupplyBoard SupplyBoard { get; set; }
        private bool _hasDoneMainAction;
        public bool HasDoneMainAction
        {
            get { return _hasDoneMainAction; }
            set
            {
                if(_hasDoneMainAction!=value)
                {
                    CurrentPlayer.PlayerBoard.RemoveInfluenceDisk();
                    _hasDoneMainAction = value;
                }
            }
        }
        private int _currentPlayerIndex = -1;
        public UpgradeUI UpgradeUI { get; set; }
        public CombatSimUI CombatSimUI { get; set; }
        public BuildUI BuildUI { get; set; }
        public DiscoveryUI DiscoveryUI { get; set; }
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

        public static Player GetCurrentPlayer()
        {
            return GetInstance().CurrentPlayer;
        }

        public static String GetLog()
        {
            return GetInstance().CurrentPlayer.PlayerBoard.Log;
        }

        public void Setup()
        {
            
            Players = new List<Player>();
            AddRandomPlayer();
            AddRandomPlayer();
            AddRandomPlayer();
            AddRandomPlayer();
            AddRandomPlayer();

            SupplyBoard = new SupplyBoard();



            //Give players their starting techs
            foreach (var player in Players)
            {
                player.SetupBoard();
               // player.PlayerBoard.AddStartingTechs(player.UniqueMethods.GetStartingTechnolyNames());
            }

            //board setup after players have their boards, otherwise we can't get blueprints
            HexBoard = new HexBoard();
            HexBoard.Setup();

            NextRound();
            NextPlayer();
        }

        public void NextRound()
        {
            Players.ForEach(x => x.PreRoundSetup());
            
        }

        public void NextPlayer()
        {
            if(Players.All(x=>x.HasPassed))
            {
                NextRound();
            }
            HasDoneMainAction = false;
            _currentPlayerIndex++;
            if (_currentPlayerIndex >= Players.Count)
                _currentPlayerIndex = 0;

            CurrentPlayer = Players[_currentPlayerIndex];
            CurrentPlayer.PreturnSetup();
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

        public ICombatant GetCombatantByName(String name)
        {
            if (name.ToLower().Contains("ancient"))
            {
                return new AncientPlayer();
            }
            else
                return Players.FirstOrDefault(x => x.Name == name);
        }
       
       
    }
}