using System;
using UniDortmund.FaProSS17P3G1.MapGenerator.Algorithm;
using UniDortmund.FaProSS17P3G1.MapGenerator.Model;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Pipeline.Biome
{
    public class TwoStageBiomeGenerator : IBiomeGenerator
    {
        public INoiseGenerator NoiseGenerator { private get; set; }

        public void ApplyTo(WorldMap targetMap, int x, int y)
        {
            throw new NotImplementedException();
        }

        public static TwoStageBiomeGenerator Create(BiomeGeneratorSettings settings)
            => throw new NotImplementedException();
    }
}
