using Depoker.UI.Components;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Depoker.UI
{
    public class PlayerStateUISystem : ComponentSystem
    {
        public struct Filter
        {
            public readonly int Length;
            [ReadOnly] public ComponentDataArray<Sit> Sits;
            public EntityArray Entities;
        }

        [Inject] public Filter filter;

        protected override void OnUpdate()
        {
            if (filter.Length > 0)
            {

                for (int index = 0; index < filter.Length; index++)
                {
                    var entity = filter.Entities[index];
                    var id = filter.Sits[index].Id;
                    var my = EntityManager.HasComponent<My>(entity);
                    var sit = my ? UIConfiguration.Instance.My : UIConfiguration.Instance.Sits[id];

                    if (EntityManager.HasComponent<Bid>(entity))
                    {
                        sit.BidData = EntityManager.GetComponentData<Bid>(entity);
                    }

                    if (EntityManager.HasComponent<Bank>(entity))
                    {
                        sit.BankData = EntityManager.GetComponentData<Bank>(entity);
                    }

                    if (EntityManager.HasComponent<Chips>(entity))
                    {
                        sit.ChipsData =
                            EntityManager.GetComponentData<Chips>(entity);
                    }

                    if (EntityManager.HasComponent<Cards>(entity))
                    {
                        var cards = EntityManager.GetComponentData<Cards>(entity);
                        sit.LeftCard = cards.Left;
                        sit.RightCard = cards.Right;
                    }
                }
            }
        }
    }
}