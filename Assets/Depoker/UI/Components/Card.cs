using System.Runtime.InteropServices;

namespace Depoker.UI.Components
{
    [System.Serializable]
    [StructLayout(LayoutKind.Explicit)]
    public struct Card
    {
        public enum States
        {
            None,
            Close,
            Open
        }
        
        [FieldOffset(0)]
        public States State;
        [FieldOffset(4)]
        public int Suit;
        [FieldOffset(8)]
        public int Value;

        public override string ToString()
        {
            return $"Card: {Suit}-{Value} [{(State > 0 ? "Open" : "Close")}]";
        }
    }
}