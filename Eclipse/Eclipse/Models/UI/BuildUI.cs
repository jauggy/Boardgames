using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.UI
{
    public class BuildUI
    {
        public Ship[] ShipBlueprints { get; set; }
        public String ResourceMessage { get; set; } //Materials Storage: 5 //Materials Production: 1

        public BuildUI(int x, int y)
        {

        }
    }
}