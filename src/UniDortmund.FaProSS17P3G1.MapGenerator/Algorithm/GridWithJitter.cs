using System.Collections.Generic;
using System.Numerics;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Algorithm
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

        public Vector2 this[Vector2 point]
            => point + new Vector2(mXNoise[point.X, point.Y] * mScale, mYNoise[point.X, point.Y] * mScale);
    }
}
