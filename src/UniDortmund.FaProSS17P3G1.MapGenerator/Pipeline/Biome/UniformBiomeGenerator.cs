using System;
using System.Collections.Generic;
using System.Text;
using NotEnoughTime.Utils;
using UniDortmund.FaProSS17P3G1.MapGenerator.Algorithm;
using UniDortmund.FaProSS17P3G1.MapGenerator.Model;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Pipeline.Biome
{
    public class UniformBiomeGenerator : IBiomeGenerator
    {
        private BiomeType mBiomeType;

        private UniformBiomeGenerator(BiomeType biomeType)
        {
            mBiomeType = biomeType;
        }

        private UniformBiomeGenerator(BiomeGeneratorSettings settings)
        {
            mBiomeType = settings.UniformGenerator.TargetBiomeType;
        }

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

        public static UniformBiomeGenerator Create(BiomeGeneratorSettings settings)
            => new UniformBiomeGenerator(settings);

        public void ApplyTo(WorldMap targetMap, int x, int y)
        {
            var col = targetMap[x, y];
            for (var i = 0; i < Constants.ChunkDimension; ++i)
            {
                for (var j = 0; j < Constants.ChunkDimension; ++j)
                {
                    col[i, j] = mBiomeType;
                }
            }
        }
    }
}
