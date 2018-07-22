using Depoker.UI.Components;
using UnityEngine;

namespace Depoker.UI.Views
{
    public class DeckView : MonoBehaviour
    {
        [SerializeField] private CardView cardPrefab;
        
        void OnEnable()
        {
            FitChildrenCount(transform, 15, cardPrefab);
            TweekRotation();
            InitCards();
        }

        private void InitCards()
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<CardView>().Card = new Card() {State = Card.States.Close};
            }
        }

        private void TweekRotation()
        {
            var index = 0;
            foreach (Transform child in transform)
            {
                child.localRotation = Quaternion.Euler(Vector3.forward * Random.Range(-1.5f, 1.5f));
                child.localPosition = Vector3.up * 3 * index++;
            }
        }

        private static void FitChildrenCount(Transform parent, int chipsSum, Object prefab)
        {
            var deleteCount = parent.childCount - chipsSum;
            var deleteIndex = 0;
            while (deleteIndex < deleteCount)
            {
                Destroy(parent.GetChild(deleteIndex++).gameObject);
            }
            
            var createCount = chipsSum - parent.childCount;

            while (createCount > 0)
            {
                Instantiate(prefab, parent);
                createCount--;
            }
        }
    }
}