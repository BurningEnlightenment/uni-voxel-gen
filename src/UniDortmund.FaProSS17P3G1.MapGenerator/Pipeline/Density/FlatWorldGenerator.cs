using System;
using System.Collections.Generic;
using System.Text;
using NotEnoughTime.Utils;
using UniDortmund.FaProSS17P3G1.MapGenerator.Algorithm;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Pipeline.Density
{
    public class FlatWorldGenerator : IDensityGenerator, IDeepClonable<FlatWorldGenerator>
    {
        public INoiseGenerator NoiseGenerator { set { } }

        public static FlatWorldGenerator Create() => new FlatWorldGenerator();

        public FlatWorldGenerator Clone()
        {
            return new FlatWorldGenerator();
        }
        IDensityGenerator IDeepClonable<IDensityGenerator>.Clone() => Clone();
    }
}
