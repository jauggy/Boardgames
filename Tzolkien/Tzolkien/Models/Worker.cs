using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tzolkien.Models
{
    /// <summary>
    /// 
    /// Should the worker have knowledge of its position? Or should the wheel have knowledge
    /// The wheel must have knowledge because it keeps track of the valid positions available...
    /// </summary>
    public class Worker
    {
        public bool IsOnBoard { get; private set; }
        public Player Owner { get; set; }

        public void AddToBoard()
        {
            IsOnBoard = true;
        }

        public void RemoveFromBoard()
        {
            IsOnBoard = false;
        }
    }
}