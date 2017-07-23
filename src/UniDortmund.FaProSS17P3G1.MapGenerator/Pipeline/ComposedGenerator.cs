using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.InteropServices;
using NotEnoughTime.Utils.Random;
using UniDortmund.FaProSS17P3G1.MapGenerator.Algorithm;
using UniDortmund.FaProSS17P3G1.MapGenerator.Model;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Pipeline
{
    public class ComposedGenerator
    {
        private readonly WorldMap mTargetMap;
        private readonly IBiomeGenerator mBiomeGenerator;
        private readonly IDensityGenerator mDensityGenerator;
        private readonly ITerrainGenerator mTerrainGenerator;
        private readonly ImmutableArray<IDetailsGenerator> mDetailsGenerators;

        public ComposedGenerator(WorldMap map)
        {
            mTargetMap = map;
            var mapParams = map.WorldInfo;

            mBiomeGenerator = GeneratorComposer.DeriveBiomeGenerator(mapParams.BiomeGenerator);
            mDensityGenerator = GeneratorComposer.DeriveDensityGenerator(mapParams.DensityGenerator);
            mTerrainGenerator = GeneratorComposer.DerivTerrainGenerator(mapParams.TerrainGenerator);

            mDetailsGenerators = (mapParams.DetailsGenerators ?? Enumerable.Empty<DetailsGeneratorSettings>())
                .Select(GeneratorComposer.DeriveDetailsGenerator)
                .ToImmutableArray();

            foreach (var generator in GeneratorSequence)
            {
                generator.NoiseGeneratorFactory =
                    seed => new SimplexNoiseGenerator(seed);
            }
        }

        public void Generate(int x, int y, int sizeX, int sizeY)
        {
            if (sizeX == 0)
            {
                throw new ArgumentException("must not be 0", nameof(sizeX));
            }
            if (sizeY == 0)
            {
                throw new ArgumentException("must not be 0", nameof(sizeY));
            }
            if (sizeX < 0)
            {
                x += sizeX;
                sizeX = -sizeX;
            }
            if (sizeY < 0)
            {
                y += sizeY;
                sizeY = -sizeY;
            }
            var targetX = x + sizeX;
            var targetY = y + sizeY;

            for (var i = x; i < targetX; ++i)
            {
                for (var j = y; j < targetY; ++j)
                {
                    Generate(i, j);
                }
            }
        }

        private void Generate(int x, int y)
        {
            foreach (var generator in GeneratorSequence)
            {
                generator.ApplyTo(mTargetMap, x, y);
            }
        }

        private IEnumerable<INoisyGenerator> GeneratorSequence
            => Enumerable.Empty<INoisyGenerator>()
                .Append(mBiomeGenerator)
                .Append(mDensityGenerator)
                .Append(mTerrainGenerator)
                .Concat(mDetailsGenerators);
    }
}
