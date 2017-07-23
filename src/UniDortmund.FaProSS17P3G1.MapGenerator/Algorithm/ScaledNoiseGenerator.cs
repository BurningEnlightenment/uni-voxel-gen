using static UniDortmund.FaProSS17P3G1.MapGenerator.Constants;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Algorithm
{
    public class ScaledNoiseGenerator : INoiseGenerator
    {
        private readonly INoiseGenerator mImplementation;
        private readonly float mScale;

        public ScaledNoiseGenerator(INoiseGenerator implementation, float scale)
        {
            mImplementation = implementation;
            mScale = scale;
        }


        public float this[float x, float y]
            => mImplementation[x * mScale, y * mScale];

        public float this[float x, float y, float z]
            => mImplementation[x * mScale, y * mScale, z * mScale];

        public float this[(float X, float Y) coords]
            => this[coords.X, coords.Y];

        public float this[(float X, float Y, float Z) coords]
            => this[coords.X, coords.Y, coords.Z];

        public ulong Seed
        {
            get => mImplementation.Seed;
            set => mImplementation.Seed = value;
        }
    }
}
