using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models
{
    public interface ICombatant
    {
        String Name { get; }
        Ship GetShipByName(String shipName);
        List<String> GetPossibleShipNames();
    }
}