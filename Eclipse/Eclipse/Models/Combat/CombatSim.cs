using Eclipse.Models.Ships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Combat
{
    public class CombatSim
    {
        /// <summary>
        /// Returns true if attackers win
        /// </summary>
        /// <param name="attackers"></param>
        /// <param name="defenders"></param>
        /// <returns></returns>
        public  bool Simulate(IEnumerable<Ship> attackers, IEnumerable<Ship> defenders)
        {
            attackers.ToList().ForEach(x => x.IsAttacker = true);
            defenders.ToList().ForEach(x => x.IsAttacker = false);

            var list = GetShipsOrdered(attackers, defenders);
            var groups = FormShipGroups(list);

            //Missile round
            foreach(var group in groups)
            {
                var damageDice =  group.GetMissileDamageDice();
                var targets = attackers;
                if (group.IsAttacker)
                    targets = defenders;

                AssignDamage(targets, damageDice);

            }
            attackers.ToList().RemoveAll(x => x.IsDestroyed);
            defenders.ToList().RemoveAll(x => x.IsDestroyed);

            while(attackers.Count()>0 && defenders.Count()>0)
            {
                AllShipsFireCannons(attackers, defenders, groups);
            }

            return attackers.Count() > 0;

            //Fire missiles
            //Initative order //defender if tied
            //Hit >= 6. Add computers subtract sheidls
            //1 damage destroys ship add 1 for each hull
        }

        private void AllShipsFireCannons(IEnumerable<Ship> attackers, IEnumerable<Ship> defenders, List<ShipGroup> orderedGroups)
        {
            //Cannon round
            foreach (var group in orderedGroups)
            {
                var damageDice = group.GetMissileDamageDice();
                var targets = attackers;
                if (group.IsAttacker)
                    targets = defenders;

                AssignDamage(targets, damageDice);

                attackers.ToList().RemoveAll(x => x.IsDestroyed);
                defenders.ToList().RemoveAll(x => x.IsDestroyed);
            }



        }

        private List<Ship> GetShipsOrdered(IEnumerable<Ship> attackers, IEnumerable<Ship> defenders)
        {
            //var list = new List<Ship> { attacker, defender };
            var combined = new List<Ship>();
            combined.AddRange(attackers);
            combined.AddRange(defenders);

            return combined.OrderBy(x => x.Initiative).ThenBy(x => x.IsAttacker).ToList();
         
        }

        public List<ShipGroup> FormShipGroups(List<Ship> shipsOrdered)
        {
            Ship lastShip = null;
            var result = new List<ShipGroup>();
            foreach (var s in shipsOrdered)
            {
                if (lastShip==null || s.Name!= lastShip.Name || s.Owner!=lastShip.Owner)
                {
                    result.Add(new ShipGroup());
                    

                }
                result[result.Count - 1].AddShip(s);
                lastShip = s;
                
            }

            return result;
        }

        public List<DamageDice> AssignDamage(IEnumerable<Ship> targets, List<DamageDice> dice)
        {
            var damageOrder = targets.OrderByDescending(x => x.Health).ThenBy(x => x.Size);

            //kill assign
            var killOrder = targets.OrderByDescending(x => x.Size).ToList();
            //damage assign

            foreach (var ship in killOrder)
            {
                dice = ship.AssignToKill(dice);
            }

            foreach(var ship in damageOrder)
            {
                dice = ship.AssignDamage(dice);
            }

            return dice;
        }
    }
}