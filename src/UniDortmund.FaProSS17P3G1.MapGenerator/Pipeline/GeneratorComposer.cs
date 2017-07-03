using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using static UniDortmund.FaProSS17P3G1.MapGenerator.Model.WorldInfo.Types;
using static UniDortmund.FaProSS17P3G1.MapGenerator.Model.WorldInfo.Types.GeneratorSettings.Types;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Pipeline
{
    public class GeneratorComposer
    {
        public delegate T GeneratorFactory<out T>();

        private static readonly ImmutableDictionary<BiomeGeneratorType, GeneratorFactory<IBiomeGenerator>> AvBiomeGenerators
            = ImmutableDictionary<BiomeGeneratorType, GeneratorFactory<IBiomeGenerator>>.Empty
                .Add(BiomeGeneratorType.UniformGrass, Biome.UniformBiomeGenerator.Create);

        private static readonly ImmutableDictionary<DensityGeneratorType, GeneratorFactory<IDensityGenerator>> AvDensityGenerators
            = ImmutableDictionary<DensityGeneratorType, GeneratorFactory<IDensityGenerator>>.Empty
                .Add(DensityGeneratorType.FlatWorld, Density.FlatWorldGenerator.Create);

        private static readonly ImmutableDictionary<TerrainGeneratorType, GeneratorFactory<ITerrainGenerator>> AvTerrainGenerators
            = ImmutableDictionary<TerrainGeneratorType, GeneratorFactory<ITerrainGenerator>>.Empty
                .Add(TerrainGeneratorType.MainBlockOnly, Terrain.MainBlockOnlyTerrainGenerator.Create);

        public static readonly ImmutableDictionary<DetailsGeneratorType, GeneratorFactory<IDetailsGenerator>> AvDetailsGenerators
            = ImmutableDictionary<DetailsGeneratorType, GeneratorFactory<IDetailsGenerator>>.Empty
                .Add(DetailsGeneratorType.None, Details.StubDetailsGenerator.Create);

        public ulong Seed;

        public IBiomeGenerator BiomeGenerator;
        public IDensityGenerator DensityGenerator;
        public ITerrainGenerator TerrainGenerator;
        public ImmutableArray<IDetailsGenerator> DetailsGenerators;

        public GeneratorComposer()
        {
        }

        public GeneratorComposer(ulong seed)
        {
            Seed = seed;
        }

        public static ComposedGenerator CreateFrom(GeneratorSettings generatorSettings)
        {
            throw new NotImplementedException();
        }

        public ComposedGenerator Create()
            => new ComposedGenerator(Seed, BiomeGenerator, DensityGenerator, TerrainGenerator, DetailsGenerators);

        public GeneratorComposer WithSeed(ulong seed)
            => SetProp(out Seed, seed);

        public GeneratorComposer WithBiomeGenerator(IBiomeGenerator generator)
            => SetProp(out BiomeGenerator, generator);
        public GeneratorComposer WithBiomeGenerator(BiomeGeneratorType generatorType)
            => WithBiomeGenerator(AvBiomeGenerators[generatorType]());

        public GeneratorComposer WithDensityGenerator(IDensityGenerator generator)
            => SetProp(out DensityGenerator, generator);
        public GeneratorComposer WithDensityGenerator(DensityGeneratorType generatorType)
            => WithDensityGenerator(AvDensityGenerators[generatorType]());

        public GeneratorComposer WithDensityGenerator(ITerrainGenerator generator)
            => SetProp(out TerrainGenerator, generator);
        public GeneratorComposer WithDensityGenerator(TerrainGeneratorType generatorType)
            => WithDensityGenerator(AvTerrainGenerators[generatorType]());

        public GeneratorComposer WithDetailsGenerator(IDetailsGenerator generator)
            => SetProp(out DetailsGenerators, DetailsGenerators.Add(generator));
        public GeneratorComposer WithDetailsGenerator(DetailsGeneratorType generatorType)
            => WithDetailsGenerator(AvDetailsGenerators[generatorType]());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private GeneratorComposer SetProp<T>(out T prop, T value)
        {
            prop = value;
            return this;
        }
    }
}
