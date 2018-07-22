using Depoker.UI.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Depoker.UI.Views
{
    public class CardView : MonoBehaviour
    {
        private const int SHEET_ROWS = 13;

        public Image CardSprite;
        public Image Cover;
        
        private DirtableValue<bool> dirtableCovered = new DirtableValue<bool>();
        private DirtableValue<Card> dirtableCard = new DirtableValue<Card>();

        public bool Covered
        {
            get { return dirtableCovered.Value; }
            set { dirtableCovered.Value = value; }
        }

        public Card Card
        {
            get { return dirtableCard.Value; }
            set { dirtableCard.Value = value; }
        }

        void OnEnable()
        {
            dirtableCard.ValueUpdated += CardUpdated;
        }

        void OnDisable()
        {
            dirtableCard.ValueUpdated -= CardUpdated;
        }

        private void CardUpdated(Card obj)
        {
            CardSprite.sprite = UIConfiguration.Instance.Cards[obj.Suit * 13 + obj.Value];
        }
    }
}