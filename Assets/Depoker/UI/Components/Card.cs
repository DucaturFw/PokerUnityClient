using System.Runtime.InteropServices;

namespace Depoker.UI.Components
{
    [System.Serializable]
    [StructLayout(LayoutKind.Explicit)]
    public struct Card
    {
        [FieldOffset(0)]
        public int Open;
        [FieldOffset(4)]
        public int Suit;
        [FieldOffset(8)]
        public int Value;

        public override string ToString()
        {
            return $"Card: {Suit}-{Value} [{(Open > 0 ? "Open" : "Close")}]";
        }
    }
}