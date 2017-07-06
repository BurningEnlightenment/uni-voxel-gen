using System;
using System.Collections.Generic;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Model
{
    public class UnpackedChunk
    {
        private ParticleType[,,] mparticles;

        public ParticleType this[int x, int y, int z]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
    }

    public class UnpackedColumn
    {
        private BiomeType[,] mbiomeTypes;
        private List<UnpackedChunk> mChunks;

        public UnpackedColumn()
        {
        }

        public UnpackedColumn(WorldColumn data)
        {
        }

        public BiomeType this[int x, int y]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public ParticleType this[int x, int y, int z]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
    }
}
