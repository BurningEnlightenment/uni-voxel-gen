using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static UniDortmund.FaProSS17P3G1.MapGenerator.LibExtensions;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Model
{
    public class FlexLazyMap<T> : IEnumerable<(int X, int Y, T Value)>
        where T: class
    {
        private readonly List<List<T>> mData
            = new List<List<T>>();

        public int OriginX { get; private set; }
        public int OriginY { get; private set; }
        public int SizeX { get; private set; }
        public int SizeY { get; private set; }
        public int MaxX => OriginX + SizeX - 1;
        public int MaxY => OriginY + SizeY - 1;

        public Func<T> DefaultValueProvider { get; set; }

        public T this[int x, int y]
        {
            get
            {
                var row = SafeRowAccess(ref x, ref y);

                var column = row[x];
                if (column == null)
                {
                    row[x] = column = DefaultValueProvider();
                }

                return column;
            }
            set
            {
                var row = SafeRowAccess(ref x, ref y);
                row[x] = value;
            }
        }

        private List<T> SafeRowAccess(ref int x, ref int y)
        {
            UpdateDimensions(x, y);
            x = MapCoord(x);
            y = MapCoord(y);

            if (y >= mData.Count)
            {
                mData.Resize(y + 1);
            }

            var row = mData[y];
            if (row == null)
            {
                mData[y] = row = new List<T>(x + 1);
            }
            if (x >= row.Count)
            {
                row.Resize(x + 1);
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
            for (var y = OriginY; y <= MaxY; ++y)
            {
                var row = mData[MapCoord(y)];
                if (row == null)
                {
                    continue;
                }

                for (var x = OriginX; x <= MaxX; ++x)
                {
                    var j = MapCoord(x);
                    if (j >= row.Count)
                    {
                        continue;
                    }
                    var cell = row[j];
                    if (cell == null)
                    {
                        continue;
                    }

                    yield return (x, y, cell);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }

    public class LazyMap<T> : FlexLazyMap<T>
        where T : class, new()
    {
        public LazyMap()
        {
            DefaultValueProvider = () => new T();
        }
    }
}
