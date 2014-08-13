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
            Index = index;
            IsUnique = index > 0 && index < 6;

            if (index > 1 && index < 6)
            {
                WoodTokens = BoardState.GetInstance().NumberOfPlayers;
                CornTokens = BoardState.GetInstance().NumberOfPlayers;

                if (index == 2)
                {
                    CornPayoff = 4;
                }
                else if (index == 3)
                {
                    CornPayoff = 5;
                    WoodPayoff = 2;
                }
                else if (index == 4)
                {
                    CornPayoff = 7;
                    WoodPayoff = 3;
                }
                else if (index == 6)
                {
                    CornPayoff = 9;
                    WoodPayoff = 4;
                }

            }
        }

        public List<PlayerAction> GetPossibleActions()
        {
            var result = new List<PlayerAction>();
            if (Index == 0)
                return result;
            else if (Index == 1)
            {
                result.Add(GetFishingAction());
                return result;
            }
            else if (Index >= 6)
            {
                return BoardState.GetInstance().PalenqueWheel.GetUniquePlayerActions();
            }
            else
            {
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
            }

            return result;
        }

        public PlayerAction GetFishingAction()
        {

            var a = new PlayerAction((p) =>
            {
                p.AddResource(Resource.Corn, 3);
            });

            return a;
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