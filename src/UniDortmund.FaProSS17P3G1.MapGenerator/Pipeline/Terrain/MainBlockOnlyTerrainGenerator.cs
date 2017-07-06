using System;
using NotEnoughTime.Utils;
using UniDortmund.FaProSS17P3G1.MapGenerator.Algorithm;
using UniDortmund.FaProSS17P3G1.MapGenerator.Model;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Pipeline.Terrain
{
    public class MainBlockOnlyTerrainGenerator : ITerrainGenerator
    {
        public TerrainGeneratorSettings Settings { set { } }

        public INoiseGenerator NoiseGenerator { get; set; }

        public static MainBlockOnlyTerrainGenerator Create(TerrainGeneratorSettings _) => new MainBlockOnlyTerrainGenerator();

        public void ApplyTo(WorldMap targetMap, int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
