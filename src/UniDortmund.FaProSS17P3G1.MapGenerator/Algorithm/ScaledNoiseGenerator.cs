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


        float INoiseGenerator.this[float x, float y]
            => mImplementation[x * mScale, y * mScale];

        float INoiseGenerator.this[float x, float y, float z]
            => mImplementation[x * mScale, y * mScale, z * mScale];

        public ulong Seed
        {
            get => mImplementation.Seed;
            set => mImplementation.Seed = value;
        }
    }
}
