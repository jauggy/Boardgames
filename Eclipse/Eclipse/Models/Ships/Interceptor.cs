using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Ships
{
    public class Interceptor:Ship
    {
        public Interceptor(Player owner)
        {
            Owner = owner;
            Code = "i";
        }
    }
}