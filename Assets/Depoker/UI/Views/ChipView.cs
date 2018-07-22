using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Depoker.UI.Views
{
    public class ChipView : MonoBehaviour
    {
        public Color[] ChipValueColors;
        public Image ChipSprite;
        private DirtableValue<int> color = new DirtableValue<int>();

        public int Color
        {
            get { return color.Value; }
            set { color.Value = value; }
        }

        void OnEnable()
        {
            Color = -1;
            color.ValueUpdated += UpdateChipColor;

            (transform.GetChild(0) as RectTransform).anchoredPosition = Vector2.up * Random.Range(25f, 35f); 
            (transform.GetChild(0) as RectTransform).DOAnchorPos(Vector3.zero, Random.Range(.3f, .5f));
        }

        void OnDisable()
        {
            color.ValueUpdated -= UpdateChipColor;
        }

        private void UpdateChipColor(int obj)
        {
            ChipSprite.color = ChipValueColors[obj];
        }
    }
}