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

        private String _description;
        public String Description 
        { 
            get 
            {
                return  GetDescription();
            } 
            set { _description = value; }
        }

        public TechnologyType Type { get; set; }

        public Technology() { }

        public Technology(String name, int defaultCost, int minCost, TechnologyType type)
        {
            Name = name;
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
    }
}