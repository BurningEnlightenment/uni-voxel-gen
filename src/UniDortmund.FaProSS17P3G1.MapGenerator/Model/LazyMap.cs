using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static UniDortmund.FaProSS17P3G1.MapGenerator.LibExtensions;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Model
{
    public class LazyMap<T> : IEnumerable<(int X, int Y, T Value)>
        where T: class, new()
    {
        private readonly List<List<T>> mData
            = new List<List<T>>();

        public int OriginX { get; private set; }
        public int OriginY { get; private set; }
        public int SizeX { get; private set; }
        public int SizeY { get; private set; }
        public int MaxX => OriginX + SizeX - 1;
        public int MaxY => OriginY + SizeY - 1;

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
            UpdateDimensions(x, y);
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

        private void UpdateDimensions(int x, int y)
        {
            var maxX = MaxX;
            if (x < OriginX)
            {
                var diff = OriginX - x;
                OriginX = x;
                SizeX += diff;
            }
            else if (x > maxX)
            {
                var diff = x - maxX;
                SizeX += diff;
            }

            var maxY = MaxY;
            if (y < OriginY)
            {
                var diff = OriginY - y;
                OriginY = y;
                SizeY += diff;
            }
            else if (y > maxY)
            {
                var diff = y - maxY;
                SizeY += diff;
            }
        }

        private static int MapCoord(int coord)
            => ZigZagEnc(coord);

        public IEnumerator<(int X, int Y, T Value)> GetEnumerator()
        {
            for (var i = 0; i < mData.Count; ++i)
            {
                var row = mData[i];
                if (row == null)
                {
                    continue;
                }

                for (var j = 0; j < row.Count; j++)
                {
                    var value = row[j];
                    yield return (ZigZagDec(j), ZigZagDec(i), value);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}
