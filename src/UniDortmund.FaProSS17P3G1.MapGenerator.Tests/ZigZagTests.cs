using System;
using Xunit;
using static UniDortmund.FaProSS17P3G1.MapGenerator.LibExtensions;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Tests
{
    public class ZigZagTests
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, -1)]
        [InlineData(2, 1)]
        [InlineData(3, -2)]
        [InlineData(4, 2)]
        [InlineData(5, -3)]
        [InlineData(6, 3)]
        public void EncodingTest(int y, int x)
        {
            Assert.Equal(y, ZigZagEnc(x));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, -1)]
        [InlineData(2, 1)]
        [InlineData(3, -2)]
        [InlineData(4, 2)]
        [InlineData(5, -3)]
        [InlineData(6, 3)]
        public void DecodingTest(int x, int y)
        {
            Assert.Equal(y, ZigZagDec(x));
        }
    }
}
