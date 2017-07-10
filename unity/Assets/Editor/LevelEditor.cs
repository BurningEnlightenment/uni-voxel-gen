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
    private string basepath = "";

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
        int z;
        int block = 1;
        string wBlock, wColumn, zheight, yheight,path;

        // Erstelle Ordner X-Y
        wColumn = origX.toString() + "-" + origY.toString();
        Directory.CreateDirectory(basepath+"/"+wColumn);
        foreach (WorldBlock block in data.Blocks)
        {
            x = origX;
            y = origY;
            // Erstelle Ordner block nummer
            wBlock = wColumn +"/"+block.toString();
            Directory.CreateDirectory(basepath + "/" + wBlock);
            block++;
            // Erstelle Ordner höhe 0
            zheight =wBlock+"/"+ z.toString();
            Directory.CreateDirectory(basepath + "/" + zheight);
            // Erstelle Ordner y = 0
            yheight = zheight +"/"+ origY.toString();
            Directory.CreateDirectory(basepath + "/" + yheight);
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
                        path = basepath + "/"+ yheight+"/";
                        Directory.SetCurrentDirectory(path);
                        Gameobject voxel = new Gameobject(ParticleType.toString()) ;
                        voxel.transform.localPosition = newVector3(x, y, z);

                        // Prefabs benötigt eventuell anpassen 
                    }
                    if (x == 15)
                    {
                        // Erstelle Ordner y++
                        x = origX-1;
                        y++;
                        yheight = zheight + "/" + y.toString();
                        Directory.CreateDirectory(basepath + "/" + yheight);
                    }
                    if (y == 15)
                    {
                        y = origY;
                        z++;
                        // Erstelle Ordner z++
                        zheight = wBlock + "/" + z.toString();
                        Directory.CreateDirectory(basepath + "/" + zheight);
                        // Erstelle Ordner y
                        yheight = zheight + "/" + origY.toString();
                        Directory.CreateDirectory(basepath + "/" + yheight);

                    }
                    x++;
                }

            }
        }
    }
}
