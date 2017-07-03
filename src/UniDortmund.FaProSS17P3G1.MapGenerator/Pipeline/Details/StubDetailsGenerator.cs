using NotEnoughTime.Utils;
using UniDortmund.FaProSS17P3G1.MapGenerator.Algorithm;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Pipeline.Details
{
    public class StubDetailsGenerator : IDetailsGenerator, IDeepClonable<StubDetailsGenerator>
    {
        public INoiseGenerator NoiseGenerator { get; set; }

        public static StubDetailsGenerator Create() => new StubDetailsGenerator();

        public StubDetailsGenerator Clone()
        {
            return new StubDetailsGenerator();
        }
        IDetailsGenerator IDeepClonable<IDetailsGenerator>.Clone() => Clone();
    }
}
