using System.Linq;
using UniDortmund.FaProSS17P3G1.MapGenerator.Model;

namespace UniDortmund.FaProSS17P3G1.MapGenerator
{
    public static class GeneratorUtils
    {
        public static readonly WorldBlock AirBlock = new WorldBlock
        {
            ParticleFields = {
                new ParticleField
                {
                    Type = ParticleType.PtAir,
                    NumberOfParticles = Constants.NumParticlesPerChunk
                }
            }
        };

        public static int Mod(int x, int m)
        {
            var r = x % m;
            return r < 0 ? r + m : r;
        }

        public static (int X, int Y) ToGlobal(int cx, int lx, int cy, int ly)
            => (cx * Constants.ChunkDimension + lx, cy * Constants.ChunkDimension + ly);

        public static (float X, float Y) ToGlobalF(int cx, int lx, int cy, int ly)
            => (cx * Constants.ChunkDimension + lx, cy * Constants.ChunkDimension + ly);
    }
}
