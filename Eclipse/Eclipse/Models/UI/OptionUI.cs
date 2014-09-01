using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.UI
{
    public class OptionUI
    {
        public String SingleOption { get; set; }
        public List<String> MultiOption { get; set; }

        public OptionUI(String option)
        {
            if (option == "Unknown")
            {
                MultiOption = new List<String> { "Money", "Science", "Materials" };

            }
            else
                SingleOption = option;
        }
    }
}