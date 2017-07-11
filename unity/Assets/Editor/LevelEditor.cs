using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Text.RegularExpressions;
using System;
using UniDortmund.FaProSS17P3G1.MapGenerator.Model;
using System.IO;

[CustomEditor(typeof(World))]
public class LevelEditor : Editor
{
    private const string FileNameRegexString = "^(?<xC>[0-9A-F]{8})-(?<yC>[0-9A-F]{8}).column$";
    private static Regex FileNameRegex = new Regex(FileNameRegexString, RegexOptions.Compiled);

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Build"))
        {
            openColums("");
        }
       // if (GUILayout.Button("Delete"))
       // {
       // }
    }
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
        int z = 0;
        int blockW = 1;
        string wBlock, wColumn, zheight, yheight;

        // Erstelle Ordner X-Y
        wColumn = origX + "-" + origY;
        GameObject wColumnG = new GameObject(wColumn);
        foreach (WorldBlock block in data.Blocks)
        {
            x = origX;
            y = origY;

            // Erstelle Ordner block nummer
            wBlock = blockW +"";
            GameObject wBlockG = new GameObject(wBlock);
            wBlockG.transform.parent = wColumnG.transform;
            blockW++;
            // Erstelle Ordner höhe 0
            zheight = z+"";
            GameObject zheightG = new GameObject(zheight);
            zheightG.transform.parent = wBlockG.transform;
            // Erstelle Ordner y = 0
            yheight = origY+"";
            GameObject yheightG = new GameObject(yheight);
            yheightG.transform.parent = zheightG.transform;

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
                        GameObject voxel=Instantiate(Resources.Load<GameObject>("Prefabs/"+Enum.GetName(typeof(ParticleType), field.Type)), new Vector3(x, y, z), Quaternion.identity) as GameObject;
                        voxel.transform.parent = yheightG.transform;
                    }
                    if (x == 15)
                    {
                        // Erstelle Ordner y++

                        x = origX-1;
                        y++;
                        yheight =""+ y;
                        yheightG = new GameObject(yheight);
                        yheightG.transform.parent = zheightG.transform;
                    }
                    if (y == 15)
                    {
                        y = origY;
                        z++;

                        // Erstelle Ordner z++
                        zheight = "" + z;
                        zheightG = new GameObject(zheight);
                        zheightG.transform.parent = wBlockG.transform;
                        // Erstelle Ordner y
                        yheight = ""+ origY;
                        yheightG = new GameObject(yheight);
                        yheightG.transform.parent = zheightG.transform;

                    }
                    x++;
                }

            }
        }
    }
}
