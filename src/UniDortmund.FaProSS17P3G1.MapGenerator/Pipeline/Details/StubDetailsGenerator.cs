﻿using System;
using NotEnoughTime.Utils;
using UniDortmund.FaProSS17P3G1.MapGenerator.Algorithm;
using UniDortmund.FaProSS17P3G1.MapGenerator.Model;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Pipeline.Details
{
    public class StubDetailsGenerator : IDetailsGenerator
    {
        public INoiseGenerator NoiseGenerator { get; set; }

        public DetailsGeneratorSettings Settings
        {
            set { }
        }

        public static StubDetailsGenerator Create(DetailsGeneratorSettings _) => new StubDetailsGenerator();

        public void ApplyTo(WorldMap targetMap, int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
