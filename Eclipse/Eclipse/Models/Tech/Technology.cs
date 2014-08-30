using Eclipse.Models.Playerboards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Tech
{
    public  class Technology
    {
        public ShipPart ShipPart { get; set; }
        public String Name { get; set; }
        public int AdjustedCost { get { return GetAdjustedCost(); } }
        public int DefaultCost { get; set; }
        public int MinCost { get; set; }
        private String _description;
        public String Description 
        { 
            get 
            {
                return  GetDescription();
            } 
            set { _description = value; }
        }

        public String CostDescription { get { return GetCostDescription(); } }
        public int SupplyCount { get; set; }
        public TechnologyType Type { get; set; }

        public Technology() { }

        public Technology(String name, int defaultCost, int minCost, TechnologyType type)
        {
            Name = name;
            Type = type;
            DefaultCost = defaultCost;
            MinCost = minCost;
        }


        public Technology(ShipPart part, int defaultCost, int minCost, TechnologyType type)
            :this(part.Name, defaultCost, minCost, type)
        {
            ShipPart = part;
            
        }

        private String GetDescription()
        {
            if (ShipPart == null)
            {
                return _description;
            }
            else
            {
                return ShipPart.GetDescription();
            }
        }

        public int GetAdjustedCost()
        {
            var discount= GameState.GetInstance().CurrentPlayer.GetTechnologyDiscount(this.Type);
            return Math.Max(MinCost, DefaultCost - discount);
        }

        public String GetCostDescription()
        {
            return String.Format("Cost: {0} <small>({1}/{2})</small>",AdjustedCost, DefaultCost,MinCost);
        }


    }
}