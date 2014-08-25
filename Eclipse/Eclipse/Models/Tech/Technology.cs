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

        public Technology(String name)
        {
            Name = name;
        }

        public Technology(String name, ShipPart part)
        {
            Name = name;
            ShipPart = part;
        }

        public Technology(ShipPart part):this(part.Name, part)
        { }
    }
}