using System;
using System.Collections.Generic;
using System.Text;
using NotEnoughTime.Utils;
using UniDortmund.FaProSS17P3G1.MapGenerator.Algorithm;
using UniDortmund.FaProSS17P3G1.MapGenerator.Model;
using static UniDortmund.FaProSS17P3G1.MapGenerator.Constants;
using static UniDortmund.FaProSS17P3G1.MapGenerator.Pipeline.GeneratorUtils;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Pipeline.Density
{
    public class FlatWorldGenerator : IDensityGenerator
    {
        private readonly int mWorldCeil;
        private readonly int mWorldFloor;

        public FlatWorldGenerator(DensityGeneratorSettings settings)
        {
            if (settings.SpecificSettingsCase != DensityGeneratorSettings.SpecificSettingsOneofCase.FlatWorldGenerator)
            {
                throw new ArgumentException("not the correct settings type");
            }
            mWorldFloor = settings.FlatWorldGenerator.FloorLevel;
            mWorldCeil = settings.FlatWorldGenerator.CeilLevel;
        }

        public INoiseGenerator NoiseGenerator { set { } }

        public static FlatWorldGenerator Create(DensityGeneratorSettings settings)
            => new FlatWorldGenerator(settings);

        public void ApplyTo(WorldMap targetMap, int x, int y)
        {
            var col = targetMap[x, y];
            if (Math.Abs(mWorldCeil - mWorldFloor) < ChunkDimension)
            {
                var chunk = col.Chunk(MapZToChunk(mWorldFloor));
                FillChunk(chunk, MapZToRelative(mWorldFloor), MapZToRelative(mWorldCeil + 1));
            }
            else
            {
                var chunk = col.Chunk(MapZToChunk(mWorldFloor));
                FillChunk(chunk, MapZToRelative(mWorldFloor), ChunkDimension);

                for (var i = mWorldFloor + ChunkDimension;
                    i <= mWorldCeil - ChunkDimension;
                    i += ChunkDimension)
                {
                    chunk = col.Chunk(MapZToChunk(i));
                    FillChunk(chunk, 0, ChunkDimension);
                }

                chunk = col.Chunk(MapZToChunk(mWorldCeil));
                FillChunk(chunk, 0, MapZToRelative(mWorldCeil + 1));
            }
        }

        private static void FillChunk(UnpackedChunk chunk, int min, int max)
        {
            for (var x = min; x < max; ++x)
            {
                for (var y = 0; y < ChunkDimension; ++y)
                {
                    for (var z = 0; z < ChunkDimension; ++z)
                    {
                        chunk[x, y, z] = ParticleType.PtDebug;
                    }
                }
            }
        }
    }
}
