using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniDortmund.FaProSS17P3G1.MapGenerator.Algorithm;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Model
{
    public interface IReadOnlyArray2D<T> : IEnumerable<(int X, int Y, T Value)>
    {
        T this[int idx] { get; }
        T this[(int X, int Y) point] { get; }
        T this[int x, int y] { get; }

        int DimensionSize { get; }
        int TotalSize { get; }
    }

    public interface IArray2D<T> : IReadOnlyArray2D<T>
    {
        new T this[int idx] { get; set; }
        new T this[(int X, int Y) point] { get; set; }
        new T this[int x, int y] { get; set; }
    }

    public class FastArray2D<T> : IArray2D<T>
    {
        public const int Arity = 2;

        private readonly T[] mData;

        public FastArray2D(int size)
        {
            DimensionSize = size;
            TotalSize = DimensionSize * DimensionSize;
            mData = new T[TotalSize];
        }

        public FastArray2D(int size, T initialValue)
            : this(size)
        {
            for (var i = 0; i < mData.Length; ++i)
            {
                mData[i] = initialValue;
            }
        }

        public T this[int idx]
        {
            get => mData[idx];
            set => mData[idx] = value;
        }

        public T this[(int X, int Y) point]
        {
            get => this[point.X, point.Y];
            set => this[point.X, point.Y] = value;
        }

        public T this[int x, int y]
        {
            get
            {
                if (x < 0 || x >= DimensionSize)
                {
                    throw new ArgumentOutOfRangeException(nameof(x));
                }
                if (y < 0 || y >= DimensionSize)
                {
                    throw new ArgumentOutOfRangeException(nameof(y));
                }
                return mData[MapCoordinateToIndex(x, y)];
            }
            set
            {
                if (x < 0 || x >= DimensionSize)
                {
                    throw new ArgumentOutOfRangeException(nameof(x));
                }
                if (y < 0 || y >= DimensionSize)
                {
                    throw new ArgumentOutOfRangeException(nameof(y));
                }
                mData[MapCoordinateToIndex(x, y)] = value;
            }
        }

        public int DimensionSize { get; }
        public int TotalSize { get; }

        public IEnumerator<(int X, int Y, T Value)> GetEnumerator()
        {
            var i = -1;
            return Helpers.SquareRange(DimensionSize)
                .Select(point => (point.X, point.Y, mData[++i]))
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IReadOnlyArray2D<T> ImmutableWrapper()
            => new FastReadOnlyArray2DWrapper<T>(this);

        public int MapCoordinateToIndex(int x, int y)
            => x + y * DimensionSize;
    }

    public class FastReadOnlyArray2DWrapper<T> : IReadOnlyArray2D<T>
    {
        private readonly IReadOnlyArray2D<T> mData;

        public FastReadOnlyArray2DWrapper(IReadOnlyArray2D<T> data)
        {
            mData = data;
        }

        public IEnumerator<(int X, int Y, T Value)> GetEnumerator() => mData.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public T this[int idx] => mData[idx];
        public T this[(int X, int Y) point] => mData[point];
        public T this[int x, int y] => mData[x, y];

        public int DimensionSize => mData.DimensionSize;
        public int TotalSize => mData.TotalSize;
    }

    public static class Array2DExtensions
    {
        public static IArray2D<T2> MapValues<T1, T2>(this IReadOnlyArray2D<T1> self, Func<T1, T2> transform)
        {
            var mapped = new FastArray2D<T2>(self.DimensionSize);
            foreach (var point in self)
            {
                mapped[point.X, point.Y] = transform(point.Value);
            }
            return mapped;
        }
        public static IArray2D<T2> MapValues<T1, T2>(this IReadOnlyArray2D<T1> self,
            Func<int, int, T1, T2> transform)
        {
            var mapped = new FastArray2D<T2>(self.DimensionSize);
            foreach (var point in self)
            {
                mapped[point.X, point.Y] = transform(point.X, point.Y, point.Value);
            }
            return mapped;
        }

        public static void Fill<T>(this IArray2D<T> self, Func<T> generate)
        {
            for (var i = 0; i < self.TotalSize; ++i)
            {
                self[i] = generate();
            }
        }
        public static void Fill<T>(this IArray2D<T> self, Func<int, int, T> generate)
        {
            var i = -1;
            foreach (var point in Helpers.SquareRange(self.DimensionSize))
            {
                self[++i] = generate(point.X, point.Y);
            }
        }

        public static void Mutate<T>(this IArray2D<T> self, Func<T, T> mutate)
        {
            for (var i = 0; i < self.TotalSize; ++i)
            {
                self[i] = mutate(self[i]);
            }
        }

        public static void Mutate<T>(this IArray2D<T> self, Func<int, int, T, T> mutate)
        {
            var i = 0;
            foreach (var point in Helpers.SquareRange(self.DimensionSize))
            {
                self[i] = mutate(point.X, point.Y, self[i]);
                ++i;
            }
        }
    }
}
