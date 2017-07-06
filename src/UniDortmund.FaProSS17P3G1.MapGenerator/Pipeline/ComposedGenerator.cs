using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using NotEnoughTime.Utils.Random;
using UniDortmund.FaProSS17P3G1.MapGenerator.Algorithm;
using UniDortmund.FaProSS17P3G1.MapGenerator.Model;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Pipeline
{
    public class ComposedGenerator
    {
        public ComposedGenerator(WorldMap map,
            IBiomeGenerator biomeGenerator,
            IDensityGenerator densityGenerator,
            ITerrainGenerator terrainGenerator,
            IEnumerable<IDetailsGenerator> detailsGenerators)
        {
            mBiomeGenerator = biomeGenerator.Clone();
            mDensityGenerator = densityGenerator.Clone();
            mTerrainGenerator = terrainGenerator.Clone();
            mDetailsGenerators = detailsGenerators.Select(g => g.Clone())
                .ToImmutableArray();
        }

        private readonly IBiomeGenerator mBiomeGenerator;
        private readonly IDensityGenerator mDensityGenerator;
        private readonly ITerrainGenerator mTerrainGenerator;
        private readonly ImmutableArray<IDetailsGenerator> mDetailsGenerators;
    }
}
