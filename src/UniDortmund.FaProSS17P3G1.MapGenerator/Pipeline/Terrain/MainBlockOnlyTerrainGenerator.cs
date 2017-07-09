using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using NotEnoughTime.Utils;
using UniDortmund.FaProSS17P3G1.MapGenerator.Algorithm;
using UniDortmund.FaProSS17P3G1.MapGenerator.Model;
using static UniDortmund.FaProSS17P3G1.MapGenerator.Constants;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Pipeline.Terrain
{
    public class MainBlockOnlyTerrainGenerator : ITerrainGenerator
    {
        private static readonly ImmutableDictionary<BiomeType, ParticleType> BiomeParticleTypes
            = ImmutableDictionary<BiomeType, ParticleType>.Empty
                .Add(BiomeType.BioDebug, ParticleType.PtDebug)
                .Add(BiomeType.BioGrassland, ParticleType.PtDirt);

        public INoiseGenerator NoiseGenerator { get; set; }

        public static MainBlockOnlyTerrainGenerator Create(TerrainGeneratorSettings _) => new MainBlockOnlyTerrainGenerator();

        public void ApplyTo(WorldMap targetMap, int x, int y)
        {
            var col = targetMap[x, y];
            var particleAssignments = col.BiomeMap
                .MapValues(biome => BiomeParticleTypes[biome]);

            foreach (var chunkData in col.UsedChunks().Select(pair => pair.Chunk.Data))
            {
                chunkData.Mutate((cx, cy, cz, old)
                    => old == ParticleType.PtDebug ? particleAssignments[cx, cy] : old);
            }
        }
    }
}
