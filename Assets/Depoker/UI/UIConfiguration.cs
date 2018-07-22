using Depoker.UI.Views;
using UnityEngine;

namespace Depoker.UI
{
    public class UIConfiguration : MonoBehaviour
    {
        public static UIConfiguration Instance;
        
        public SitView[] Sits;
        public SitView My;
        public FlopView Flop;
        public Sprite[] Cards;

        void Awake()
        {
            Instance = this;
        }
    }
}