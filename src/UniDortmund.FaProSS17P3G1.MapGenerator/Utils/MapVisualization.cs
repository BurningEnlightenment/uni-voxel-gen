using System.Collections.Generic;
using System.IO;
using ImageSharp;
using SixLabors.Primitives;
using UniDortmund.FaProSS17P3G1.MapGenerator.Model;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Utils
{
    public static class MapVisualization
    {
        private const int BiomePicChunkDimension = Constants.ChunkDimension + 1;

        private static readonly Dictionary<BiomeType, Rgba32> BiomeColor
            = new Dictionary<BiomeType, Rgba32>
            {
                { BiomeType.BioDebug, Rgba32.Pink },
                { BiomeType.BioOcean, Rgba32.Blue },
                { BiomeType.BioGrassland, Rgba32.Green },
                { BiomeType.BioForest, Rgba32.DarkGreen },
                { BiomeType.BioHighlands, Rgba32.LightGreen },
                { BiomeType.BioTundra, Rgba32.Cyan },
                { BiomeType.BioDesert, Rgba32.Yellow },
            };

        public static void PrintBiomeMap(this WorldMap map)
        {
            map.CreateDirectory();
            var heigth = map.MapData.SizeX * BiomePicChunkDimension - 1;
            var width = map.MapData.SizeY * BiomePicChunkDimension - 1;

            using (var image = new Image<Rgba32>(Configuration.Default, heigth, width))
            {
                image.BackgroundColor(Rgba32.Black);
                foreach (var column in map.MapData)
                {
                    var xpx = (column.X - map.MapData.OriginX) * BiomePicChunkDimension;
                    var ypx = width - (column.Y - map.MapData.OriginY) * BiomePicChunkDimension - 1;

                    if (column.Value == null)
                    {
                        //image.Fill(Rgba32.Pink, new RectangleF(xpx, ypx, Constants.ChunkDimension, Constants.ChunkDimension));
                        continue;
                    }

                    var lastY = 0;
                    var rowspan = image.GetRowSpan(ypx);
                    foreach (var biome in column.Value.BiomeMap)
                    {
                        rowspan = lastY == biome.Y ? rowspan : image.GetRowSpan(ypx - biome.Y);
                        lastY = biome.Y;

                        rowspan[xpx + biome.X] = BiomeColor[biome.Value];
                    }
                }
                image.Save(Path.Combine(map.LevelPath, "biomes.png"));
            }
        }
    }
}
