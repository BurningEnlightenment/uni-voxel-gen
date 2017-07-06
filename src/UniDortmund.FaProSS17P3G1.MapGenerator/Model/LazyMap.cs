using System;
using System.Collections.Generic;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Model
{
    public class LazyMap<T> where T: class, new()
    {
        private readonly List<List<T>> mData
            = new List<List<T>>();

        public T this[int x, int y]
        {
            get
            {
                var row = SafeRowAccess(ref x, ref y);

                var column = row[y];
                if (column == null)
                {
                    row[y] = column = new T();
                }

                return column;
            }
            set
            {
                var row = SafeRowAccess(ref x, ref y);
                row[y] = value;
            }
        }

        private List<T> SafeRowAccess(ref int x, ref int y)
        {
            x = MapCoord(x);
            y = MapCoord(y);

            if (x >= mData.Count)
            {
                mData.Resize(x + 1);
            }

            var row = mData[x];
            if (row == null)
            {
                mData[x] = row = new List<T>(y);
            }
            if (y >= row.Count)
            {
                row.Resize(y + 1);
            }
            return row;
        }

        private static int MapCoord(int coord)
            => (coord << 1) | (coord >> 31);
    }
}
