using UniDortmund.FaProSS17P3G1.MapGenerator.Algorithm;
using UniDortmund.FaProSS17P3G1.MapGenerator.Model;
using UniDortmund.FaProSS17P3G1.MapGenerator.Utils;
using UniDortmund.FaProSS17P3G1.MapGenerator.Pipeline;
using Xunit;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Tests
{
    public class Gen
    {
        private const string TestLevelPath = "./tmp/";

        [Fact]
        public void First()
        {
            var map = new WorldMap(TestLevelPath + "first", new WorldInfo
            {
                BiomeGenerator = new BiomeGeneratorSettings
                {
                    Seed = 6341646385753246658L,
                    Type = BiomeGeneratorSettings.Types.BiomeGeneratorType.TwoStage
                },
                DensityGenerator = new DensityGeneratorSettings
                {
                    Seed = 634164638573246658L,
                    Type = DensityGeneratorSettings.Types.DensityGeneratorType.HeightNoise,
                },
                TerrainGenerator = new TerrainGeneratorSettings
                {
                    Type = TerrainGeneratorSettings.Types.TerrainGeneratorType.Simple
                }
            });

            var generator = new ComposedGenerator(map);
            generator.Generate(-5, -5, 11, 11);
            map.Save();
            map.PrintBiomeMap();
        }
    }
}
