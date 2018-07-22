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
        
        private DirtableValue<Card> dirtableCard = new DirtableValue<Card>();
        private CanvasGroup cachedGroup;

        private CanvasGroup Group
        {
            get
            {
                if (cachedGroup == null)
                {
                    cachedGroup = gameObject.AddComponent<CanvasGroup>();
                }

                return cachedGroup;
            }
        }

        public Card Card
        {
            get { return dirtableCard.Value; }
            set { dirtableCard.Value = value; }
        }

        void OnEnable()
        {
            dirtableCard.ValueUpdated += CardUpdated;
            CardUpdated(Card);
        }

        void OnDisable()
        {
            dirtableCard.ValueUpdated -= CardUpdated;
        }

        private void CardUpdated(Card obj)
        {
            if (obj.State == Card.States.None)
            {
                Group.alpha = 0;
            }
            else
            {
                Group.alpha = 1;
                
                Cover.gameObject.SetActive(obj.State == Card.States.Close);
                CardSprite.gameObject.SetActive(obj.State == Card.States.Open);
                
                CardSprite.sprite = UIConfiguration.Instance.Cards[obj.Suit * 13 + obj.Value];
            }
        }
    }
}