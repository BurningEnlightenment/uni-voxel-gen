using UniDortmund.FaProSS17P3G1.MapGenerator.Model;
using UniDortmund.FaProSS17P3G1.MapGenerator.Pipeline;
using UniDortmund.FaProSS17P3G1.MapGenerator.Utils;
using Xunit;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Tests
{
    public class SimpleGenTest
    {
        private const string TestLevelPath = @"./tmp/test-level";

        [Fact]
        public void Exec()
        {
            var map = new WorldMap(TestLevelPath, new WorldInfo
            {
                BiomeGenerator = new BiomeGeneratorSettings
                {
                    Type = BiomeGeneratorSettings.Types.BiomeGeneratorType.Uniform,
                    UniformGenerator = new BiomeGeneratorSettings.Types.UniformGeneratorSettings
                    {
                        TargetBiomeType = BiomeType.BioGrassland
                    }
                },
                DensityGenerator = new DensityGeneratorSettings
                {
                    Type = DensityGeneratorSettings.Types.DensityGeneratorType.FlatWorld,
                    FlatWorldGenerator = new DensityGeneratorSettings.Types.FlatWorldGeneratorSettings
                    {
                        CeilLevel = 31,
                        FloorLevel = -35
                    }
                },
                TerrainGenerator = new TerrainGeneratorSettings
                {
                    Type = TerrainGeneratorSettings.Types.TerrainGeneratorType.MainBlockOnly
                }
            });

            var generator = new ComposedGenerator(map);
            generator.Generate(-2, -2, 5, 5);
            map.Save();
            map.SaveDebug();
            map.PrintBiomeMap();
        }
    }
}
