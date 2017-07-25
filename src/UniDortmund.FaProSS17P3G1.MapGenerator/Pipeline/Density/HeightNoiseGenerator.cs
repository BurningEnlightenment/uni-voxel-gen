using System;
using UniDortmund.FaProSS17P3G1.MapGenerator.Algorithm;
using UniDortmund.FaProSS17P3G1.MapGenerator.Model;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Pipeline.Density
{
    public class HeightNoiseGenerator : IDensityGenerator
    {
        public HeightNoiseGenerator(DensityGeneratorSettings settings)
        {
            
        }

        public Func<ulong, INoiseGenerator> NoiseGeneratorFactory { private get; set; }

        public static HeightNoiseGenerator Create(DensityGeneratorSettings settings)
            => new HeightNoiseGenerator(settings);

        public void ApplyTo(WorldMap targetMap, int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
