namespace Depoker.UI.Components
{
    [System.Serializable]
    public struct State : IUIComponentData
    {
        public enum States
        {
            Wait,
            Fold,
            Check
        }
    }
}