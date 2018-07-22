using System.Runtime.InteropServices;
using Unity.Entities;

namespace Depoker.UI.Components
{
    [System.Serializable]
    [StructLayout(LayoutKind.Explicit)]
    public struct Cards : IComponentData
    {
        [FieldOffset(0)]
        public Card Left;
        [FieldOffset(12)]
        public Card Right;

        public override string ToString()
        {
            return Left.ToString() + " " + Right.ToString();
        }
    }
}