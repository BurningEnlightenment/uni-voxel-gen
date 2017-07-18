using System;
using NotEnoughTime.Utils;
using UniDortmund.FaProSS17P3G1.MapGenerator.Algorithm;
using UniDortmund.FaProSS17P3G1.MapGenerator.Model;
using System.Collections.Generic;
using UniDortmund.FaProSS17P3G1.MapGenerator.Pipeline.Details.objectGenerators;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Pipeline.Details
{
    public class BasicDetailsGenerator : IDetailsGenerator
    {
        private FastArray2D<double> NoiseMap = null;

        public Dictionary<BiomeType, int> TreeLikeObjectDensity =
            new Dictionary<BiomeType, int> {{BiomeType.BioGrassland, 3}, {BiomeType.BioDebug, 3}};
        public Dictionary<BiomeType, int> FlowerLikeObjectDensity =
            new Dictionary<BiomeType, int> { { BiomeType.BioGrassland, 1 }, { BiomeType.BioDebug, 1 } };

        private ulong seed;

        public Func<ulong, INoiseGenerator> NoiseGeneratorFactory { set; private get; }

        public static BasicDetailsGenerator Create(DetailsGeneratorSettings settings) => new BasicDetailsGenerator(settings);

        public BasicDetailsGenerator(DetailsGeneratorSettings settings)
        {
            seed = settings.Seed;
        }

        public void ApplyTo(WorldMap targetMap, int x, int y)
        {

            var col = targetMap[x, y];
            //Find Location for Flowers 
            // R Dependend on Biome
            for (int yc = 0; yc < Constants.ChunkDimension; yc++)
            {
                for (int xc = 0; xc < Constants.ChunkDimension; xc++)
                {
                    int R = FlowerLikeObjectDensity[col.BiomeMap[yc, xc]];
                    double max = 0;
                    for (int yn = yc - R; yn <= yc + R; yn++)
                    {
                        for (int xn = xc - R; xn <= xc + R; xn++)
                        {
                            double e = noiseMap(yn,xn);
                            if (e > max) { max = e; }
                        }
                    }
                    if (noiseMap(yc,xc) == max)
                    {
                        // place tree at xc,yc
                        var generator = new FlowerGenerator();
                        int zc = 1; //get from heightmap
                        generator.AddElement(col, yc, xc, zc);
                    }
                }
            }

        }

        private double noiseMap(int x, int y)
        {
            var noise = NoiseGeneratorFactory(seed);
            return noise[x, y];
        }
    }
}
