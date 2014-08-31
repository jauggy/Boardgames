using Eclipse.Models.Hexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.UI
{
    public class DropDownMenu
    {
        public String[] MenuItems { get; set; }
        public String Heading { get; set; }
        public Hex Hex { get; set; }
    }
}