using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
    }
}