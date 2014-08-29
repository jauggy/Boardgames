using Eclipse.Models.Tech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Playerboards
{
    public class TechnologySegment
    {
        public String Name { get; set; }
        public List<Technology> Technologies { get; set; }
        public String[] Footer { get; set; }
        public TechnologySegment(String name, List<Technology>techs)
        {
            Name = name;
            Technologies = techs;
        }

        public TechnologySegment() { }
    }
}