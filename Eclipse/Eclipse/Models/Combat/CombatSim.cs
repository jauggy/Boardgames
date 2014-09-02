using Eclipse.Models.Ships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Combat
{
    public enum CombatResult { Default, Win, Lose, Draw };

    public class CombatSim
    {
     
        public CombatResultTotal Simulate(IEnumerable<Ship> attackers, IEnumerable<Ship> defenders, int numSimulations)
        {
            var win = 0;
            var total = new CombatResultTotal();
            for(int i = 0; i<numSimulations;i++)
            {
                var result = Simulate(attackers, defenders);
                total.AddResult(result);
            
            }

            return total;

        }
        /// <summary>
        /// Returns true if attackers win
        /// </summary>
        /// <param name="attackers"></param>
        /// <param name="defenders"></param>
        /// <returns></returns>
        public CombatResult Simulate(IEnumerable<Ship> att, IEnumerable<Ship> def)
        {
            try
            {
                var attackers = new List<Ship>();
                attackers.AddRange( att);
                var defenders = new List<Ship>();
                defenders.AddRange(def);

                attackers.ForEach(x => x.Reset());
                defenders.ForEach(x => x.Reset());

                attackers.ToList().ForEach(x => x.IsAttacker = true);
                defenders.ToList().ForEach(x => x.IsAttacker = false);

                var list = GetShipsOrdered(attackers, defenders);
                var groups = FormShipGroups(list);

                //Missile round
                foreach (var group in groups)
                {
                    var damageDice = group.GetMissileDamageDice();
                    var targets = attackers;
                    if (group.IsAttacker)
                        targets = defenders;

                    AssignDamage(targets, damageDice);

                }
                attackers.ToList().RemoveAll(x => x.IsDestroyed);
                defenders.ToList().RemoveAll(x => x.IsDestroyed);

                var result = CombatResult.Default;
                while (result == CombatResult.Default)
                {
                    result = AllShipsFireCannons(attackers, defenders, groups);
                }

                return result;
            }
            catch(Exception e)
            {

            }
            throw new NotImplementedException();
            //Fire missiles
            //Initative order //defender if tied
            //Hit >= 6. Add computers subtract sheidls
            //1 damage destroys ship add 1 for each hull
        }

        private CombatResult AllShipsFireCannons(List<Ship> attackers, List<Ship> defenders, List<ShipGroup> orderedGroups)
        {
            if (attackers.Count() > 0 && defenders.Count() > 0)
            {
                //Cannon round
                foreach (var group in orderedGroups)
                {
                    var damageDice = group.GetCannonDamageDice();
                    var targets = attackers;
                    if (group.IsAttacker)
                        targets = defenders;

                    AssignDamage(targets, damageDice);

                    attackers.RemoveAll(x => x.IsDestroyed);
                    defenders.RemoveAll(x => x.IsDestroyed);
                }
            }

            if (attackers.Count() == 0)
                return CombatResult.Lose;
            else if (defenders.Count() == 0)
                return CombatResult.Win;
            else if (attackers.Count(x => x.IsArmed()) + defenders.Count(x => x.IsArmed()) == 0)
                return CombatResult.Draw;
            else
                return CombatResult.Default;

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