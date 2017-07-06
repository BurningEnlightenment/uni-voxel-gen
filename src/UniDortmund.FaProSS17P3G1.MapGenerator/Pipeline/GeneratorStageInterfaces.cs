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
    }

    public interface IDensityGenerator : INoisyGenerator
    {
    }

    public interface IDetailsGenerator : INoisyGenerator
    {
    }

    public interface ITerrainGenerator : INoisyGenerator
    {
    }
}
