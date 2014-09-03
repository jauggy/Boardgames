using Eclipse.Models.Hexes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Eclipse.Models.UI
{
    public class TempMenu
    {
        public String[] MenuItems { get; set; }
        public Hex Hex { get; set; }

        public TempMenu(List<String> items, Hex hex)
        {
            Hex = hex;
            MenuItems = items.ToArray();
        }

        public TempMenu(String items, Hex hex)
            : this(new List<String> { items}, hex)
        {

        }

        public TempMenu() { }
    }
}