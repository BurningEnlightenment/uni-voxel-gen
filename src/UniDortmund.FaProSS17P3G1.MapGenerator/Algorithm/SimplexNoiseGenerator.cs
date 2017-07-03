using System;
using System.Collections.Generic;
using System.Text;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Algorithm
{
    public class SimplexNoiseGenerator : INoiseGenerator
    {
        private readonly FastNoise mImpl;

        public SimplexNoiseGenerator(ulong seed)
        {
            mImpl = new FastNoise((int)seed);
        }

        float INoiseGenerator.this[float x, float y]
            => mImpl.GetSimplex(x, y);

        float INoiseGenerator.this[float x, float y, float z]
            => mImpl.GetSimplex(x, y, z);
    }
}
