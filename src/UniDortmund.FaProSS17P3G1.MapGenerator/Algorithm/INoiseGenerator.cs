namespace UniDortmund.FaProSS17P3G1.MapGenerator.Algorithm
{
    public interface INoiseGenerator
    {
        float this[float x, float y] { get; }
        float this[(float X, float Y) coords] { get; }
        float this[float x, float y, float z] { get; }
        float this[(float X, float Y, float Z) coords] { get; }

        ulong Seed { get; set; }
    }
}
