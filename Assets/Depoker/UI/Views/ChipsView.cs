using Depoker.UI.Components;
using UnityEngine;
using UnityScript.Steps;

namespace Depoker.UI.Views
{
    public class ChipsView : MonoBehaviour
    {
        public Chips current;
        private DirtableValue<Chips> data = new DirtableValue<Chips>();

        public Chips Chips
        {
            get { return data.Value; }
            set { data.Value = value; }
        }

        public ChipView ChipPrefab;

        void Awake()
        {
            FitChildrenCount(0);
        }

        void OnEnable()
        {
            data.ValueUpdated += ChipsUpdated;
        }

        void OnDisable()
        {
            data.ValueUpdated -= ChipsUpdated;
        }

        private void ChipsUpdated(Chips obj)
        {
            current = obj;
            var chipsSum = CalculateChipsSum(obj);
            FitChildrenCount(chipsSum);
            var next = 0;

            for (int color = 0; color < Components.Chips.CHIPS_TYPES_COUNT; color++)
            {
                for (int index = 0; index < obj[color]; index++)
                {
                    transform.GetChild(next++).GetComponent<ChipView>().Color = color;
                }
                
            }
        }

        private void FitChildrenCount(int chipsSum)
        {
            var deleteCount = transform.childCount - chipsSum;
            var deleteIndex = 0;
            while (deleteIndex < deleteCount)
            {
                Destroy(transform.GetChild(deleteIndex++).gameObject);
            }
            
            var createCount = chipsSum - transform.childCount;

            while (createCount > 0)
            {
                Instantiate(ChipPrefab, transform);
                createCount--;
            }
        }

        private int CalculateChipsSum(Chips chips)
        {
            var sum = 0;
            for (int index = 0; index < Chips.CHIPS_TYPES_COUNT; index++)
            {
                sum += chips[index];
            }

            return sum;
        }
    }
}