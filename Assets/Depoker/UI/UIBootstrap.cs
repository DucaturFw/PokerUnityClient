using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Depoker.UI.Components;
using Unity.Entities;
using UnityEngine;

namespace Depoker.UI
{
    public class UIBootstrap : MonoBehaviour
    {
        public EntityArchetype LocalPlayer;
        private Stack<Card> Deck;

        private World Active => World.Active;
        private EntityManager Manager => Active.GetOrCreateManager<EntityManager>();
        
        IEnumerator Start()
        {
            var cards = new Card[52];
            for (var cardIndex = 0; cardIndex < cards.Length; cardIndex++)
            {
                cards[cardIndex] = new Card
                {
                    State = Card.States.None,
                    Suit = cardIndex / 13,
                    Value = cardIndex % 13
                };
            }

            Deck = new Stack<Card>(cards.OrderBy(x => Random.value));
            
            LocalPlayer = Manager.CreateArchetype(
                typeof(My),
                typeof(Player),
                typeof(Sit),
                typeof(Bank)
            );

            Entity flop = Manager.CreateEntity(typeof(Flop));

            Entity local = Entity.Null;
            Entity oponent1 = Entity.Null;
            Entity oponent2 = Entity.Null;
            

            for (int i = 0; i < 9; i++)
            {
                var sit = Manager.CreateEntity();
                AddOrSet(sit, new Sit { Id = i });

                if (i == 0)
                {
                    local = sit;
                }

                if (i == 4)
                {
                    oponent1 = sit;
                }

                if (i == 5)
                {
                    oponent2 = sit;
                }
            }

            yield return new WaitForSeconds(1f);
            AddOrSet(local, new Player {Id = 50});
            AddOrSet(local, new Bank { Value = 5000 });
            
            
            yield return new WaitForSeconds(1f);
            AddOrSet(oponent1, new Player {Id = 60});
            AddOrSet(oponent1, new Bank { Value = 5000 });
            
            yield return new WaitForSeconds(0.5f);
            AddOrSet(oponent2, new Player {Id = 70});
            AddOrSet(oponent2, new Bank { Value = 5000 });
            
            
            yield return new WaitForSeconds(0.5f);

            AddOrSet(local, new Cards
            {
                Left = RandomCard(),
                Right = RandomCard()
            });
            
            yield return new WaitForSeconds(0.5f);
            
            AddOrSet(oponent1, new Cards
            {
                Left = RandomCard(Card.States.Close),
                Right = RandomCard(Card.States.Close)
            });
            
            yield return new WaitForSeconds(0.5f);
            
            AddOrSet(oponent2, new Cards
            {
                Left = RandomCard(Card.States.Close),
                Right = RandomCard(Card.States.Close)
            });
            
            AddOrSet(oponent1, new Bid() { Value = 5 });
            AddOrSet(oponent2, new Bid() { Value = 10 });
            
            yield return new WaitForSeconds(0.5f);
            
            AddOrSet(local, new Bid() { Value = 100 });
            yield return new WaitForSeconds(0.5f);
            AddOrSet(oponent1, new Bid() { Value = 250 });
            yield return new WaitForSeconds(0.5f);
            Manager.RemoveComponent<Cards>(oponent2);
            yield return new WaitForSeconds(0.5f);
            AddOrSet(local, new Bid() { Value = 250 });
            yield return new WaitForSeconds(0.5f);
            
            var flop1 = new Flop
            {
                First = RandomCard(),
                Second = RandomCard(),
                Third = RandomCard()
            };

            AddOrSet(flop, flop1);
            
            yield return new WaitForSeconds(1.5f);

            flop1.Fourth = RandomCard();
            AddOrSet(flop, flop1);
            
            yield return new WaitForSeconds(0.5f);
            AddOrSet(oponent1, new Bid() { Value = 1500 });
            yield return new WaitForSeconds(0.5f);
            AddOrSet(local, new Bid() { Value = 1500 });
           
            yield return new WaitForSeconds(0.1f);
            
            flop1.Fiveth = RandomCard();
            AddOrSet(flop, flop1);
            
            
            yield return new WaitForSeconds(0.1f);
            AddOrSet(oponent1, new Bid() { Value = 5000 });
            yield return new WaitForSeconds(0.2f);
            AddOrSet(local, new Bid() { Value = 5000 });
            
            
            AddOrSet(oponent1, new Cards
            {
                Left = RandomCard(),
                Right = RandomCard()
            });
        }
        

        private Card RandomCard(Card.States state = Card.States.Open)
        {
            var card = Deck.Pop();
            card.State = state;
            return card;
        }

        void AddOrSet<T>(Entity e, T data) where T : struct, IComponentData
        {
            if (Manager.HasComponent<T>(e))
            {
                Manager.SetComponentData(e, data);
            }
            else
            {
                Manager.AddComponentData(e, data);
            }
        } 
    }
}