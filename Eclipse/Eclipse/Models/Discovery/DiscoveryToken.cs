using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eclipse.Models.Discovery
{
    public class DiscoveryToken
    {
        public String Html {get;set;}
        public String Name { get;set;  }
        public virtual void ExecuteDiscovery(String args) { }
    }
}
