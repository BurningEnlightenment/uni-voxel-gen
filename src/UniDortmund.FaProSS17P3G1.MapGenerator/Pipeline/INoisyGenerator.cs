using UniDortmund.FaProSS17P3G1.MapGenerator.Algorithm;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Pipeline
{
    public interface INoisyGenerator
    {
        INoiseGenerator NoiseGenerator { set; }
    }
}