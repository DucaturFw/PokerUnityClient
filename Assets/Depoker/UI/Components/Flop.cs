using System.Runtime.InteropServices;
using Unity.Entities;

namespace Depoker.UI.Components
{
    [System.Serializable]
    [StructLayout(LayoutKind.Explicit)]
    public struct Flop : IComponentData
    {
        [FieldOffset(0)]
        public Card First;
        [FieldOffset(12)]
        public Card Second;
        [FieldOffset(24)]
        public Card Third;
        [FieldOffset(36)]
        public Card Fourth;
        [FieldOffset(48)]
        public Card Fiveth;
    }
}