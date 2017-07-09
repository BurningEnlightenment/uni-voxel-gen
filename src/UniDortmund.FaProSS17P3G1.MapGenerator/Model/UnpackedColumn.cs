using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using static UniDortmund.FaProSS17P3G1.MapGenerator.Constants;
using static UniDortmund.FaProSS17P3G1.MapGenerator.LibExtensions;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Model
{
    public class UnpackedChunk
    {
        public readonly IArray3D<ParticleType> Data
            = new FastArray3D<ParticleType>(ChunkDimension);
    }

    public class UnpackedColumn
    {
        private List<UnpackedChunk> mChunks;

        public UnpackedColumn()
        {
        }

        public UnpackedColumn(WorldColumn data)
        {
        }

        public IArray2D<BiomeType> BiomeMap { get; }
            = new FastArray2D<BiomeType>(ChunkDimension);

        public UnpackedChunk Chunk(int coord)
        {
            var enc = ZigZagEnc(coord);
            if (enc >= mChunks.Count)
            {
                mChunks.Resize(enc + 1);
            }
            var chunk = mChunks[enc];
            if (chunk == null)
            {
                mChunks[enc] = chunk = new UnpackedChunk();
            }
            return chunk;
        }

        /// <summary>
        /// Enumerates the chunks from the [begin, end] interval
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public IEnumerable<UnpackedChunk> ChunkRange(int begin, int end)
        {
            var realBegin = Math.Min(begin, end);
            var realEnd = Math.Max(begin, end);

            var neededSize = Math.Max(ZigZagEnc(begin), ZigZagEnc(end));
            if (mChunks.Capacity < neededSize)
            {
                mChunks.Capacity = neededSize;
            }

            return Enumerable.Range(realBegin, realEnd - realBegin)
                .Select(Chunk);
        }
        
        public IEnumerable<(int Coord, UnpackedChunk Chunk)> UsedChunks()
        {
            var count = mChunks.Count;
            var halfCount = (count-1) / 2;
            for (var i = halfCount - count + 1; i <= halfCount; ++i)
            {
                var chunk = mChunks[ZigZagEnc(i)];
                if (chunk != null)
                {
                    yield return (i, chunk);
                }
            }
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
