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
    }
}
