using System.Collections.Generic;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Algorithm
{
    public static class Helpers
    {
        public static IEnumerable<(int X, int Y)> SquareRange(int dimSize)
        {
            for (var i = 0; i < dimSize; ++i)
            {
                for (var j = 0; j < dimSize; ++j)
                {
                    yield return (j, i);
                }
            }
        }

        public static IEnumerable<(int X, int Y, int Z)> CubeRange(int dimSize)
        {
            for (var i = 0; i < dimSize; ++i)
            {
                for (var j = 0; j < dimSize; ++j)
                {
                    for (var k = 0; k < dimSize; ++k)
                    {
                        yield return (k, j, i);
                    }
                }
            }
        }
    }
}
