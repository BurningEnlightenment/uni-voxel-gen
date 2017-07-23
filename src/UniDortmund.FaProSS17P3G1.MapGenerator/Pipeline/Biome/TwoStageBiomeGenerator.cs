using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Google.Protobuf.WellKnownTypes;
using NotEnoughTime.Utils.Random;
using UniDortmund.FaProSS17P3G1.MapGenerator.Algorithm;
using UniDortmund.FaProSS17P3G1.MapGenerator.Model;
using UniDortmund.FaProSS17P3G1.MapGenerator.Utils;
using static UniDortmund.FaProSS17P3G1.MapGenerator.Constants;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Pipeline.Biome
{
    public class TwoStageBiomeGenerator : IBiomeGenerator
    {

        private readonly ulong mSeed;

        public TwoStageBiomeGenerator(BiomeGeneratorSettings settings)
        {
            if (settings.Type != BiomeGeneratorSettings.Types.BiomeGeneratorType.TwoStage
                || settings.SpecificSettingsCase != BiomeGeneratorSettings.SpecificSettingsOneofCase.None)
            {
                throw new ArgumentException("wrong settings object type", nameof(settings));
            }

            mSeed = settings.Seed;
        }

        public Func<ulong, INoiseGenerator> NoiseGeneratorFactory { private get; set; }


        public void ApplyTo(WorldMap targetMap, int x, int y)
        {

            var generator = CreateGenerator();
            foreach ((var colX, var colY) in Helpers.SquareRange(3)
                .Select(p => (p.X + x, p.Y + y)))
            {
                var col = targetMap[colX, colY];
                if (col.BiomeMap[0, 0] == BiomeType.BioDebug)
                {
                    col.BiomeMap.Fill(GeneratorForBlock(colX, colY));
                }
            }

            Func<int, int, BiomeType> GeneratorForBlock(int gx, int gy)
                => (lx, ly) => generator(MapCoord(gx, lx), MapCoord(gy, ly));

            const float coordScale = 1f * 3;
            const float localCoordScale = coordScale / ChunkDimension;

            float MapCoord(float g, float l)
                => g * coordScale + l * localCoordScale;
        }

        private Func<float, float, BiomeType> CreateGenerator()
        {
            var rng = XoroShiro128Plus.Create(mSeed);
            var global = CreateGlobalGenerator(rng);
            var local = CreateLocalGenerator(rng);
            return (x, y) => local(x, y, global(x, y));
        }

        private Func<float, float, BiomeType, BiomeType> CreateLocalGenerator(IUniformRandomBitGenerator seed)
        {
            var voronoi = CreateVoronoiNoise(seed, 0.5f);
            var coordjitter = new GridWithJitter(
                new ScaledNoiseGenerator(NoiseGeneratorFactory(seed.Next64Bits()), 21.331f),
                new ScaledNoiseGenerator(NoiseGeneratorFactory(seed.Next64Bits()), 21.331f),
                2.03f
            );
            return (x, y, globalBiome)
                => LocalBiomeMapping[globalBiome][voronoi(coordjitter[x, y])];
                //=> globalBiome;
        }

        private Func<float, float, BiomeType> CreateGlobalGenerator(IUniformRandomBitGenerator seed)
        {
            var voronoi = CreateVoronoiNoise(seed, 1.337f);
            var coordjitter = new GridWithJitter(
                new ScaledNoiseGenerator(NoiseGeneratorFactory(seed.Next64Bits()), 10.331f),
                new ScaledNoiseGenerator(NoiseGeneratorFactory(seed.Next64Bits()), 10.331f),
                6.03f
            );
            //return (x, y) => GlobalBiomeMapping[voronoi((x, y))];
            return (x, y) => GlobalBiomeMapping[voronoi(coordjitter[x, y])];
        }

        private Func<(float, float), float> CreateVoronoiNoise(IUniformRandomBitGenerator seed, float scale)
        {
            var voronoi = new Voronoi(
                new ScaledNoiseGenerator(NoiseGeneratorFactory(seed.Next64Bits()), 0.5f),
                new ScaledNoiseGenerator(NoiseGeneratorFactory(seed.Next64Bits()), 0.5f),
                scale
            );
            var noise = new CachingNoiseGenerator(NoiseGeneratorFactory(seed.Next64Bits()));
            return point => noise[voronoi.FindClosest(point)];
        }


        private static readonly Dictionary<BiomeType, ProbabilityRange<BiomeType>> LocalBiomeMapping
            = new Dictionary<BiomeType, ProbabilityRange<BiomeType>>
            {
                { BiomeType.BioOcean, new ProbabilityRange<BiomeType>(
                        (1, BiomeType.BioOcean)
                    )
                },
                { BiomeType.BioGrassland, new ProbabilityRange<BiomeType>(
                        (1, BiomeType.BioForest),
                        (1.5f, BiomeType.BioGrassland),
                        (1, BiomeType.BioHighlands)
                    )
                },
                { BiomeType.BioDesert, new ProbabilityRange<BiomeType>(
                        (1, BiomeType.BioDesert)
                    )
                },
                { BiomeType.BioTundra, new ProbabilityRange<BiomeType>(
                        (1, BiomeType.BioTundra)
                    )
                },
            };

        private static readonly ProbabilityRange<BiomeType> GlobalBiomeMapping
            = new ProbabilityRange<BiomeType>(
                (1.5f, BiomeType.BioTundra),
                (2, BiomeType.BioOcean),
                (2.5f, BiomeType.BioGrassland),
                (1.5f, BiomeType.BioDesert)
            );

        public static TwoStageBiomeGenerator Create(BiomeGeneratorSettings settings)
            => new TwoStageBiomeGenerator(settings);
    }
}
