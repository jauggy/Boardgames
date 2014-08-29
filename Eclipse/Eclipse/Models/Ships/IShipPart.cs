using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Ships
{
    public interface IShipPart
    {
         List<int> CannonDamage { get;  }
         int Shield { get;  }
         int Hull { get;  }
         int Computer { get;  }
         int EnergyRequirement { get;  }
         int EnergySource { get;  }
         List<int> MissileDamage { get;  }
         int Initiative { get;  }
         int Movement { get;  }

    }
}