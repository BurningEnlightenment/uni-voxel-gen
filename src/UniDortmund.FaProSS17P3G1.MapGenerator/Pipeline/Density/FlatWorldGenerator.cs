using System;
using System.Collections.Generic;
using System.Text;
using NotEnoughTime.Utils;
using UniDortmund.FaProSS17P3G1.MapGenerator.Algorithm;
using UniDortmund.FaProSS17P3G1.MapGenerator.Model;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Pipeline.Density
{
    public class FlatWorldGenerator : IDensityGenerator
    {
        private int mWorldHeigth;

        public INoiseGenerator NoiseGenerator { set { } }

        public DensityGeneratorSettings Settings
        {
            set
            {
                if (value.SpecificSettingsCase != DensityGeneratorSettings.SpecificSettingsOneofCase.FlatWorldGenerator)
                {
                    throw new ArgumentException("not the correct settings type");
                }
                mWorldHeigth = value.FlatWorldGenerator.HeightLevel;
            }
        }

        public static FlatWorldGenerator Create(DensityGeneratorSettings _) => new FlatWorldGenerator();

        public void ApplyTo(WorldMap targetMap, int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
