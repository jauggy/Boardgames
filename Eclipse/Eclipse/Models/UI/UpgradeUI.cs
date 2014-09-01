using Eclipse.Models.Playerboards;
using Eclipse.Models.Ships;
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
        public ShipPart[] WorkshopShipParts { get { return WorkshopParts.ToArray(); } }
        public ShipPart[] AvailableShipParts { get { return AvailableParts.ToArray(); } }

        private List<ShipPart> WorkshopParts { get; set; }
        private List<ShipPart> AvailableParts { get; set; }
        private ShipBlueprint OriginalPrint { get; set; }

        public String OriginalDescription { get { return ShipHelper.GetFullDescription(OriginalPrint); } }
        public String WorkshopDescription { get { return ShipHelper.GetFullDescription(WorkshopParts); } }
        public String ShipType { get; set; }
        public bool HasDoneMainAction { get; set; }
        public String ValidationMessage { get { return GetValidationMessage(); } }

        public UpgradeUI(String shipType)
        {
            HasDoneMainAction = GameState.GetInstance().HasDoneMainAction;
            ShipType = shipType;
            var board =  GameState.GetInstance().CurrentPlayer.PlayerBoard;
            OriginalPrint = board.GetBlueprint(shipType);
            var list = new List<ShipPart>();
             list.AddRange(board.GetBlueprint(shipType).ShipParts);

            var emptySpaces =   OriginalPrint.Size - list.Where(x => !x.IsBonus).Count();

            for (int i = 0; i < emptySpaces; i++ )
            {
                list.Add(new EmptyShipPart());
            }

                WorkshopParts = list;
            AvailableParts = board.GetAvailableShipParts();

            for (var i = 0; i < WorkshopParts.Count(); i++)
            {
                WorkshopParts[i].ID = i+1;
                if (WorkshopParts[i].IsBonus)
                    WorkshopParts[i].ID = 0;
            }
            for (var j = 0; j < AvailableParts.Count(); j++)
            {

                AvailableParts[j].ID = -(j + 1);
            }

        }

        public void ExecuteUpgrade()
        {
            OriginalPrint.ShipParts = WorkshopParts.Select(x=>x.Copy()).ToList();
        }

        public String GetValidationMessage()
        {
            var bp = new ShipBlueprint();
            bp.ShipParts = WorkshopParts.ToList();

            if (bp.EnergyRequirement > bp.EnergySource)
                return "You need more Energy Sources to meet Energy Requirements";

            else if (WorkshopParts.Where(x => x.ID < 0).Count() > 2)
                return "You may only swap in two available ship parts";

            else return "";
        }

        public void Swap(int workshopId, int availableId)
        {

            try { 
                var workshopIndex = WorkshopParts.FindIndex(x => x.ID == workshopId);
                var availableIndex = AvailableParts.FindIndex(x => x.ID == availableId);
                var leftPart = WorkshopParts[workshopIndex];
                var rightPart = AvailableParts[availableIndex];

                WorkshopParts.Remove(leftPart);
                if (!rightPart.IsBasic || rightPart.ID > 0)
                    AvailableParts.Remove(rightPart);

                WorkshopParts.Insert(workshopIndex,rightPart);

                if (!leftPart.IsBasic || leftPart.ID >= 0)
                    AvailableParts.Insert(availableIndex, leftPart);
                else
                {
                }

            }
            catch (Exception e)
            {
              //  throw new NotImplementedException();
            }

        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}