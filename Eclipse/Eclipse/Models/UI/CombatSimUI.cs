using Eclipse.Models.Combat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.UI
{
    public class CombatSimUI
    {
        public String[] DefenderNameSelection { get; set; }
        public Ship[] AttackerShipSelection { get; set; }
        public Ship[] DefenderShipSelection { get; set; }
        public Ship[] DefenderShips { get { return _defenderShips.ToArray(); } }
        public Ship[] AttackerShips { get { return _attackerShips.ToArray(); } }
        public ICombatant Enemy { get; set; }
        public ICombatant CurrentPlayer { get; set; }

        private List<Ship> _defenderShips = new List<Ship>();
        private List<Ship> _attackerShips = new List<Ship>();

        public CombatSimUI(String enemy)
        {
           // Enemy = enemy;
            Enemy = GameState.GetInstance().GetCombatantByName(enemy);
            CurrentPlayer = GameState.GetInstance().CurrentPlayer;
            AttackerShipSelection = CurrentPlayer.GetPossibleShipNames().Select(x => CurrentPlayer.GetShipByName(x)).ToArray();
            DefenderShipSelection = Enemy.GetPossibleShipNames().Select(x => Enemy.GetShipByName(x)).ToArray();

            var names = GameState.GetInstance().Players.Select(x => x.Name).ToList();
            var list = new List<String> { AncientPlayer.NAME };
            list.AddRange(names);
            DefenderNameSelection = list.ToArray();
        }

        public void ChangeEnemy(String name)
        {
            Enemy = GameState.GetInstance().GetCombatantByName(name);
            _defenderShips.Clear();
            DefenderShipSelection = Enemy.GetPossibleShipNames().Select(x => Enemy.GetShipByName(x)).ToArray();
        }

        public void AddDefenderShip(String name)
        {
            _defenderShips.Add(Enemy.GetShipByName(name));
        }

        public void AddAttackerShip(String name)
        {
            _attackerShips.Add(CurrentPlayer.GetShipByName(name));
        }

        public void Simulate()
        {
            var sim = new CombatSim();
            var attackerWins = sim.Simulate(AttackerShips, DefenderShips);
        }
    }
}