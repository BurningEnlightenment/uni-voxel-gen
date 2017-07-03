using System;
using System.Collections.Generic;
using System.IO;
using Google.Protobuf;
using static UniDortmund.FaProSS17P3G1.MapGenerator.Model.WorldInfo.Types;
using static UniDortmund.FaProSS17P3G1.MapGenerator.Model.WorldInfo.Types.GeneratorSettings.Types;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Model
{
    public class WorldMap
    {
        private const string WorldInfoFile = "level.dat";

        public WorldMap(string levelPath)
            : this(levelPath, ReadWorldInfoFile(levelPath))
        {
        }

        private WorldMap(string levelPath, WorldInfo worldInfo)
        {
            LevelPath = levelPath;
            WorldInfo = worldInfo;
        }

        public ColumnDummy this[int x, int y] => throw new NotImplementedException();

        public string LevelPath { get; }

        public WorldInfo WorldInfo { get; }

        public static WorldMap Create(string levelPath, ulong seed,
            BiomeGeneratorType biomeGenerator = BiomeGeneratorType.UniformGrass,
            DensityGeneratorType densityGenerator = DensityGeneratorType.FlatWorld,
            TerrainGeneratorType terrainGenerator = TerrainGeneratorType.MainBlockOnly,
            params DetailsGeneratorType[] detailsGenerators)
        {
            var worldInfo = new WorldInfo
            {
                Generator = new GeneratorSettings
                {
                    BiomeGenerator = biomeGenerator,
                    DensityGenerator = densityGenerator,
                    TerrainGenerator = terrainGenerator
                }
            };
            if (detailsGenerators != null)
            {
                worldInfo.Generator.DetailsGenerator.AddRange(detailsGenerators);
            }

            Directory.CreateDirectory(levelPath);
            using (var level = File.Create(Path.Combine(levelPath, WorldInfoFile)))
            {
                worldInfo.WriteTo(level);
            }
            return new WorldMap(levelPath, worldInfo);
        }

        private static WorldInfo ReadWorldInfoFile(string levelPath)
        {
            using (var level = File.OpenRead(Path.Combine(levelPath, WorldInfoFile)))
            {
                return WorldInfo.Parser.ParseFrom(level);
            }
        }
    }
}
