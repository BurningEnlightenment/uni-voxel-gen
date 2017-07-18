﻿namespace UniDortmund.FaProSS17P3G1.MapGenerator.Algorithm
{
    public class GridWithJitter
    {
        private readonly INoiseGenerator mXNoise;
        private readonly INoiseGenerator mYNoise;
        private readonly float mScale;

        public GridWithJitter(INoiseGenerator xNoise, INoiseGenerator yNoise, float scale = 0.5f)
        {
            mXNoise = xNoise;
            mYNoise = yNoise;
            mScale = scale;
        }

        public (float X, float Y) this[float x, float y]
            => (x + mXNoise[x, y] * mScale, y + mYNoise[x, y] * mScale);
    }
}
