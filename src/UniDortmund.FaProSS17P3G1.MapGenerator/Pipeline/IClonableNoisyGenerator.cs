using NotEnoughTime.Utils;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Pipeline
{
    public interface IClonableNoisyGenerator<out T> : INoisyGenerator, IDeepClonable<T>
    {
    }
}
