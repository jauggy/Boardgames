using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tzolkien.Models.BoardState
{
    public class YaxLocation:Location
    {
        /*
         * 1 wood
         * stone corn
         * gold 2 corn
         * skull
         * gold stone 2 corn
         * 
         */


        public void PalenqueLocation(int index)
        {
            Index = index;
        }

        public List<PlayerAction> GetPossibleActions()
        {
            var list = new List<PlayerAction>();

            var result = new List<PlayerAction>();
            switch (Index)
            {
                case 1:
                    list.Add(
                        new PlayerAction((p) =>
                        {
                            p.AddResource(Resource.Wood, 1);
                        })
                        );
                    break;
                case 2:
                        list.Add(
                        new PlayerAction((p) =>
                        {
                            p.AddResource(Resource.Stone, 1);
                            p.AddResource(Resource.Corn, 1);
                        })
                        );
                    break;
                case 3:
                    list.Add(
                    new PlayerAction((p) =>
                    {
                        p.AddResource(Resource.Gold, 1);
                        p.AddResource(Resource.Corn, 2);
                    })
                    );
                    break;
                case 4:
                    list.Add(
                    new PlayerAction((p) =>
                    {
                        p.AddResource(Resource.Skull, 1);

                    })
                    );
                    break;
                case 5:
                    list.Add(
                    new PlayerAction((p) =>
                    {
                        p.AddResource(Resource.Gold, 1);
                        p.AddResource(Resource.Stone, 1);
                        p.AddResource(Resource.Corn, 2);
                    })
                    );
                    break;

            }
            if (Index == 1)
            {

            }

            return list;
        }



 
    }
}