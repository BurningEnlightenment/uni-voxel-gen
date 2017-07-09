using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Google.Protobuf;
using NotEnoughTime.Utils.Random;
using UniDortmund.FaProSS17P3G1.MapGenerator.Pipeline;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Model
{
    public class WorldMap
    {
        private const string ColumnFileNameRegexString = @"^([0-9A-Z]{8})-([0-9A-Z]{8})\.column$";
        private static readonly Regex ColumnFileNameRegex = new Regex(ColumnFileNameRegexString, RegexOptions.Compiled);
        private const string WorldInfoFile = "level.dat";

        private readonly LazyMap<UnpackedColumn> mData
            = new LazyMap<UnpackedColumn>();

        /// <summary>
        /// Opens an existing level directory.
        /// </summary>
        /// <param name="levelPath"></param>
        public WorldMap(string levelPath)
            : this(levelPath, ReadWorldInfoFile(levelPath))
        {
            var levelDirInfo = new DirectoryInfo(LevelPath);
            foreach (var columnFileInfo in levelDirInfo.EnumerateFiles())
            {
                var match = ColumnFileNameRegex.Match(columnFileInfo.Name);
                if (!match.Success)
                {
                    continue;
                }

                var x = ParseCoord(match.Groups[1].Value);
                var y = ParseCoord(match.Groups[2].Value);

                using (var stream = columnFileInfo.OpenRead())
                {
                    var columnData = WorldColumn.Parser.ParseFrom(stream);
                    mData[x, y] = new UnpackedColumn(columnData);
                }
            }

            int ParseCoord(string val) => (int) Convert.ToUInt32(val, 16);
        }

        /// <summary>
        /// Create a new World MapValues based on the given world info.
        /// </summary>
        /// <param name="levelPath"></param>
        /// <param name="worldInfo"></param>
        public WorldMap(string levelPath, WorldInfo worldInfo)
        {
            LevelPath = levelPath;
            WorldInfo = worldInfo;
        }

        public UnpackedColumn this[int x, int y] => mData[x, y];

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
