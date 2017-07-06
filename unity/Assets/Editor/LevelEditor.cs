using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Text.RegularExpressions;
using System;
using UniDortmund.FaProSS17P3G1.MapGenerator.Model;
using System.IO;

[CustomEditor(typeof(LevelEditor))]
public class LevelEditor : Editor
{
    private const string FileNameRegexString = "^(?<xC>[0-9A-F]{8})-(?<yC>[0-9A-F]{8}).column$";
    private static Regex FileNameRegex = new Regex(FileNameRegexString, RegexOptions.Compiled);

    public void openColums(string path)
    {
        System.IO.DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo(path);
        var origX = 0;
        int origY = 0;

        foreach (System.IO.FileInfo f in ParentDirectory.GetFiles())
        {
            if (f.Name.Contains(".column"))
            {

                Match match = FileNameRegex.Match(f.Name);
                origX = (int)Convert.ToUInt32(match.Groups["xC"].Value, 16);
                origY = (int)Convert.ToUInt32(match.Groups["yC"].Value, 16);

                WorldColumn data;
                using (var input = File.OpenRead(f.ToString()))
                {
                    data = WorldColumn.Parser.ParseFrom(input);
                }

                doStuff((origX*16), (origY*16), data);

            }
        }
    }

    private void doStuff(int x, int y, WorldColumn data)
    {
        var origX = x;
        int origY = y;
        int round = 0;
        bool neg = true;

        // Erstelle Ordner X-Y
        foreach (WorldBlock block in data.Blocks)
        {
            x = origX;
            y = origY;
            int z;
            // Erstelle Ordner block nummer
            // Erstelle Ordner höhe 0
            // Erstelle Ordner y = 0
            if (neg == false)
            {
                z = (round * 16);
                neg = true;
            }
            else
            {
                z = -(round * 16);
                neg = false;
                round++;
            }

            foreach (ParticleField field in block.ParticleFields)
            {
                for (var NOP = 0; NOP < field.NumberOfParticles; ++NOP)
                {
                    if(field.Type != ParticleType.PtAir)
                    {
                        // Erstelle Game Object an x,y,z
                    }
                    if (x == 15)
                    {
                        // Erstelle Ordner y++
                        x = origX-1;
                        y++;
                    }
                    if (y == 15)
                    {
                        // Erstelle Ordner z++
                        // Erstelle Ordner y
                        y = origY;
                        z++;
                    }
                    x++;
                }

            }
        }
    }
}
