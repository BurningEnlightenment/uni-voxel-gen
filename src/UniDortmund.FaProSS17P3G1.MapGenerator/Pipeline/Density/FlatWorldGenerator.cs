using System;
using System.Collections.Generic;
using System.Text;
using NotEnoughTime.Utils;
using UniDortmund.FaProSS17P3G1.MapGenerator.Algorithm;
using UniDortmund.FaProSS17P3G1.MapGenerator.Model;
using static UniDortmund.FaProSS17P3G1.MapGenerator.Constants;
using static UniDortmund.FaProSS17P3G1.MapGenerator.Model.UnpackedColumn;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Pipeline.Density
{
    public class FlatWorldGenerator : IDensityGenerator
    {
        private readonly int mWorldCeil;
        private readonly int mWorldCeilChunk;
        private readonly int mWorldCeilRel;
        private readonly int mWorldFloor;
        private readonly int mWorldFloorChunk;
        private readonly int mWorldFloorRel;

        public FlatWorldGenerator(DensityGeneratorSettings settings)
        {
            if (settings.SpecificSettingsCase != DensityGeneratorSettings.SpecificSettingsOneofCase.FlatWorldGenerator)
            {
                throw new ArgumentException("not the correct settings type");
            }
            mWorldFloor = settings.FlatWorldGenerator.FloorLevel;
            mWorldFloorChunk = MapZToChunkNum(mWorldFloor);
            mWorldFloorRel = MapZToRelative(mWorldFloor);
            mWorldCeil = settings.FlatWorldGenerator.CeilLevel;
            mWorldCeilChunk = MapZToChunkNum(mWorldCeil);
            mWorldCeilRel = MapZToRelative(mWorldCeil);
        }

        public INoiseGenerator NoiseGenerator { set { } }

        public static FlatWorldGenerator Create(DensityGeneratorSettings settings)
            => new FlatWorldGenerator(settings);

        public void ApplyTo(WorldMap targetMap, int x, int y)
        {
            var col = targetMap[x, y];

            // touch all chunks in between
            foreach (var _ in col.ChunkRange(mWorldFloorChunk, mWorldCeilChunk))
            {
            }

            var chunk = col.Chunk(mWorldCeilChunk);
            FillChunkWithAir(chunk, mWorldCeilRel + 1, ChunkDimension - 1);

            chunk = col.Chunk(mWorldFloorChunk);
            FillChunkWithAir(chunk, 0, mWorldFloorRel);

        }

        private static void FillChunkWithAir(UnpackedChunk chunk, int min, int max)
        {
            for (var z = min; z <= max; ++z)
            {
                for (var y = 0; y < ChunkDimension; ++y)
                {
                    for (var x = 0; x < ChunkDimension; ++x)
                    {
                        chunk.Data[x, y, z] = ParticleType.PtAir;
                    }
                }
            }
        }
    }
}
