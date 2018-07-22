using Depoker.UI.Views;
using UnityEngine;

namespace Depoker.UI
{
    public class UIConfiguration : MonoBehaviour
    {
        public static UIConfiguration Instance;
        public Color SpaceColor;
        public Sprite SpaceSprite;
        public Sprite CoverSprite;
        public SitView[] Sits;
        public SitView My;
        public FlopView Flop;
        public Sprite[] Cards;

        public Transform DeckPivot;
        public Transform FlopPivot;

        void Awake()
        {
            Instance = this;
        }
    }
}