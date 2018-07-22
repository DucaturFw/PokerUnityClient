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
                var flopView = UIConfiguration.Instance.Flop;

                flopView.Flop = flop;
            }
        }
    }
}