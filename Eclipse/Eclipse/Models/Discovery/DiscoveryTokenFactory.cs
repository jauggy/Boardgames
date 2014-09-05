using Eclipse.Models.Tech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eclipse.Models.Discovery
{

    public class DiscoveryTokenFactory
    {
        public static DiscoveryToken CreateRandomDiscovery()
        {
            int num = RandomGenerator.GetInt(16, 21);

            if (num <= 3)
            {
               return new ResourceDiscovery(PopulationType.Money, 8);
            }
            else if(num <=6)
            {
                return new ResourceDiscovery(PopulationType.Science, 5);
            }
            else if(num<=9)
            {
                return new ResourceDiscovery(PopulationType.Materials, 6);
            }
            else if(num<=12)
            {
                return new TechnologyDiscovery();
            }
            else if(num<=15)
            {
                return new CruiserDiscovery();
            }
            else
            {
                return new ShipPartDiscovery(GetRandomShipPart());
            }


        }

        public static ShipPart GetRandomShipPart()
        {
            var part = new ShipPart();
            var i = RandomGenerator.GetInt(1,6);
            if(i==1)
            {
                
                part.Name = "Axion Computer";
                part.Computer = 3;
            }
            else if(i==2)
            {
                part.Name = "Hypergrid Source";
                part.EnergySource = 11;
            }
            else if(i==3)
            {
                part.Name = "Shard Hull";
                part.Hull = 3;
            }
            else if (i == 4) {
                part.Name = "Ion Turrent";
                part.CannonDamage = new List<int> { 1, 1 };
                part.EnergyRequirement = 1;
            }
            else if (i == 5) {
                part.Name = "Conformal Drive";
                part.Initiative = 2;
                part.EnergyRequirement = 2;
            }

            else if (i == 6) {
                part.Name = "Flux Shield";
                part.Shield = 3;
                part.EnergyRequirement = 2;
            }

            return part;
        }
    }
}