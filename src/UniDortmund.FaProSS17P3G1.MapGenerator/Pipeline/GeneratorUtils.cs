namespace UniDortmund.FaProSS17P3G1.MapGenerator.Pipeline
{
    public static class GeneratorUtils
    {
        public static int Mod(int x, int m)
        {
            var r = x % m;
            return r < 0 ? r + m : r;
        }

        public static int MapZToChunk(int z)
            => z / 16;

        public static int MapZToRelative(int z)
            => Mod(z, Constants.ChunkDimension);
    }
}
