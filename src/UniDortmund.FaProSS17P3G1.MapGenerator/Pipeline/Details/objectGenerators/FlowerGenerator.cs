using System;
using System.Collections.Generic;
using System.Text;
using UniDortmund.FaProSS17P3G1.MapGenerator.Model;

namespace UniDortmund.FaProSS17P3G1.MapGenerator.Pipeline.Details.objectGenerators
{
    class FlowerGenerator
    {
        public UnpackedColumn AddElement(UnpackedColumn col, int x, int y,int z)
        {
            //planted with seed, earth and water but without love :(
            if (col[x, y, z + 1] == ParticleType.PtDebug)
            {
                col.ChunkByZ(z+1).Data.Fill(() => ParticleType.PtAir);
            }
            col[x,y,z+1] = ParticleType.PtRose;
            return col;
        }
    }
}
