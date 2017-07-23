using UniDortmund.FaProSS17P3G1.MapGenerator.Model;
using UniDortmund.FaProSS17P3G1.MapGenerator.Pipeline;
using UniDortmund.FaProSS17P3G1.MapGenerator.Utils;
using Xunit;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Tests
{
    public class TwoStageBiomeGenTest
    {
        private const string TestLevelPath = @"./tmp/test-biome";


        [Fact]
        public void Exec()
        {
            var map = new WorldMap(TestLevelPath, new WorldInfo
            {
                BiomeGenerator = new BiomeGeneratorSettings
                {
                    Seed = 6341646385753246658L,
                    Type = BiomeGeneratorSettings.Types.BiomeGeneratorType.TwoStage
                },
                DensityGenerator = new DensityGeneratorSettings
                {
                    Type = DensityGeneratorSettings.Types.DensityGeneratorType.FlatWorld,
                    FlatWorldGenerator = new DensityGeneratorSettings.Types.FlatWorldGeneratorSettings
                    {
                        CeilLevel = 16,
                        FloorLevel = 0
                    }
                },
                TerrainGenerator = new TerrainGeneratorSettings
                {
                    Type = TerrainGeneratorSettings.Types.TerrainGeneratorType.MainBlockOnly
                }
            });

            var generator = new ComposedGenerator(map);
            generator.Generate(-50, -50, 101, 101);
            map.PrintBiomeMap();
        }

        [Fact]
        public void Small()
        {
            var map = new WorldMap(TestLevelPath, new WorldInfo
            {
                BiomeGenerator = new BiomeGeneratorSettings
                {
                    Seed = 6341645753946658L,
                    Type = BiomeGeneratorSettings.Types.BiomeGeneratorType.TwoStage
                },
                DensityGenerator = new DensityGeneratorSettings
                {
                    Type = DensityGeneratorSettings.Types.DensityGeneratorType.FlatWorld,
                    FlatWorldGenerator = new DensityGeneratorSettings.Types.FlatWorldGeneratorSettings
                    {
                        CeilLevel = 16,
                        FloorLevel = 0
                    }
                },
                TerrainGenerator = new TerrainGeneratorSettings
                {
                    Type = TerrainGeneratorSettings.Types.TerrainGeneratorType.MainBlockOnly
                }
            });

            var generator = new ComposedGenerator(map);
            generator.Generate(-10, 14, 4, 5);
            map.PrintBiomeMap();
        }
    }
}
