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
        private UpdateStateBarrier Barrier => Active.GetOrCreateManager<UpdateStateBarrier>();
        
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
            
            Entity flop = Manager.CreateEntity(typeof(Flop), typeof(Bid));

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

            yield return new WaitForSeconds(2f);
            yield return new WaitForEndOfFrame();
            
            AddOrSet(local, new Player {Id = 50});
            AddOrSet(local, new Bank { Value = 10000 });
            
            
            yield return new WaitForSeconds(2f);
            yield return new WaitForEndOfFrame();
            
            AddOrSet(oponent1, new Player {Id = 60});
            AddOrSet(oponent1, new Bank { Value = 10000 });
            
            yield return new WaitForSeconds(2.5f);
            yield return new WaitForEndOfFrame();
            
            AddOrSet(oponent2, new Player {Id = 70});
            AddOrSet(oponent2, new Bank { Value = 10000 });
            
            yield return new WaitForSeconds(2.5f);
            yield return new WaitForEndOfFrame();
            
            
            AddOrSet(oponent1, new Bid() { Value = 5 });
            AddOrSet(oponent2, new Bid() { Value = 10 });
            
            AddOrSet(oponent1, new Bank() { Value = 10000 - 5 });
            AddOrSet(oponent2, new Bank() { Value = 10000 - 10 });
            
            
            yield return new WaitForSeconds(2.5f);
            yield return new WaitForEndOfFrame();
            

            var localCards = default(Cards);
            var opponent1Cards = default(Cards);
            var opponent2Cards = default(Cards);

            localCards.Right = RandomCard();
            
            AddOrSet(local, localCards);
            yield return new WaitForSeconds(2.5f);
            yield return new WaitForEndOfFrame();
            
            opponent1Cards.Right = RandomCard(Card.States.Close);
            AddOrSet(oponent1, opponent1Cards);
            yield return new WaitForSeconds(2.5f);
            yield return new WaitForEndOfFrame();
            
            opponent2Cards.Right = RandomCard(Card.States.Close);
            AddOrSet(oponent2, opponent2Cards);
            yield return new WaitForSeconds(2.5f);
            yield return new WaitForEndOfFrame();
            
            
            localCards.Left = RandomCard();
            AddOrSet(local, localCards);
            yield return new WaitForSeconds(2.5f);
            yield return new WaitForEndOfFrame();
            
            opponent1Cards.Left = RandomCard(Card.States.Close);
            AddOrSet(oponent1, opponent1Cards);
            yield return new WaitForSeconds(2.5f);
            yield return new WaitForEndOfFrame();
            
            opponent2Cards.Left = RandomCard(Card.States.Close);
            AddOrSet(oponent2, opponent2Cards);
            
            yield return new WaitForSeconds(2.5f);
            yield return new WaitForEndOfFrame();
            
            
            AddOrSet(local, new Bid() { Value = 100 });
            AddOrSet(local, new Bank() { Value = 10000 - 100 });
            yield return new WaitForSeconds(2.5f);
            yield return new WaitForEndOfFrame();
            
            AddOrSet(oponent1, new Bid() { Value = 250 });
            AddOrSet(local, new Bank() { Value = 10000 - 250 });
            yield return new WaitForSeconds(2.5f);
            yield return new WaitForEndOfFrame();
            
            Manager.RemoveComponent<Cards>(oponent2);
            yield return new WaitForSeconds(2.5f);
            yield return new WaitForEndOfFrame();
            
            AddOrSet(local, new Bid() { Value = 250 });
            AddOrSet(local, new Bank() { Value = 10000 - 250 });
            yield return new WaitForSeconds(2.5f);
            yield return new WaitForEndOfFrame();
            
            
            var flop1 = new Flop
            {
                First = RandomCard(),
                Second = RandomCard(),
                Third = RandomCard()
            };

            AddOrSet(flop, flop1);
            AddOrSet(local, default(Bid));
            AddOrSet(oponent1, default(Bid));
            AddOrSet(oponent2, default(Bid));
            AddOrSet(flop, new Bid() { Value = 510 });
            
            yield return new WaitForSeconds(2.5f);
            yield return new WaitForEndOfFrame();
            

            flop1.Fourth = RandomCard();
            AddOrSet(flop, flop1);
            
            yield return new WaitForSeconds(2.5f);
            yield return new WaitForEndOfFrame();
            
            AddOrSet(oponent1, new Bid() { Value = 1500 });
            AddOrSet(oponent1, new Bank() { Value = 10000 - 250 - 1500 });
            
            yield return new WaitForSeconds(2.5f);
            yield return new WaitForEndOfFrame();
            
            AddOrSet(local, new Bid() { Value = 1500 });
            AddOrSet(local, new Bank() { Value = 10000 - 250 - 1500 });
           
            yield return new WaitForSeconds(2.1f);
            yield return new WaitForEndOfFrame();
            
            
            AddOrSet(local, default(Bid));
            AddOrSet(oponent1, default(Bid));
            AddOrSet(flop, new Bid() { Value = 510 + 3000 });
            flop1.Fiveth = RandomCard();
            AddOrSet(flop, flop1);
            
            
            yield return new WaitForSeconds(2.5f);
            yield return new WaitForEndOfFrame();
            
            AddOrSet(oponent1, new Bid() { Value = 5000 });
            AddOrSet(oponent1, new Bank() { Value = 10000 - 250 - 1500 - 5000 });
            yield return new WaitForSeconds(2.5f);
            yield return new WaitForEndOfFrame();
            
            AddOrSet(local, new Bid() { Value = 5000 });
            AddOrSet(local, new Bank() { Value = 10000 - 250 - 1500 - 5000 });
            
            yield return new WaitForSeconds(2.5f);
            yield return new WaitForEndOfFrame();
            
            AddOrSet(local, default(Bid));
            AddOrSet(oponent1, default(Bid));
            AddOrSet(flop, new Bid() { Value = 510 + 3000 + 10000 });
            
            
            AddOrSet(oponent1, new Cards
            {
                Left = RandomCard(),
                Right = RandomCard()
            });
            
            yield return new WaitForSeconds(2f);
            yield return new WaitForEndOfFrame();
            
            
            AddOrSet(flop, default(Bid));
            AddOrSet(oponent1, default(Bid));
            AddOrSet(oponent2, default(Bid));
            AddOrSet(local, default(Bid));

            Manager.RemoveComponent<Cards>(local);
            Manager.RemoveComponent<Cards>(oponent1);
            Manager.RemoveComponent<Flop>(flop);
            
            AddOrSet(local, new Bank { Value = 10000 - 250 - 1500 - 5000 + 510 + 3000 + 10000 });
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