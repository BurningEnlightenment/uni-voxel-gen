using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Google.Protobuf;
using static UniDortmund.FaProSS17P3G1.MapGenerator.Constants;
using static UniDortmund.FaProSS17P3G1.MapGenerator.LibExtensions;


namespace UniDortmund.FaProSS17P3G1.MapGenerator.Model
{
    public class UnpackedChunk
    {
        public readonly IArray3D<ParticleType> Data
            = new FastArray3D<ParticleType>(ChunkDimension);

        public UnpackedChunk()
        {
<<<<<<< Updated upstream
        }
        public UnpackedChunk(WorldBlock block)
        {
            var ctr = 0;
            foreach (var particleField in block.ParticleFields)
            {
                for (var bound = ctr + particleField.NumberOfParticles; ctr < bound; ++ctr)
                {
                    Data[ctr] = particleField.Type;
                }
            }
        }
        public UnpackedChunk(ParticleType initial)
        {
            Data.Fill(() => initial);
        }

        public WorldBlock Pack()
        {
            var packed = new WorldBlock();

            var count = 0u;
            var lastParticle = Data[0];
            foreach (var particle in Data.Select(data => data.Value))
            {
                if (particle != lastParticle)
                {
                    packed.ParticleFields.Add(new ParticleField
                    {
                        Type = lastParticle,
                        NumberOfParticles = count,
                    });
                    count = 0;
                    lastParticle = particle;
                }
                ++count;
            }
            if (count > 0)
            {
                packed.ParticleFields.Add(new ParticleField
                {
                    Type = lastParticle,
                    NumberOfParticles = count,
                });
            }

            return packed;
=======
            get => mparticles[x, y, z];
            set => mparticles[x, y, z] = value;
>>>>>>> Stashed changes
        }
    }

    public class UnpackedColumn
    {
        private readonly List<UnpackedChunk> mChunks;

        public UnpackedColumn()
        {
<<<<<<< Updated upstream
            mChunks = new List<UnpackedChunk>(8);
        }

        public UnpackedColumn(WorldColumn data)
        {
            mChunks = new List<UnpackedChunk>(data.Blocks.Count);
            mChunks.AddRange(data.Blocks.Select(block =>
            {
                var blockData = block.ParticleFields;
                if (blockData.Count == 0
                    || blockData.Count == 1 && blockData[0].Type == ParticleType.PtAir)
                {
                    return null;
                }
                return new UnpackedChunk(block);
            }));
=======
            get => mbiomeTypes[x, y];
            set => mbiomeTypes[x, y] = value;
>>>>>>> Stashed changes
        }

        public IArray2D<BiomeType> BiomeMap { get; }
            = new FastArray2D<BiomeType>(ChunkDimension);

        public int FloorChunkIdx => CeilChunkIdx - mChunks.Count + 1;
        public int CeilChunkIdx => (mChunks.Count - 1) / 2;

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

        public UnpackedChunk ChunkByZ(int z) => Chunk(MapZToChunkNum(z));

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
            for (var i = FloorChunkIdx; i <= CeilChunkIdx; ++i)
            {
                var chunk = mChunks[ZigZagEnc(i)];
                if (chunk != null)
                {
                    yield return (i, chunk);
                }
            }
        }

        public WorldColumn Pack() => new WorldColumn
        {
            Blocks = {mChunks.Select(block => block?.Pack() ?? GeneratorUtils.AirBlock )}
        };

        public ParticleType this[int x, int y, int z]
        {
            get
            {
                var chunkID = GetChunkFromHeight(z);
                var localZ = ConvertGlobalToLocalHeight(z);
                return mChunks[chunkID][x, y, localZ];
            }
            set
            {
                var chunkID = GetChunkFromHeight(z);
                var localZ = ConvertGlobalToLocalHeight(z);
                mChunks[chunkID][x, y, localZ] = value;

            }
        }

        private int ConvertGlobalToLocalHeight(int globalHeight)
        {
            return mod(globalHeight, Constants.chunkHeight);
        }

        private int GetChunkFromHeight(int globalHeight)
        {
            int localHeight;
            if (globalHeight < 0)
            {
                localHeight = (1 - (globalHeight / (Constants.chunkHeight / 2)));
            }
            else
            {
                localHeight = (globalHeight / (Constants.chunkHeight / 2));
            }
            return localHeight;
        }
        private int mod(int x, int m)
        {
            return (x % m + m) % m;
        }

        public static int MapZToChunkNum(int z) => z / 16;

        public static int MapZToRelative(int z)
            => GeneratorUtils.Mod(z, ChunkDimension);
    }
}
