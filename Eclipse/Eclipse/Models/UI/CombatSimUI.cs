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

        public ICombatant Enemy { get; set; }
        public ICombatant CurrentPlayer { get; set; }

        private List<Ship> _defenderShips = new List<Ship>();
        private List<Ship> _attackerShips = new List<Ship>();

        public String Message { get; set; }
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

        public void Simulate(IEnumerable<String> attacker, IEnumerable<String> defender, int number)
        {
            if(attacker.Count()==0||defender.Count()==0)
            {
                Message = "You have not added enough ships";
                return;
            }
            var denom = Convert.ToDouble(number);
            var shipsA = attacker.Select(x => CurrentPlayer.GetShipByName(x)).ToList();
            var shipsD = defender.Select(x => Enemy.GetShipByName(x)).ToList();
            var sim = new CombatSim();
            var total = sim.Simulate(shipsA, shipsD, number);
            var winRate = String.Format("{0:P}", total.Wins / denom);
            var loseRate = String.Format("{0:P}", total.Losses / denom);
            var drawRate = String.Format("{0:P}", total.Draws / denom);
            Message = String.Format("Win: {0}, Lose: {1}, Draw: {2}", winRate, loseRate, drawRate);
           // return attackerWins;
        }
    }
}