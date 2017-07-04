using System;
using System.Collections.Generic;
using System.IO;
using Google.Protobuf;
using NotEnoughTime.Utils.Random;
using static UniDortmund.FaProSS17P3G1.MapGenerator.Model.BiomeGeneratorSettings.Types;
using static UniDortmund.FaProSS17P3G1.MapGenerator.Model.DensityGeneratorSettings.Types;
using static UniDortmund.FaProSS17P3G1.MapGenerator.Model.TerrainGeneratorSettings.Types;
using static UniDortmund.FaProSS17P3G1.MapGenerator.Model.DetailsGeneratorSettings.Types;

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

        public static WorldMap Create(string levelPath, ulong seed)
        {
            IUniformRandomBitGenerator prng = XoroShiro128Plus.Create(seed);

            var worldInfo = new WorldInfo
            {
                BiomeGenerator = new BiomeGeneratorSettings
                {
                    Seed = prng.Next64Bits()
                },
                DensityGenerator = new DensityGeneratorSettings
                {
                    Seed = prng.Next64Bits()
                },
                TerrainGenerator = new TerrainGeneratorSettings
                {
                    Seed = prng.Next64Bits()
                }
            };

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
