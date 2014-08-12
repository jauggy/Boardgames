using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tzolkien.Models.BoardState
{
    public class Wheel
    {
        public List<Location> Locations { get; set; }

        public void Rotate()
        {
        }

        public void AddWorker(Worker worker)
        {
            //Add worker to the next free position...
            //Wheel can't be full though
        }

        public void RemoveWorker()
        {
        }

        public bool IsFull()
        {
            return false;
        }

        public List<Worker> GetWorkersOwnedByPlayer(Player player)
        {
            return null;
        }
    }
}