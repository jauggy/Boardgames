using Eclipse.Models.Tech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.UI
{
    public class TechnologySection
    {
        public List<Technology> Technologies { get; set; }
        public String Name { get; set; }
        public List<String> Footer { get; set; }
        public String PlayerName { get; set; }
        public TechnologySection(String name, Player player)
        {
            PlayerName = player.Name;
            Name = name;
            Technologies = new List<Technology>();
            Footer = new List<String>();
        }
    }
}