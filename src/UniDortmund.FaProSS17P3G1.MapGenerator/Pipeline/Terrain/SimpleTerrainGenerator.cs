using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using NotEnoughTime.Utils;
using UniDortmund.FaProSS17P3G1.MapGenerator.Algorithm;
using UniDortmund.FaProSS17P3G1.MapGenerator.Model;
using static UniDortmund.FaProSS17P3G1.MapGenerator.LibExtensions;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Pipeline.Terrain
{
    public class SimpleTerrainGenerator : ITerrainGenerator
    {
        private static readonly ImmutableDictionary<BiomeType, ParticleType> BiomeParticleTypes
            = ImmutableDictionary<BiomeType, ParticleType>.Empty
                .Add(BiomeType.BioDebug, ParticleType.PtDebug)
                .Add(BiomeType.BioGrassland, ParticleType.PtDirt);

        public Func<ulong, INoiseGenerator> NoiseGeneratorFactory
        {
            set { }
        }

        public static SimpleTerrainGenerator Create(TerrainGeneratorSettings _) => new SimpleTerrainGenerator();

        public void ApplyTo(WorldMap targetMap, int x, int y)
        {
            var col = targetMap[x, y];

            for (int i = 0; i <= col.CeilChunkIdx; i++)
            {
                var chunk = col.Chunk(i);
                chunk.Data.Mutate((cx, cy, cz, old) => BlockReplacementStrategy(cx, cy, cz, old, col, i));
            }
        }


        private static ParticleType BlockReplacementStrategy(int x, int y, int cz, ParticleType oldParticle, UnpackedColumn col, int i)
        {
            BiomeType Biome = col.BiomeMap[x, y];
            int snowlevel = 20;
            int waterlevel = 0;
            int heightFirstAirBlock = col.HeightMap[x, y];
            int iz = ZigZagDec(i) * 16;
            int z = iz + cz;
            if (oldParticle != ParticleType.PtAir)
            {
                if (Biome == BiomeType.BioDesert)
                {
                    if (z <= heightFirstAirBlock - 10)
                    {
                        return ParticleType.PtStone;
                    }
                    else
                    return ParticleType.PtSand;
                    
                }
                if (Biome == BiomeType.BioGrassland || Biome == BiomeType.BioForest)
                {
                    if (z <= heightFirstAirBlock - 10)
                    {
                        return ParticleType.PtStone;
                    }
                    else if (z <= heightFirstAirBlock - 1)
                    {
                        return ParticleType.PtDirt;
                    }
                    return ParticleType.PtGrass;
                    
                }
                if (Biome == BiomeType.BioOcean)
                {
                    if (z > waterlevel - 7 && z>= heightFirstAirBlock - 6)
                    {
                        return ParticleType.PtSand;
                    }
                    return ParticleType.PtStone;
                }
                if (Biome == BiomeType.BioHighlands)
                {
                    if (z >= snowlevel && z >= heightFirstAirBlock - 6)
                    {
                        return ParticleType.PtSnow;
                    }
                    if (z >= snowlevel+5 && z < heightFirstAirBlock - 6)
                    {
                        return ParticleType.PtStone;
                    }
                    if (z == heightFirstAirBlock - 1)
                    {
                        return ParticleType.PtGrass;
                    }
                    if (z >= heightFirstAirBlock - 4)
                    {
                        return ParticleType.PtDirt;
                    }
                    return ParticleType.PtStone;
                }
                if (Biome == BiomeType.BioTundra);
                {
                    if (z <= heightFirstAirBlock - 15)
                    {
                        return ParticleType.PtStone;
                    }
                    return ParticleType.PtSnow;
                }
            }
            if (Biome == BiomeType.BioOcean && oldParticle == ParticleType.PtAir)
            {
                if (z < waterlevel)
                {
                    return ParticleType.PtWater;
                }
            }
            return oldParticle;
        }
    }

}