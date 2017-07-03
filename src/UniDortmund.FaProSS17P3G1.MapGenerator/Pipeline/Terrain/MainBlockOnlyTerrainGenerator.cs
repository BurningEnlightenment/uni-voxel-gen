using NotEnoughTime.Utils;
using UniDortmund.FaProSS17P3G1.MapGenerator.Algorithm;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Pipeline.Terrain
{
    public class MainBlockOnlyTerrainGenerator : ITerrainGenerator, IDeepClonable<MainBlockOnlyTerrainGenerator>
    {
        public INoiseGenerator NoiseGenerator { get; set; }

        public static MainBlockOnlyTerrainGenerator Create() => new MainBlockOnlyTerrainGenerator();

        public MainBlockOnlyTerrainGenerator Clone()
        {
            return new MainBlockOnlyTerrainGenerator();
        }
        ITerrainGenerator IDeepClonable<ITerrainGenerator>.Clone() => Clone();
    }
}
