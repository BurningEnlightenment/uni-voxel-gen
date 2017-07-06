using System;
using System.Collections.Generic;
using System.Text;
using NotEnoughTime.Utils;
using UniDortmund.FaProSS17P3G1.MapGenerator.Algorithm;
using UniDortmund.FaProSS17P3G1.MapGenerator.Model;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Pipeline.Biome
{
    public class UniformBiomeGenerator : IBiomeGenerator, IDeepClonable<UniformBiomeGenerator>
    {
        private BiomeType mBiomeType;

        public INoiseGenerator NoiseGenerator { set { } }

        public BiomeGeneratorSettings Settings
        {
            set
            {
                if (value.SpecificSettingsCase != BiomeGeneratorSettings.SpecificSettingsOneofCase.UniformGenerator)
                {
                    throw new ArgumentException("invalid settings type");
                }
                mBiomeType = value.UniformGenerator.TargetBiomeType;
            }
        }

        public static UniformBiomeGenerator Create() => new UniformBiomeGenerator();

        public UniformBiomeGenerator Clone()
        {
            return new UniformBiomeGenerator();
        }
        IBiomeGenerator IDeepClonable<IBiomeGenerator>.Clone() => Clone();
    }
}
