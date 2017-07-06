using System.Collections.Generic;
using System.Linq;

namespace UniDortmund.FaProSS17P3G1.MapGenerator
{
    public static class LibExtensions
    {
        public static void Resize<T>(this List<T> list, int size, T elem = default(T))
        {
            var current = list.Count;
            if (current > size)
            {
                list.RemoveRange(size, current - size);
            }
            else if (current < size)
            {
                if (list.Capacity < size)
                {
                    list.Capacity = size;
                }
                list.AddRange(Enumerable.Repeat<T>(elem, size - current));
            }
        }

        public static int ZigZagEnc(int val)
            => (val << 1) ^ (val >> 31);

        public static int ZigZagDec(int val)
            => (int) ((uint) val >> 1) ^ -(val & 1);
    }
}
