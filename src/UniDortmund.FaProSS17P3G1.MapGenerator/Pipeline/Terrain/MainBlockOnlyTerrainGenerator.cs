using NotEnoughTime.Utils;
using UniDortmund.FaProSS17P3G1.MapGenerator.Algorithm;
using UniDortmund.FaProSS17P3G1.MapGenerator.Model;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Pipeline.Terrain
{
    public class MainBlockOnlyTerrainGenerator : ITerrainGenerator, IDeepClonable<MainBlockOnlyTerrainGenerator>
    {
        public TerrainGeneratorSettings Settings { set { } }

        public INoiseGenerator NoiseGenerator { get; set; }

        public static MainBlockOnlyTerrainGenerator Create() => new MainBlockOnlyTerrainGenerator();

        public MainBlockOnlyTerrainGenerator Clone()
        {
            return new MainBlockOnlyTerrainGenerator();
        }
        ITerrainGenerator IDeepClonable<ITerrainGenerator>.Clone() => Clone();
    }
}
