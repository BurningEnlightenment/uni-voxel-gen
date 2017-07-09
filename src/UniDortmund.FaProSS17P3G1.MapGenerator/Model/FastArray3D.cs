using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniDortmund.FaProSS17P3G1.MapGenerator.Algorithm;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Model
{
    public interface IReadOnlyArray3D<T> : IEnumerable<(int X, int Y, int Z, T Value)>
    {
        T this[int idx] { get; }
        T this[(int X, int Y, int Z) point] { get; }
        T this[int x, int y, int z] { get; }

        int DimensionSize { get; }
        int TotalSize { get; }
    }

    public interface IArray3D<T> : IReadOnlyArray3D<T>
    {
        new T this[int idx] { get; set; }
        new T this[(int X, int Y, int Z) point] { get; set; }
        new T this[int x, int y, int z] { get; set; }
    }

    public class FastArray3D<T> : IArray3D<T>
    {
        public const int Arity = 2;

        private readonly T[] mData;
        private readonly int mSquaredDimensionSize;

        public FastArray3D(int size)
        {
            DimensionSize = size;
            mSquaredDimensionSize = DimensionSize * DimensionSize;
            TotalSize = mSquaredDimensionSize * DimensionSize;
            mData = new T[TotalSize];
        }

        public FastArray3D(int size, T initialValue)
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

        public T this[(int X, int Y, int Z) point]
        {
            get => this[point.X, point.Y, point.Z];
            set => this[point.X, point.Y, point.Z] = value;
        }

        public T this[int x, int y, int z]
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
                if (z < 0 || z >= DimensionSize)
                {
                    throw new ArgumentOutOfRangeException(nameof(z));
                }
                return mData[x + y * DimensionSize + z * mSquaredDimensionSize];
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
                if (z < 0 || z >= DimensionSize)
                {
                    throw new ArgumentOutOfRangeException(nameof(z));
                }
                mData[x + y * DimensionSize + z * mSquaredDimensionSize] = value;
            }
        }

        public int DimensionSize { get; }
        public int TotalSize { get; }

        public IEnumerable<T> Values() => mData;

        public IEnumerator<(int X, int Y, int Z, T Value)> GetEnumerator()
        {
            var n = -1;
            return Helpers.CubeRange(DimensionSize)
                .Select(point => (point.X, point.Y, point.Z, mData[++n]))
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IReadOnlyArray3D<T> ImmutableWrapper()
            => new FastReadOnlyArray3DWrapper<T>(this);
    }

    public class FastReadOnlyArray3DWrapper<T> : IReadOnlyArray3D<T>
    {
        private readonly IReadOnlyArray3D<T> mData;

        public FastReadOnlyArray3DWrapper(IReadOnlyArray3D<T> data)
        {
            mData = data;
        }

        public IEnumerator<(int X, int Y, int Z, T Value)> GetEnumerator() => mData.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public T this[int idx] => mData[idx];
        public T this[(int X, int Y, int Z) point] => mData[point];

        public T this[int x, int y, int z] => mData[x, y, z];

        public int DimensionSize => mData.DimensionSize;
        public int TotalSize => mData.TotalSize;
    }

    public static class Array3DExtensions
    {
        public static IArray3D<T2> MapValues<T1, T2>(this IReadOnlyArray3D<T1> self,
            Func<T1, T2> transform)
        {
            var mapped = new FastArray3D<T2>(self.DimensionSize);
            foreach (var point in self)
            {
                mapped[point.X, point.Y, point.Z] = transform(point.Value);
            }
            return mapped;
        }

        public static IArray3D<T2> MapValues<T1, T2>(this IReadOnlyArray3D<T1> self,
            Func<int, int, int, T1, T2> transform)
        {
            var mapped = new FastArray3D<T2>(self.DimensionSize);
            foreach (var point in self)
            {
                mapped[point.X, point.Y, point.Z] = transform(point.X, point.Y, point.Z, point.Value);
            }
            return mapped;
        }

        public static void Fill<T>(this IArray3D<T> self, Func<T> generate)
        {
            for (var i = 0; i < self.TotalSize; ++i)
            {
                self[i] = generate();
            }
        }
        public static void Fill<T>(this IArray3D<T> self, Func<int, int, int, T> generate)
        {
            var i = -1;
            foreach (var point in Helpers.CubeRange(self.DimensionSize))
            {
                self[++i] = generate(point.X, point.Y, point.Z);
            }
        }

        public static void Mutate<T>(this IArray3D<T> self, Func<T, T> mutate)
        {
            for (var i = 0; i < self.TotalSize; ++i)
            {
                self[i] = mutate(self[i]);
            }
        }

        public static void Mutate<T>(this IArray3D<T> self, Func<int, int, int, T, T> mutate)
        {
            var i = 0;
            foreach (var point in Helpers.CubeRange(self.DimensionSize))
            {
                self[i] = mutate(point.X, point.Y, point.Z, self[i]);
                ++i;
            }
        }
    }
}
