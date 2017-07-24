using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using UniDortmund.FaProSS17P3G1.MapGenerator.Model;
using static UniDortmund.FaProSS17P3G1.MapGenerator.Model.BiomeGeneratorSettings.Types;
using static UniDortmund.FaProSS17P3G1.MapGenerator.Model.DensityGeneratorSettings.Types;
using static UniDortmund.FaProSS17P3G1.MapGenerator.Model.TerrainGeneratorSettings.Types;
using static UniDortmund.FaProSS17P3G1.MapGenerator.Model.DetailsGeneratorSettings.Types;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Pipeline
{
    public class GeneratorComposer
    {
        public delegate T GeneratorFactory<out T, in TSettings>(TSettings settings);

        private static readonly ImmutableDictionary<BiomeGeneratorType, GeneratorFactory<IBiomeGenerator, BiomeGeneratorSettings>> AvBiomeGenerators
            = ImmutableDictionary<BiomeGeneratorType, GeneratorFactory<IBiomeGenerator, BiomeGeneratorSettings>>.Empty
                .Add(BiomeGeneratorType.Uniform, Biome.UniformBiomeGenerator.Create)
                .Add(BiomeGeneratorType.TwoStage, Biome.TwoStageBiomeGenerator.Create);

        private static readonly ImmutableDictionary<DensityGeneratorType, GeneratorFactory<IDensityGenerator, DensityGeneratorSettings>> AvDensityGenerators
            = ImmutableDictionary<DensityGeneratorType, GeneratorFactory<IDensityGenerator, DensityGeneratorSettings>>.Empty
                .Add(DensityGeneratorType.FlatWorld, Density.FlatWorldGenerator.Create);

        private static readonly ImmutableDictionary<TerrainGeneratorType, GeneratorFactory<ITerrainGenerator, TerrainGeneratorSettings>> AvTerrainGenerators
            = ImmutableDictionary<TerrainGeneratorType, GeneratorFactory<ITerrainGenerator, TerrainGeneratorSettings>>.Empty
                .Add(TerrainGeneratorType.MainBlockOnly, Terrain.MainBlockOnlyTerrainGenerator.Create)
                .Add(TerrainGeneratorType.Simple, Terrain.SimpleTerrainGenerator.Create);

        public static readonly ImmutableDictionary<DetailsGeneratorType,
            GeneratorFactory<IDetailsGenerator, DetailsGeneratorSettings>> AvDetailsGenerators
            = ImmutableDictionary<DetailsGeneratorType, GeneratorFactory<IDetailsGenerator, DetailsGeneratorSettings>>.Empty
                .Add(DetailsGeneratorType.None, Details.StubDetailsGenerator.Create)
                .Add(DetailsGeneratorType.Basic, Details.BasicDetailsGenerator.Create);

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

        public static IBiomeGenerator DeriveBiomeGenerator(BiomeGeneratorSettings settings)
            => AvBiomeGenerators[settings.Type](settings);

        public static IDensityGenerator DeriveDensityGenerator(DensityGeneratorSettings settings)
            => AvDensityGenerators[settings.Type](settings);

        public static IDetailsGenerator DeriveDetailsGenerator(DetailsGeneratorSettings settings)
            => AvDetailsGenerators[settings.Type](settings);

        public static ITerrainGenerator DerivTerrainGenerator(TerrainGeneratorSettings settings)
            => AvTerrainGenerators[settings.Type](settings);

        public static ComposedGenerator CreateFrom(WorldInfo generatorSettings)
        {
            throw new NotImplementedException();
        }
    }
}
