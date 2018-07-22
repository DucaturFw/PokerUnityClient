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