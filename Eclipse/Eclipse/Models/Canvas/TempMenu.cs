using Eclipse.Models.Hexes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Canvas
{
    public class TempMenu
    {
        public List<String> MenuItems { get; set; }
        public Hex Hex { get; set; }

        public TempMenu(List<String> items, Hex hex)
        {
            Hex = hex;
            MenuItems = items;
        }
    }
}