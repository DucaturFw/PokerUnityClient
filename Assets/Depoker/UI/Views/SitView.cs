using Depoker.UI.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Depoker.UI.Views
{
    public class SitView : MonoBehaviour
    {
        private DirtableValue<Bid> dirtableBidData = new DirtableValue<Bid>();
        private DirtableValue<Bank> dirtableBankData = new DirtableValue<Bank>();
        private DirtableValue<Chips> dirtableChipsData = new DirtableValue<Chips>();
        private DirtableValue<bool> dirtableLeftCardOpen = new DirtableValue<bool>();
        private DirtableValue<bool> dirtableRightCardOpen = new DirtableValue<bool>();
        private DirtableValue<Card> dirtableLeftCard = new DirtableValue<Card>();
        private DirtableValue<Card> dirtableRightCard = new DirtableValue<Card>();

        public Bid BidData
        {
            get { return dirtableBidData.Value; }
            set { dirtableBidData.Value = value; }
        }

        public Bank BankData
        {
            get { return dirtableBankData.Value; }
            set { dirtableBankData.Value = value; }
        }

        public Chips ChipsData
        {
            get { return dirtableChipsData.Value; }
            set { dirtableChipsData.Value = value; }
        }

        public bool LeftCardOpen
        {
            get { return dirtableLeftCardOpen.Value; }
            set { dirtableLeftCardOpen.Value = value; }
        }

        public bool RightCardOpen
        {
            get { return dirtableRightCardOpen.Value; }
            set { dirtableRightCardOpen.Value = value; }
        }

        public Card LeftCard
        {
            get { return dirtableLeftCard.Value; }
            set { dirtableLeftCard.Value = value; }
        }

        public Card RightCard
        {
            get { return dirtableRightCard.Value; }
            set { dirtableRightCard.Value = value; }
        }

        public Text BidValue;
        public Text BankValue;
        public ChipsView ChipsView;
        public CardView LeftCardView;
        public CardView RightCardView;

        void OnEnable()
        {
            dirtableBidData.ValueUpdated += UpdateBidData;
            dirtableBankData.ValueUpdated += UpdateBankData;
            dirtableChipsData.ValueUpdated += UpdateChipsData;
            dirtableLeftCardOpen.ValueUpdated += UpdateLeftCardOpen;
            dirtableRightCardOpen.ValueUpdated += UpdateRightCardOpen;
            dirtableLeftCard.ValueUpdated += UpdateLeftCard;
            dirtableRightCard.ValueUpdated += UpdateRightCard;
        }

        void OnDisable()
        {
            dirtableBidData.ValueUpdated -= UpdateBidData;
            dirtableBankData.ValueUpdated -= UpdateBankData;
            dirtableChipsData.ValueUpdated -= UpdateChipsData;
            dirtableLeftCardOpen.ValueUpdated -= UpdateLeftCardOpen;
            dirtableRightCardOpen.ValueUpdated -= UpdateRightCardOpen;
            dirtableLeftCard.ValueUpdated -= UpdateLeftCard;
            dirtableRightCard.ValueUpdated -= UpdateRightCard;
        }

        void UpdateBidData(Bid newValue) {
            BidValue.text = ViewUtils.NumberToValue(newValue.Value);
        }

        void UpdateBankData(Bank newValue)
        {
            BankValue.text = string.Format("Bank: <b>{0}</b>", ViewUtils.NumberToValue(newValue.Value));
        }

        void UpdateChipsData(Chips newValue)
        {
            ChipsView.Chips = newValue;
        }

        void UpdateLeftCardOpen(bool newValue)
        {
            LeftCardView.Covered = newValue;
        }

        void UpdateRightCardOpen(bool newValue)
        {
            RightCardView.Covered = newValue;
        }

        void UpdateLeftCard(Card newValue)
        {
            LeftCardView.Card = newValue;
        }

        void UpdateRightCard(Card newValue)
        {
            RightCardView.Card = newValue;
        }
    }
}