using System.Runtime.Serialization.Formatters;
using Depoker.UI.Components;
using UnityEngine;

namespace Depoker.UI.Views
{
    public class FlopView : MonoBehaviour
    {
        [SerializeField] private CardView[] Cards;
        
        private DirtableValue<Flop> dirtableFlop = new DirtableValue<Flop>();

        public Flop Flop
        {
            get { return dirtableFlop.Value; }
            set { dirtableFlop.Value = value; }
        }

        void OnEnable()
        {
            dirtableFlop.ValueUpdated += FlopUpdated;
        }

        void OnDisable()
        {
            dirtableFlop.ValueUpdated -= FlopUpdated;
        }

        private void FlopUpdated(Flop obj)
        {
            Prepare(Cards[0], obj.First);
            Prepare(Cards[1], obj.Second);
            Prepare(Cards[2], obj.Third);
            Prepare(Cards[3], obj.Fourth);
            Prepare(Cards[4], obj.Fiveth);
        }

        private void Prepare(CardView cardView, Card card)
        {
            if (card.Open == 0)
            {
                cardView.gameObject.SetActive(false);
            }
            else
            {
                cardView.gameObject.SetActive(true);
                cardView.Card = card;
            }
        }
    }
}