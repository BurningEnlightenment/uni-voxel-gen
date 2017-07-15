using System;
using System.Collections.Generic;
using System.Text;
using UniDortmund.FaProSS17P3G1.MapGenerator.Model;
using Xunit;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Tests
{
    public class LazyMapTests
    {
        [Fact]
        public void DimensionInitializationZero()
        {
            var map = new LazyMap<object>();
            Assert.Equal(0, map.OriginX);
            Assert.Equal(0, map.OriginY);
            Assert.Equal(0, map.SizeX);
            Assert.Equal(0, map.SizeY);
            Assert.Equal(-1, map.MaxX);
            Assert.Equal(-1, map.MaxY);
        }

        [Fact]
        public void DimensionXExpansion()
        {
            var map = new LazyMap<object>();

            map[0, 0] = null;
            Assert.Equal(0, map.OriginX);
            Assert.Equal(0, map.OriginY);
            Assert.Equal(1, map.SizeX);
            Assert.Equal(1, map.SizeY);
            Assert.Equal(0, map.MaxX);
            Assert.Equal(0, map.MaxY);

            map[3, 0] = null;
            Assert.Equal(0, map.OriginX);
            Assert.Equal(0, map.OriginY);
            Assert.Equal(4, map.SizeX);
            Assert.Equal(1, map.SizeY);
            Assert.Equal(3, map.MaxX);
            Assert.Equal(0, map.MaxY);

            map[2, 0] = null;
            Assert.Equal(0, map.OriginX);
            Assert.Equal(0, map.OriginY);
            Assert.Equal(4, map.SizeX);
            Assert.Equal(1, map.SizeY);
            Assert.Equal(3, map.MaxX);
            Assert.Equal(0, map.MaxY);

            map[6, 0] = null;
            Assert.Equal(0, map.OriginX);
            Assert.Equal(0, map.OriginY);
            Assert.Equal(7, map.SizeX);
            Assert.Equal(1, map.SizeY);
            Assert.Equal(6, map.MaxX);
            Assert.Equal(0, map.MaxY);

            map[-4, 0] = null;
            Assert.Equal(-4, map.OriginX);
            Assert.Equal(0, map.OriginY);
            Assert.Equal(11, map.SizeX);
            Assert.Equal(1, map.SizeY);
            Assert.Equal(6, map.MaxX);
            Assert.Equal(0, map.MaxY);
        }
    }
}
