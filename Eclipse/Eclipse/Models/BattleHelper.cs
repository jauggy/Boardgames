using Eclipse.Models.Playerboards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models
{
    public class BattleHelper
    {
        public int GetTotalCannonDamage(ShipBlueprint attacker, ShipBlueprint defender)
        {
            var totalDamage = 0;
            foreach(var cannonDamage in attacker.GetCannonDamage())
            {
                var dice = RandomGenerator.GetDice();
                if(dice==6)
                {
                    totalDamage += cannonDamage;
                }
                else if(dice==1)
                {
                    //nothing
                }
                else
                {
                    if (dice + attacker.Computer - defender.Shield >= 6)
                        totalDamage += cannonDamage;
                }
            }

            return totalDamage;
        }
    }
}