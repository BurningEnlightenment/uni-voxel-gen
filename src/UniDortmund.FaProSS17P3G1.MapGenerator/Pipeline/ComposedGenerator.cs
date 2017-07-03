using System.Collections.Generic;
using System.Collections.Immutable;
using NotEnoughTime.Utils.Random;
using UniDortmund.FaProSS17P3G1.MapGenerator.Algorithm;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Pipeline
{
    public class ComposedGenerator
    {
        public ComposedGenerator(ulong seed,
            IBiomeGenerator biomeGenerator,
            IDensityGenerator densityGenerator,
            ITerrainGenerator terrainGenerator,
            IEnumerable<IDetailsGenerator> detailsGenerators)
        {
            Seed = seed;
            BiomeGenerator = biomeGenerator;
            DensityGenerator = densityGenerator;
            TerrainGenerator = terrainGenerator;
            DetailsGenerators = detailsGenerators.ToImmutableArray();

            var seeder = XoroShiro128Plus.Create(seed);
            foreach (var noisyGenerator in DetailsGenerators.CastArray<INoisyGenerator>()
                .Add(BiomeGenerator)
                .Add(DensityGenerator)
                .Add(TerrainGenerator))
            {
                noisyGenerator.NoiseGenerator
                    = new SimplexNoiseGenerator(seeder.Next64Bits());
            }
        }

        public ulong Seed { get; }
        
        public IBiomeGenerator BiomeGenerator { get; }
        public IDensityGenerator DensityGenerator { get; }
        public ITerrainGenerator TerrainGenerator { get; }
        public ImmutableArray<IDetailsGenerator> DetailsGenerators { get; }
    }
}
