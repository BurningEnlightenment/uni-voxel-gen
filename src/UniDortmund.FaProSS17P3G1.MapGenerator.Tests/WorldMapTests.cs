using UniDortmund.FaProSS17P3G1.MapGenerator.Algorithm;
using UniDortmund.FaProSS17P3G1.MapGenerator.Model;
using UniDortmund.FaProSS17P3G1.MapGenerator.Utils;
using Xunit;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Tests
{
    public class WorldMapTests
    {
        private const string TestLevelPath = @"./tmp/test-map";

        [Fact]
        public void WorldMapPrintBiomeTest()
        {
            var map = new WorldMap(TestLevelPath, new WorldInfo());

            var biomeMap = map[0, 0].BiomeMap;
            biomeMap[0, 0] = BiomeType.BioGrassland;
            biomeMap[0, 1] = BiomeType.BioOcean;
            biomeMap[15, 15] = BiomeType.BioForest;
            biomeMap[14, 15] = BiomeType.BioDesert;

            biomeMap = map[1, 0].BiomeMap;
            biomeMap[0, 0] = BiomeType.BioDesert;

            biomeMap = map[0, 1].BiomeMap;
            biomeMap[0, 0] = BiomeType.BioOcean;

            biomeMap = map[1, 1].BiomeMap;
            biomeMap[0, 0] = BiomeType.BioTundra;

            map.CreateDirectory();
            map.PrintBiomeMap();
        }

        [Fact]
        public void TestTest()
        {
            var array = new FastArray2D<int>(5);
            
            for (var i = 0; i < array.TotalSize; ++i)
            {
                array[i] = i;
            }
            var j = 0;
            foreach (var point in Helpers.SquareRange(5))
            {
                Assert.Equal(j++, array.MapCoordinateToIndex(point.X, point.Y));
            }
        }
    }
}
