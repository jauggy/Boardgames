using Eclipse.Models.Playerboards;
using Eclipse.Models.Tech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.UI
{
    public class UpgradeUI
    {
        //Need array to maintain order in the UI
        public ShipPart[] WorkshopShipParts { get; set; }
        public ShipPart[] AvailableShipParts { get; set; }

        public List<ShipPart> WorkshopParts { get; set; }
        public List<ShipPart> AvailableParts {get;set;}
        private ShipBlueprint OriginalPrint { get; set; }

        public String OriginalDescription { get; set; }
        public String WorkshopDescription { get; set; }

        public UpgradeUI(String shipType)
        {
            var board =  GameState.GetInstance().CurrentPlayer.PlayerBoard;
            OriginalPrint = board.GetBlueprint(shipType);
            WorkshopParts = board.GetBlueprint(shipType).ShipParts;
            AvailableParts = board.GetAvailableShipParts();

            for(var i=0;i<WorkshopShipParts.Count();i++)
            {
                WorkshopShipParts[i].ID = i;
            }
            for(var j = 0; j <AvailableShipParts.Count();j++)
            {

                AvailableShipParts[j].ID = -(j + 1);
            }

        }

        public void Swap(int leftId, int rightId)
        {
            var leftPart = WorkshopParts.First(x => x.ID == leftId);
            var rightPart = AvailableParts.First(x => x.ID == rightId);

            WorkshopParts.Add(rightPart);
            AvailableParts.Add(leftPart);
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}