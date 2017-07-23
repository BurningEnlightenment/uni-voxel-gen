using System;
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
                for (; current < size; ++current)
                {
                    list.Add(elem);
                }
            }
        }

        public static int LowerBound<TKey, TValue>(this SortedList<TKey, TValue> self, TKey value)
        {
            if (self == null)
            {
                throw new ArgumentNullException(nameof(self));
            }
            var comp = self.Comparer;
            var keyList = self.Keys;
            var lo = 0;
            var hi = keyList.Count - 1;
            while (lo < hi)
            {
                var m = lo + (hi - lo) / 2;
                if (comp.Compare(keyList[m], value) < 0)
                {
                    lo = m + 1;
                }
                else
                {
                    hi = m - 1;
                }
            }
            if (comp.Compare(keyList[lo], value) < 0)
            {
                lo++;
            }
            return lo;
        }

        public static int ZigZagEnc(int val)
            => (val << 1) ^ (val >> 31);

        public static int ZigZagDec(int val)
            => (int) ((uint) val >> 1) ^ -(val & 1);
    }
}
