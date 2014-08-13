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

        public bool IsFishing { get; set; }
        public bool IsAnyChoice { get; set; } //the last two spots on the wheel.
        public bool IsZero { get; set; }
        public bool IsUnique { get { return !IsAnyChoice; } }

        public int WoodPayoff { get; set; }
        public int CornPayoff { get; set; }

        public List<PlayerAction> GetPossibleActions()
        {
            var result = new List<PlayerAction>();
            if (IsZero)
            {
                //nothing
            }
            else if (IsFishing)
            {
                //get three corn
                var a = new PlayerAction((p)=>
                    {
                        p.AddResource(Resource.Corn, 3);
                    });
                result.Add(a);
            }
            else if (IsAnyChoice)
            {
                //we need to get the possible actions from PalenqueWheel
                return BoardState.GetInstance().PalenqueWheel.GetUniquePlayerActions();
            }
            else
            {
                if (WoodTokens > 0)
                {
                }
                if (CornTokens > 0)
                {
                }
            }
          


            return null;
            
        }
    }
}