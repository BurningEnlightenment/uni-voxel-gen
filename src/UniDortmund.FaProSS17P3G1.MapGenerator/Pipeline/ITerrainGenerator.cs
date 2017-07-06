using UniDortmund.FaProSS17P3G1.MapGenerator.Model;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Pipeline
{
    public interface ITerrainGenerator : IClonableNoisyGenerator<ITerrainGenerator>
    {
        TerrainGeneratorSettings Settings { set; }
    }
}
