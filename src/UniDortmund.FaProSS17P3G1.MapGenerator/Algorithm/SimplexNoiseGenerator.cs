using System;
using System.Collections.Generic;
using System.Text;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Algorithm
{
    public class SimplexNoiseGenerator : INoiseGenerator
    {
        private readonly FastNoise mImpl;
        private ulong mSeed;

        public SimplexNoiseGenerator(ulong seed)
        {
            mImpl = new FastNoise((int)seed);
            mImpl.SetNoiseType(FastNoise.NoiseType.Simplex);
        }

        public float this[float x, float y]
            => mImpl.GetNoise(x, y);

        public float this[float x, float y, float z]
            => mImpl.GetNoise(x, y, z);

        public float this[(float X, float Y) coords]
            => this[coords.X, coords.Y];

        public float this[(float X, float Y, float Z) coords]
            => this[coords.X, coords.Y, coords.Z];

        public ulong Seed
        {
            get => mSeed;
            set
            {
                mImpl.SetSeed((int)value);
                mSeed = value;
            }
        }
    }
}
