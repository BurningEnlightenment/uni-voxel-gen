using System;
using System.Collections.Generic;
using System.Text;
using NotEnoughTime.Utils;
using UniDortmund.FaProSS17P3G1.MapGenerator.Algorithm;
using UniDortmund.FaProSS17P3G1.MapGenerator.Model;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Pipeline
{
    public interface INoisyGenerator
    {
        INoiseGenerator NoiseGenerator { set; }

        void ApplyTo(WorldMap targetMap, int x, int y);
    }

    public interface IBiomeGenerator : INoisyGenerator
    {
        BiomeGeneratorSettings Settings { set; }
    }

    public interface IDensityGenerator : INoisyGenerator
    {
        DensityGeneratorSettings Settings { set; }
    }

    public interface IDetailsGenerator : INoisyGenerator
    {
        DetailsGeneratorSettings Settings { set; }
    }

    public interface ITerrainGenerator : INoisyGenerator
    {
        TerrainGeneratorSettings Settings { set; }
    }
}
