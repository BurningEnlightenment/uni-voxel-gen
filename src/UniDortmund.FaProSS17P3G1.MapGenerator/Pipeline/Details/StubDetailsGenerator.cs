using System;
using NotEnoughTime.Utils;
using UniDortmund.FaProSS17P3G1.MapGenerator.Algorithm;
using UniDortmund.FaProSS17P3G1.MapGenerator.Model;
<<<<<<< HEAD
using System.Collections.Generic;
using UniDortmund.FaProSS17P3G1.MapGenerator.Pipeline.Details.objectGenerators;
=======
>>>>>>> master

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Pipeline.Details
{
    public class StubDetailsGenerator : IDetailsGenerator
    {
<<<<<<< HEAD
        public Func<ulong, INoiseGenerator> NoiseGeneratorFactory { set; private get; }

        public static StubDetailsGenerator Create(DetailsGeneratorSettings settings) => new StubDetailsGenerator(settings);
       
        public void ApplyTo(WorldMap targetMap, int x, int y)
        {}
=======
        private FastArray2D<double> NoiseMap = null;

        public Func<ulong, INoiseGenerator> NoiseGeneratorFactory { set { } }

        public static StubDetailsGenerator Create(DetailsGeneratorSettings _) => new StubDetailsGenerator();

        public void ApplyTo(WorldMap targetMap, int x, int y)
        {
            var col = targetMap[x, y];
            if (NoiseMap == null)
            {
                generateNoiseMap();
            }
            //Find Location for Flowers
            // R Dependend on Biome
            int R = 0; //TODO: FIXME
            for (int yc = 0; yc < Constants.ChunkDimension; yc++)
            {
                for (int xc = 0; xc < Constants.ChunkDimension; xc++)
                {
                    double max = 0;
                    for (int yn = yc - R; yn <= yc + R; yn++)
                    {
                        for (int xn = xc - R; xn <= xc + R; xn++)
                        {
                            double e = NoiseMap[yn,xn];
                            if (e > max) { max = e; }
                        }
                    }
                    if (NoiseMap[yc,xc] == max)
                    {
                        // place tree at xc,yc
                    }
                }
            }

        }

        private void generateNoiseMap()
        {
            Random rNum = new Random();
            for(int x =0;x<= Constants.ChunkDimension; x++)
            {
                for(int y = 0; y <= Constants.ChunkDimension; x++) {
                    NoiseMap[x, y] = rNum.NextDouble();
                }
            }
        }
>>>>>>> master
    }
}
