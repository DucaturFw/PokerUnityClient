using System;
using Depoker.UI.Components;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Depoker.UI.Views
{
    public class CardView : MonoBehaviour
    {
        private const int SHEET_ROWS = 13;

        private Image shadow;

        public Image CardSprite;
        public Image Cover;
        public Image Space;
        
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

        void Awake()
        {
            Space = Instantiate(Cover, transform);
            Space.transform.SetSiblingIndex(101);
            CardSprite.transform.SetSiblingIndex(102);
            Cover.transform.SetSiblingIndex(103);
            
            Space.sprite = UIConfiguration.Instance.SpaceSprite;
            Space.color = UIConfiguration.Instance.SpaceColor;
            
        }

        void Start()
        {
            FixView();
        }

        private void FixView()
        {
        }

        void OnEnable()
        {
            shadow = transform.Find("shadow-close").GetComponent<Image>();
            dirtableCard.ValueUpdated += CardUpdated;
            CardUpdated(Card);
        }

        void OnDisable()
        {
            dirtableCard.ValueUpdated -= CardUpdated;
        }

        private Tweener Move(RectTransform t, Vector3 fromPoint, Vector3 toPoint)
        {
            t.localPosition = fromPoint;
            return t.DOAnchorPos(toPoint, 1f);
        }

        private Tweener MoveToCenter(RectTransform t)
        {
            var center = t.parent.InverseTransformPoint(UIConfiguration.Instance.FlopPivot.transform.position);
            return Move(t, Vector3.zero, center * 0.4f);
        }

        private Tweener MoveToPlace(RectTransform t, Vector3 from)
        {
            var localPoint = t.parent.InverseTransformPoint(from);
            return Move(t, localPoint * 0.6f, Vector3.zero);
        }
        
        private Tweener Fade(Image sprite, float from, float to)
        {
            return DOTween.To(value =>
            {
                var color = sprite.color;
                color.a = value;
                sprite.color = color;
            }, from, to, 1f);
        }

        private Tweener FadeIn(Image sprite)
        {
            return Fade(sprite, 0f, 1f);
        }
        
        private Tweener FadeOut(Image sprite)
        {
            return Fade(sprite, 1f, 0f);
        }

        private void CardUpdated(Card card)
        {
            var previous = dirtableCard.Previous;
            if (card.State == Card.States.None)
            {
                if (previous.State == Card.States.Open)
                {
                    CardSprite.transform.DOScale(0f, 3f);
                    MoveToCenter(CardSprite.rectTransform).onComplete += () => CardSprite.gameObject.SetActive(false);
                }
                else
                {
                    CardSprite.gameObject.SetActive(false);
                }
                
                Cover.gameObject.SetActive(false);
                Space.gameObject.SetActive(true);

                FadeIn(Space);
            } 
            else if (card.State == Card.States.Close)
            {
                Cover.gameObject.SetActive(true);
                
                if (previous.State == Card.States.None)
                {
                    MoveToPlace(Cover.rectTransform, UIConfiguration.Instance.DeckPivot.transform.position);
                    FadeIn(Cover);
                }
                
                CardSprite.gameObject.SetActive(false);
            }
            else
            {
                CardSprite.sprite = UIConfiguration.Instance.Cards[card.Suit * 13 + card.Value];
                CardSprite.gameObject.SetActive(true);
                
                if (previous.State == Card.States.None)
                {
                    MoveToPlace(CardSprite.rectTransform, UIConfiguration.Instance.DeckPivot.transform.position);
                    FadeIn(CardSprite);
                }
                
                Cover.gameObject.SetActive(false);
            }
//            
//            if (card.State == Card.States.None)
//            {
//                if (previous.State == Card.States.Open)
//                {
//                    CardSprite.transform.DOScale(0f, 3f);
//                    DOTween.To(value => CardSprite.color = new Color(1, 1, 1, value), 1, 0, 1f);
//                }
//                else
//                {
//                    CardSprite.gameObject.SetActive(false);
//                }
//                
//                if (!Cover)
//                {
//                    throw new NullReferenceException("Cover not found at " + gameObject);
//                }
//
//                if (!UIConfiguration.Instance)
//                {
//                    throw new NullReferenceException("UIConfiguration not found at" + gameObject);
//                }
//
//                Cover.gameObject.SetActive(true);
//                Cover.sprite = UIConfiguration.Instance.SpaceSprite;
//                Cover.color = UIConfiguration.Instance.SpaceColor;
//            }
//            else if (card.State == Card.States.Close)
//            {
////                CardSprite.sprite = UIConfiguration.Instance.Cards[card.Suit * 13 + card.Value];
//                if (previous.State == Card.States.Open)
//                {
//                    CardSprite.transform.DOScale(0f, 3f);
//                    DOTween.To(value => CardSprite.color = new Color(1, 1, 1, value), 1, 0, 1f);
//                }
//                else
//                {
//                    CardSprite.gameObject.SetActive(false);
//                }
//                
//                Cover.sprite = UIConfiguration.Instance.CoverSprite;
//                Cover.color = Color.white;
//
//                Cover.gameObject.SetActive(true);
//            }
//            else
//            {
//                Cover.sprite = UIConfiguration.Instance.SpaceSprite;
//                Cover.color = UIConfiguration.Instance.SpaceColor;
//                CardSprite.sprite = UIConfiguration.Instance.Cards[card.Suit * 13 + card.Value];
//                CardSprite.gameObject.SetActive(true);
//                
//                DOTween.To(value => shadow.color = new Color(1, 1, 1, value), 0, 1, 1f);
//
//                var from = CardSprite.transform.parent.InverseTransformPoint(UIConfiguration.Instance.DeckPivot
//                    .transform.position);
//                
//                CardSprite.transform.localPosition = from * 0.6f;
//                shadow.transform.localPosition = from * 0.6f;
//                
//                CardSprite.rectTransform.DOAnchorPos(Vector3.zero, 0.8f).onComplete += () => Cover.gameObject.SetActive(false);
//                
//                shadow.rectTransform.DOAnchorPos(Vector2.zero, 0.8f);
////                CardSprite.transform.DOShakePosition(1f, 13f);
//                DOTween.To(value => CardSprite.color = new Color(1, 1, 1, value), 0, 1, 1f);
//                
//
//                
//                
//                
//            }
        }
    }
}