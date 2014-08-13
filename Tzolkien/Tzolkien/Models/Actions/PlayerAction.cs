using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tzolkien.Models
{
    public class PlayerAction
    {
        public Action<Player> Execute { get; set; }


        public PlayerAction(Action<Player> method)
        {
            Execute = method;
        }

    }
}