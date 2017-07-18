using System;
using NotEnoughTime.Utils;
using UniDortmund.FaProSS17P3G1.MapGenerator.Algorithm;
using UniDortmund.FaProSS17P3G1.MapGenerator.Model;
using System.Collections.Generic;
using UniDortmund.FaProSS17P3G1.MapGenerator.Pipeline.Details.objectGenerators;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Pipeline.Details
{
    public class StubDetailsGenerator : IDetailsGenerator
    {
        public Func<ulong, INoiseGenerator> NoiseGeneratorFactory { set; private get; }

        public static StubDetailsGenerator Create(DetailsGeneratorSettings settings) => new StubDetailsGenerator(settings);
       
        public void ApplyTo(WorldMap targetMap, int x, int y)
        {}
    }
}
