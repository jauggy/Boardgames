using Eclipse.Models.Hexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eclipse.Models.Playerboards
{
    public interface IBuildable
    {
        string Name { get; }
        string Description { get; }
        int MaterialCost { get; }
        void ActionOnBuild(Hex hex);
        bool IsAffordable { get;  }
    }
}
