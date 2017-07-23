using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Algorithm
{
    public class GridWithJitterCache
    {
        private readonly GridWithJitter mGridWithJitter;
        private readonly Dictionary<Vector2, Vector2> mCache
            = new Dictionary<Vector2, Vector2>();

        public GridWithJitterCache(GridWithJitter gridWithJitter)
        {
            mGridWithJitter = gridWithJitter;
        }

        public (float X, float Y) this[float x, float y]
        {
            get
            {
                var value = this[new Vector2(x, y)];
                return (value.X, value.Y);
            }
        }

        public Vector2 this[Vector2 coord]
        {
            get
            {
                if (mCache.TryGetValue(coord, out var value))
                {
                    return value;
                }
                value = mGridWithJitter[coord];
                mCache[coord] = value;
                return value;
            }
        }

        public void ClearCache()
            => mCache.Clear();
    }
}
