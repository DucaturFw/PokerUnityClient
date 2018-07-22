using System;
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

        void Start()
        {
            FixView();
        }

        private void FixView()
        {
            Debug.Log(Cover);
            Cover.gameObject.SetActive(true);
            CardSprite.gameObject.SetActive(true);
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
                if (!Cover)
                {
                    throw new NullReferenceException("Cover not found at " + gameObject);
                }

                if (!UIConfiguration.Instance)
                {
                    throw new NullReferenceException("UIConfiguration not found at" + gameObject);
                }
                
                Cover.sprite = UIConfiguration.Instance.SpaceSprite;
                Cover.color = UIConfiguration.Instance.SpaceColor;
            }
            else
            {
                Cover.sprite = UIConfiguration.Instance.CoverSprite;
                Cover.color = Color.white;
                
                Cover.gameObject.SetActive(obj.State == Card.States.Close);
                CardSprite.gameObject.SetActive(obj.State == Card.States.Open);
                
                CardSprite.sprite = UIConfiguration.Instance.Cards[obj.Suit * 13 + obj.Value];
            }
        }
    }
}