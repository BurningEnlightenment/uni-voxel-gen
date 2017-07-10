using System;

public class Parsing
{
	public Parsing() // SOll das der editor script sein?
	{
        // Convert.ToUInt32(hex, 16) Wo brauche ich das ?

        // C:\Users\Sabse\Uni\Fachprojekt\P3G1\level.dat 

        // für jede .colum Datei in Level.dat
        using (var input = File.OpenRead(".colum")) // Pfad?
        {
            
            foreach() // WorldBlock in Worlcolum
            {
                foreach () // ParticleField in Worldblock
                {
                    foreach () // Block in ParticleField
                    {
                        // Beko0mmt jeder ParticleField euine eigene Message??
                        type = Type.Parser.ParseFrom(input);
                        NOP = NumberOfParticles.Parser.ParseFrom(input);
                        while(NOP != 0)
                        {
                            new Gameobject = // ccordinate uas der message, type
                              NOP--;
                        }

                    }

                }
            }
            


        }
    }
}
