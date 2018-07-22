using Depoker.UI.Components;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Depoker.UI
{
    public class UpdateFlopSystem : ComponentSystem
    {
        public struct Filter
        {
            public readonly int Length;
            [ReadOnly] public ComponentDataArray<Flop> Flops;
            public EntityArray Entities;
        }

        [Inject] public Filter filter;

        protected override void OnUpdate()
        {
            if (filter.Length > 0)
            {
                var index = 0;
                var flop = filter.Flops[index];
                var entity = filter.Entities[index];
                var flopView = UIConfiguration.Instance.Flop;

                flopView.Flop = flop;

                if (EntityManager.HasComponent<Bid>(entity))
                {
                    flopView.BidData = EntityManager.GetComponentData<Bid>(entity);
                }

                if (EntityManager.HasComponent<Chips>(entity))
                {
                    flopView.ChipsData = EntityManager.GetComponentData<Chips>(entity);
                }
            }
        }
    }
}