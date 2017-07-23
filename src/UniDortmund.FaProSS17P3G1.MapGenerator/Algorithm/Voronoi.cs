using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SixLabors.Primitives;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Algorithm
{
    public class Voronoi
    {
        private readonly GridWithJitterCache mGridWithJitter;
        private readonly float mScale;

        public Voronoi(INoiseGenerator xNoise, INoiseGenerator yNoise, float scale = 0.5f)
        {
            mGridWithJitter = new GridWithJitterCache(new GridWithJitter(xNoise, yNoise, scale));
            mScale = 2 * scale;
        }

        public (float X, float Y) FindClosest((float X, float Y) coord)
            => FindClosest(coord.X, coord.Y);

        public (float X, float Y) FindClosest(float x, float y)
        {
            var minDist = float.PositiveInfinity;
            var nearestPoint = new Vector2();

            var origX = GridOrigin(x);
            var origY = GridOrigin(y);
            
            var point = new Vector2(x, y);
            var orig = new Vector2(origX, origY);
            var xScale = new Vector2(mScale, 0);
            var yScale = new Vector2(0, mScale);
            for (var i = 0; i < 6; ++i)
            {
                orig.Y = origY;
                for (var j = 0; j < 6; ++j)
                {
                    var distorted = mGridWithJitter[orig];
                    var dist = Vector2.Distance(point, distorted);
                    if (dist < minDist)
                    {
                        minDist = dist;
                        nearestPoint = distorted;
                    }
                    orig += yScale;
                }
                orig += xScale;
            }
            return (nearestPoint.X, nearestPoint.Y);
        }

        private static float Distance(Vector2 a, Vector2 b)
            => (float)Math.Sqrt(SquareDiff(a.X, b.X) + SquareDiff(a.Y, b.Y));

        private static float SquareDiff(float l, float r)
        {
            l -= r;
            return l * l;
        }

        private float GridOrigin(float v)
            => mScale * ((int) (v / mScale) - 2);
    }
}
