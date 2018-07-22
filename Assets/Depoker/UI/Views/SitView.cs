using Depoker.UI.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Depoker.UI.Views
{
    public class SitView : MonoBehaviour
    {
        public bool vacant;
        
        private DirtableValue<Bid> dirtableBidData = new DirtableValue<Bid>();
        private DirtableValue<Bank> dirtableBankData = new DirtableValue<Bank>();
        private DirtableValue<Chips> dirtableChipsData = new DirtableValue<Chips>();
        private DirtableValue<bool> dirtableLeftCardOpen = new DirtableValue<bool>();
        private DirtableValue<bool> dirtableRightCardOpen = new DirtableValue<bool>();
        private DirtableValue<Card> dirtableLeftCard = new DirtableValue<Card>();
        private DirtableValue<Card> dirtableRightCard = new DirtableValue<Card>();
        private DirtableValue<bool> dirtableVacant = new DirtableValue<bool>();

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

        public bool Vacant
        {
            get { return dirtableVacant.Value;}
            set { dirtableVacant.Value = value; }
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
            dirtableLeftCard.ValueUpdated += UpdateLeftCard;
            dirtableRightCard.ValueUpdated += UpdateRightCard;
            dirtableVacant.ValueUpdated += UpdateVacant;
            
            UpdateVacant(Vacant);
            UpdateBidData(BidData);
            UpdateBankData(BankData);
            UpdateChipsData(ChipsData);
            UpdateLeftCard(LeftCard);
            UpdateRightCard(RightCard);
        }

        void OnDisable()
        {
            dirtableBidData.ValueUpdated -= UpdateBidData;
            dirtableBankData.ValueUpdated -= UpdateBankData;
            dirtableChipsData.ValueUpdated -= UpdateChipsData;
            dirtableLeftCard.ValueUpdated -= UpdateLeftCard;
            dirtableRightCard.ValueUpdated -= UpdateRightCard;
            dirtableVacant.ValueUpdated -= UpdateVacant;
        }

        private void UpdateVacant(bool obj)
        {
            vacant = obj;

            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(!obj);
            }
        }

        void UpdateBidData(Bid newValue)
        {
            BidValue.text = ViewUtils.NumberToValue(newValue.Value);
            BidValue.transform.parent.gameObject.SetActive(newValue.Value > 0);
        }

        void UpdateBankData(Bank newValue)
        {
            BankValue.text = string.Format("Bank: <b>{0}</b>", ViewUtils.NumberToValue(newValue.Value));
            BankValue.transform.parent.gameObject.SetActive(newValue.Value > 0);
        }

        void UpdateChipsData(Chips newValue)
        {
            ChipsView.Chips = newValue;
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