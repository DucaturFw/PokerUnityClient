using System;
using System.Runtime.InteropServices;

namespace Depoker.UI.Components
{
    [StructLayout(LayoutKind.Explicit)]
    [System.Serializable]
    public unsafe struct Chips : IUIComponentData
    {   
//        [FieldOffset(0)]
//        public fixed int chips[10];   
        [FieldOffset(0)]
        public int Chips1;
        [FieldOffset(4)]
        public int Chips5;
        [FieldOffset(8)]
        public int Chips10;
        [FieldOffset(12)]
        public int Chips25;
        [FieldOffset(16)]
        public int Chips50;
        [FieldOffset(20)]
        public int Chips100;
        [FieldOffset(24)]
        public int Chips500;
        [FieldOffset(28)]
        public int Chips1000;
        [FieldOffset(32)]
        public int Chips5000;
        [FieldOffset(36)]
        public int Chips10000;
        
        
        public const int CHIPS_TYPES_COUNT = 10;
        
        public unsafe int this[int key]
        {
            get
            {
                if (key == 0)
                    return Chips1;
                if (key == 1)
                    return Chips5;
                if (key == 2)
                    return Chips10;
                if (key == 3)
                    return Chips25;
                if (key == 4)
                    return Chips50;
                if (key == 5)
                    return Chips100;
                if (key == 6)
                    return Chips500;
                if (key == 7)
                    return Chips1000;
                if (key == 8)
                    return Chips5000;
                if (key == 9)
                    return Chips10000;
                
                throw new ArgumentException();
            }
            set
            {
                if (key == 0)
                    Chips1 = value;
                if (key == 1)
                    Chips5 = value;
                if (key == 2)
                    Chips10 = value;
                if (key == 3)
                    Chips25 = value;
                if (key == 4)
                    Chips50 = value;
                if (key == 5)
                    Chips100 = value;
                if (key == 6)
                    Chips500 = value;
                if (key == 7)
                    Chips1000 = value;
                if (key == 8)
                    Chips5000 = value;
                if (key == 9)
                    Chips10000 = value;
                
            }
        }

        public override string ToString()
        {
            return $"1: {Chips1}, 5: {Chips5}, 10: {Chips10}, 25: {Chips25}, 50: {Chips50}, 100: {Chips100}, 500: {Chips500}, 1000: {Chips1000}, 10000: {Chips10000}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Chips)
            {
                var other = (Chips) obj;

                for (int index = 0; index < 10; index++)
                {
                    if (other[index] != this[index])
                        return false;
                }

                return true;
            }

            return false;
        }
    }
}