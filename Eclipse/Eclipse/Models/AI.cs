using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models
{
    public class AI
    {
        //AI will copy board state then perform an action then evaluate the board state
        //Repeat this several times.
        //Pick the action with the highest delta in board state evaluation.

        public void SaveAndEvaluateBoardState(GameState state)
        {

        }

        public void GetBestAction()
        {

        }

        public void GetProxyWorkers()
        {

        }

        //If order doesn't matter it is a combination
       /* public List<GameState> GetPossibleGameStatesOnMove(Player player)
        {
            var list = new List<GameState>();
            
            var numShips = player.UniqueMethods.GetNumberMovableShips();
            player.GetShips().ForEach(x => x.ResetLastHex());
            for (int i = 0; i < numShips; i++)
            {
                foreach (var ship in player.GetShips())
                {
          
                    var gameStates = ship.GetAllGameStatesOnMove(true);
                    list.AddRange(gameStates);
                }
            }
            
            return null;
        }*/

    }
}