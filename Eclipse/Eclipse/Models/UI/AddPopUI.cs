using Eclipse.Models.Hexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.UI
{
    public class AddPopUI
    {
        public OptionUI[] Options { get; set; }

        public AddPopUI(int x, int y)
        {
            var list = new List<OptionUI>();
            var result = HexBoard.GetInstance().FindHex(x, y,true).GetPopulatableTypes(true);
 
            foreach(var option in result)
            {
                list.Add(new OptionUI(option));
            }

            Options = list.ToArray();
        }
    }
}