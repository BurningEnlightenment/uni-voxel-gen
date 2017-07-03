using System;
using System.Collections.Generic;
using System.Text;
using NotEnoughTime.Utils;
using UniDortmund.FaProSS17P3G1.MapGenerator.Algorithm;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Pipeline.Biome
{
    public class UniformBiomeGenerator : IBiomeGenerator, IDeepClonable<UniformBiomeGenerator>
    {
        public INoiseGenerator NoiseGenerator { set { } }

        public static UniformBiomeGenerator Create() => new UniformBiomeGenerator();

        public UniformBiomeGenerator Clone()
        {
            return new UniformBiomeGenerator();
        }
        IBiomeGenerator IDeepClonable<IBiomeGenerator>.Clone() => Clone();
    }
}
