using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tzolkien.Models.BoardState
{
    public class Location
    {
        public Worker Worker { get; set; }
        public Action<Worker> LocationAction { get; set; }

        public void RemoveWorker()
        {
            if (Worker != null)
            {
                LocationAction(Worker);
                Worker = null;
                
            }
        }

    }
}