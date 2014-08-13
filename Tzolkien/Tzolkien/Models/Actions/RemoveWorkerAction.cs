using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tzolkien.Models.BoardState;

namespace Tzolkien.Models
{
    public class RemoveWorkerAction
    {
        public void Execute(Worker worker)
        {
            if (!worker.IsOnBoard)
            {
                throw new NotImplementedException();
            }
        }

        public List<Location> RemoveLocations()
        {
            //a list of locations where removal of worker is possible
            return null;
        }
    }
}