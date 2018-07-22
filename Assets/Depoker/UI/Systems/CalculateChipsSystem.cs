using Depoker.UI.Components;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Depoker.UI
{
    public class CalculateChipsSystem : ComponentSystem
    {
        private static int[] chipSizes = new int[]
        {
            1,
            5,
            10,
            25,
            50,
            100,
            500,
            1000,
            5000,
            10000
        };
        
        public struct Filter
        {
            public readonly int Length;
            [ReadOnly] public ComponentDataArray<Bid> Bids;
            public EntityArray Entities;
        }

        [Inject] private Filter filter;
        
        protected override void OnUpdate()
        {
            if (filter.Length > 0)
            {
                for (int index = 0; index < filter.Length; index++)
                {
                    var bid = filter.Bids[index];
                    var value = bid.Value;
                    var chips = new Chips(); 
                    if (value == 0)
                    {
                        EntityManager.RemoveComponent<Chips>(filter.Entities[index]);
                    }
                    else
                    {
                        var sizeIndex = 9;
                        while (sizeIndex >= 0 && value > 0)
                        {
                            var chipsCount = value / chipSizes[sizeIndex];
                           
                            chips[sizeIndex] = chipsCount;
                            value -= chipsCount * chipSizes[sizeIndex];
                            sizeIndex--;
                        }
                        
                        if (!EntityManager.HasComponent<Chips>(filter.Entities[index]))
                        {
                            PostUpdateCommands.AddComponent(filter.Entities[index], chips);
                        }
                        else
                        {
                            PostUpdateCommands.SetComponent(filter.Entities[index], chips);
                        }
                    }
                }
            }
        }
    }
}