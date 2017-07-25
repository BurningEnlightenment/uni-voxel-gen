#define DEBUG
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using UniDortmund.FaProSS17P3G1.MapGenerator.Algorithm;
using UniDortmund.FaProSS17P3G1.MapGenerator.Model;
using static UniDortmund.FaProSS17P3G1.MapGenerator.Constants;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Pipeline.Density
{
    public class HeightNoiseGenerator : IDensityGenerator
    {
        private readonly Dictionary<BiomeType, BiomeHeightSettings> BiomeSettings
            = new Dictionary<BiomeType, BiomeHeightSettings>
            {
                { BiomeType.BioDebug, new BiomeHeightSettings(0, 0) },
                { BiomeType.BioOcean, new BiomeHeightSettings(-12, -8) },
                { BiomeType.BioGrassland, new BiomeHeightSettings(2, 12) },
                { BiomeType.BioForest, new BiomeHeightSettings(2, 12) },
                { BiomeType.BioHighlands, new BiomeHeightSettings(16, 31) },
                { BiomeType.BioTundra, new BiomeHeightSettings(0, 4) },
                { BiomeType.BioDesert, new BiomeHeightSettings(2, 8) }
            };

        private readonly ulong Seed;
        private readonly LazyMap<ColumnHeightMap> RawHeightMap = new LazyMap<ColumnHeightMap>();
        private INoiseGenerator HeightNoise;

        public HeightNoiseGenerator(DensityGeneratorSettings settings)
        {
            Seed = settings.Seed;
        }

        public Func<ulong, INoiseGenerator> NoiseGeneratorFactory { private get; set; }

        public static HeightNoiseGenerator Create(DensityGeneratorSettings settings)
            => new HeightNoiseGenerator(settings);

        public void ApplyTo(WorldMap targetMap, int x, int y)
        {
            Debug.WriteLine($"hng for {x} {y}");
            HeightNoise = NoiseGeneratorFactory(Seed);
            for (var i = x - 1; i <= x + 1; ++i)
            {
                for (var j = y - 1; j <= y + 1; ++j)
                {
                    var col = targetMap[i, j];
                    var localHeightMap = RawHeightMap[i, j];
                    if (localHeightMap[0, 0] != 0)
                    {
                        continue;
                    }
                    Debug.WriteLine($"hng raw for ({i} {j})");
                    var cx = i;
                    var cy = j;
                    localHeightMap.Fill((lx, ly) =>
                    {
                        var bio = col.BiomeMap[lx, ly];
                        var set = BiomeSettings[bio];
                        
                        return set.Distribute(HeightNoise[GeneratorUtils.ToGlobalF(cx, lx, cy, ly)]);
                    });
                }
            }

            Debug.WriteLine($"raw hm passed");
            var column = targetMap[x, y];
            column.HeightMap.Fill((lx, ly) =>
            {
                var sum = 0;
                var global = GeneratorUtils.ToGlobal(x, lx, y, ly);
                foreach (var coords in Helpers.SquareRange(9))
                {
                    var tx = global.X + coords.X - 4;
                    var tcx = UnpackedColumn.MapZToChunkNum(tx);
                    var tlx = UnpackedColumn.MapZToRelative(tx);
                    var ty = global.Y + coords.Y - 4;
                    var tcy = UnpackedColumn.MapZToChunkNum(ty);
                    var tly = UnpackedColumn.MapZToRelative(ty);
                    sum += RawHeightMap[tcx, tcy][tlx, tly];
                }
                return sum / 81;
            });

            Debug.WriteLine($"eq hm passed");
            // touch bottom
            for (var cz = -1; cz < 2; ++cz)
            {
                var chunk = column.Chunk(cz);
                var cgz = cz * ChunkDimension;

                Debug.WriteLine($"doing chunk {cz}");
                chunk.Data.Fill((lx, ly, lz) =>
                {
                    var bound = column.HeightMap[lx, ly];
                    return cgz + lz > bound ? ParticleType.PtAir : ParticleType.PtDebug;
                });
            }
        }

        private class BiomeHeightSettings
        {
            public readonly int MinCeilLevel;
            public readonly int MaxCeilLevel;

            public BiomeHeightSettings(int minCeilLevel, int maxCeilLevel)
            {
                MinCeilLevel = minCeilLevel;
                MaxCeilLevel = maxCeilLevel;
            }

            public int Distribute(float val)
            {
                var normedVal = val / 2.0 + 0.5;
                return MinCeilLevel + (int)Math.Round((MaxCeilLevel - MinCeilLevel) * normedVal);
            }
        }

        private class ColumnHeightMap : FastArray2D<int>
        {
            public ColumnHeightMap()
                : base(ChunkDimension)
            {
            }
        }
    }
}
