using System.Runtime.Serialization.Formatters;
using Depoker.UI.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Depoker.UI.Views
{
    public class FlopView : MonoBehaviour
    {
        [SerializeField] private CardView[] cards;
        [SerializeField] private ChipsView chipsView;
        [SerializeField] private Text bidValue;
        
        private DirtableValue<Bid> dirtableBidData = new DirtableValue<Bid>();
        private DirtableValue<Chips> dirtableChipsData = new DirtableValue<Chips>();
        private DirtableValue<Flop> dirtableFlop = new DirtableValue<Flop>();

        public Flop Flop
        {
            get { return dirtableFlop.Value; }
            set { dirtableFlop.Value = value; }
        }

        public Chips ChipsData
        {
            get { return dirtableChipsData.Value; }
            set { dirtableChipsData.Value = value; }
        }
        
        public Bid BidData
        {
            get { return dirtableBidData.Value; }
            set { dirtableBidData.Value = value; }
        }

        void OnEnable()
        {
            dirtableFlop.ValueUpdated += FlopUpdated;
            dirtableBidData.ValueUpdated += BidUpdate;
            dirtableChipsData.ValueUpdated += ChipsUpdated;
            
            FlopUpdated(default(Flop));
            BidUpdate(default(Bid));
            ChipsUpdated(default(Chips));
        }

        private void ChipsUpdated(Chips chips)
        {
            chipsView.Chips = chips;
        }

        private void BidUpdate(Bid bid)
        {
            bidValue.text = ViewUtils.NumberToValue(bid.Value);
            bidValue.transform.parent.gameObject.SetActive(bid.Value > 0);
        }

        void OnDisable()
        {
            dirtableFlop.ValueUpdated -= FlopUpdated;
        }

        private void FlopUpdated(Flop obj)
        {
            Prepare(cards[0], obj.First);
            Prepare(cards[1], obj.Second);
            Prepare(cards[2], obj.Third);
            Prepare(cards[3], obj.Fourth);
            Prepare(cards[4], obj.Fiveth);
        }

        private void Prepare(CardView cardView, Card card)
        {
            if (card.State == 0)
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