using UniDortmund.FaProSS17P3G1.MapGenerator.Algorithm;
using UniDortmund.FaProSS17P3G1.MapGenerator.Model;
using UniDortmund.FaProSS17P3G1.MapGenerator.Utils;
using UniDortmund.FaProSS17P3G1.MapGenerator.Pipeline;
using Xunit;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Tests
{
    public class Gen
    {
        private const string TestLevelPath = "./tmp/first";

        [Fact]
        public void First()
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
                    Seed = 634164638573246658L,
                    Type = DensityGeneratorSettings.Types.DensityGeneratorType.HeightNoise,
                },
                TerrainGenerator = new TerrainGeneratorSettings
                {
                    Type = TerrainGeneratorSettings.Types.TerrainGeneratorType.Simple
                }
            });

            var generator = new ComposedGenerator(map);
            generator.Generate(0, 0, 11, 11);
            map.Save();
            map.PrintBiomeMap();
        }

        [Fact]
        public void Second()
        {
            var map = new WorldMap(TestLevelPath, new WorldInfo
            {
                BiomeGenerator = new BiomeGeneratorSettings
                {
                    Seed = 6346436385753246658L,
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
            generator.Generate(0, 0, 11, 11);
            map.Save();
            map.PrintBiomeMap();
        }

        [Fact]
        public void Third()
        {
            var map = new WorldMap(TestLevelPath, new WorldInfo
            {
                BiomeGenerator = new BiomeGeneratorSettings
                {
                    Seed = 2346436365753246628L,
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
            map.WorldInfo.DetailsGenerators.Add(new DetailsGeneratorSettings
            {
                Seed = 5341645753946658L,
                Type = DetailsGeneratorSettings.Types.DetailsGeneratorType.Basic
            });

            var generator = new ComposedGenerator(map);
            generator.Generate(0, 0, 11, 11);
            map.Save();
            map.PrintBiomeMap();
        }

        [Fact]
        public void Fourth()
        {
            var map = new WorldMap(TestLevelPath, new WorldInfo
            {
                BiomeGenerator = new BiomeGeneratorSettings
                {
                    Seed = 2346465365753246628L,
                    Type = BiomeGeneratorSettings.Types.BiomeGeneratorType.TwoStage
                },
                DensityGenerator = new DensityGeneratorSettings
                {
                    Seed = 634164628573246658L,
                    Type = DensityGeneratorSettings.Types.DensityGeneratorType.HeightNoise,
                },
                TerrainGenerator = new TerrainGeneratorSettings
                {
                    Type = TerrainGeneratorSettings.Types.TerrainGeneratorType.Simple
                }
            });
            map.WorldInfo.DetailsGenerators.Add(new DetailsGeneratorSettings
            {
                Seed = 5341645759666658L,
                Type = DetailsGeneratorSettings.Types.DetailsGeneratorType.Basic
            });

            var generator = new ComposedGenerator(map);
            generator.Generate(0, 0, 11, 11);
            map.Save();
            map.PrintBiomeMap();
        }

        [Fact]
        public void Fifth()
        {
            var map = new WorldMap(TestLevelPath, new WorldInfo
            {
                BiomeGenerator = new BiomeGeneratorSettings
                {
                    Seed = 2349436365753246628L,
                    Type = BiomeGeneratorSettings.Types.BiomeGeneratorType.TwoStage
                },
                DensityGenerator = new DensityGeneratorSettings
                {
                    Seed = 634164628633246658L,
                    Type = DensityGeneratorSettings.Types.DensityGeneratorType.HeightNoise,
                },
                TerrainGenerator = new TerrainGeneratorSettings
                {
                    Type = TerrainGeneratorSettings.Types.TerrainGeneratorType.Simple
                }
            });
            map.WorldInfo.DetailsGenerators.Add(new DetailsGeneratorSettings
            {
                Seed = 5341612864666658L,
                Type = DetailsGeneratorSettings.Types.DetailsGeneratorType.Basic
            });

            var generator = new ComposedGenerator(map);
            generator.Generate(0, 0, 11, 11);
            map.Save();
            map.PrintBiomeMap();
        }
    }
}
