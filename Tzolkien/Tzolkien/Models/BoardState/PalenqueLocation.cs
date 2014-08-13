using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tzolkien.Models.BoardState
{
    public class PalenqueLocation
    {
        public int WoodTokens { get; set; }
        public int CornTokens { get; set; }

        public bool IsUnique { get; set; }

        public int WoodPayoff { get; set; }
        public int CornPayoff { get; set; }
        public int Index { get; set; }

        public void PalenqueLocation(int index)
        {
            PossibleActions = new List<PlayerAction>();
            if (index == 0)
            {
            }
            else if (index == 1)
            {

            }
            else if (index > 1)
            {
                WoodTokens = BoardState.GetInstance().NumberOfPlayers;
                CornTokens = BoardState.GetInstance().NumberOfPlayers;


            }
        }

        public List<PlayerAction> GetPossibleActions()
        {
            var result = new List<PlayerAction>();
            //if index between certain range
            if (WoodTokens > 0)
            {
                result.Add(GetWoodTokenAction());
            }

            if (WoodTokens == CornTokens && CornTokens > 0)
            {
                result.Add(GetCornTokenBurnAction());
            }
            else if (CornTokens > 0)
            {
                result.Add(GetCornTokenAction());
            }

            return result;
        }

        public PlayerAction GetCornTokenAction()
        {

            var a= new PlayerAction((p) =>
                {
                    p.AddResource(Resource.Corn, CornPayoff);
                    CornTokens--;
                });

            return a;
        }

        public PlayerAction GetWoodTokenAction()
        {

            var a = new PlayerAction((p) =>
            {
                p.AddResource(Resource.Wood, WoodPayoff);
                WoodTokens--;
            });

            return a;
        }


        public PlayerAction GetCornTokenBurnAction()
        {

            var a = new PlayerAction((p) =>
            {
                p.AddResource(Resource.Corn, CornPayoff);
                CornTokens--;
            });
            a.AddAngerGodsSubAction();

            return a;
        }

        public List<PlayerAction> PossibleActions { get; set; }


    }
}