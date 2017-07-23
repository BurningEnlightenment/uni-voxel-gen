using System.Collections.Generic;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Algorithm
{
    public class CachingNoiseGenerator : INoiseGenerator
    {
        private readonly INoiseGenerator mImpl;
        private readonly Dictionary<(float, float), float> m2DCache
            = new Dictionary<(float, float), float>();
        private readonly Dictionary<(float, float, float), float> m3DCache
            = new Dictionary<(float, float, float), float>();

        public CachingNoiseGenerator(INoiseGenerator impl)
        {
            mImpl = impl;
        }

        public float this[float x, float y]
            => this[(x, y)];

        public float this[(float X, float Y) coords]
        {
            get
            {
                if (m2DCache.TryGetValue(coords, out var value))
                {
                    return value;
                }
                m2DCache[coords] = value = mImpl[coords];
                return value;
            }
        }

        public float this[float x, float y, float z]
            => this[(x, y, z)];

        public float this[(float X, float Y, float Z) coords]
        {
            get
            {
                if (m3DCache.TryGetValue(coords, out var value))
                {
                    return value;
                }
                m3DCache[coords] = value = mImpl[coords];
                return value;
            }
        }

        public ulong Seed
        {
            get => mImpl.Seed;
            set => mImpl.Seed = value;
        }
    }
}
