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
    private const string FileNameRegexString = "^([0-9A-F]{8})-([0-9A-F]{8}).column$";
    private static Regex FileNameRegex = new Regex(FileNameRegexString, RegexOptions.Compiled);
    GameObject levelG;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Build"))
        {
            openColums(@"D:\P3G1\src\UniDortmund.FaProSS17P3G1.MapGenerator.Tests\bin\Debug\netcoreapp1.1\tmp\test-level");
        }
        if (GUILayout.Button("Delete"))
        {
            GameObject.DestroyImmediate(levelG);
        }
    }
    public void openColums(string path)
    {
        System.IO.DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo(path);
        var origX = 0;
        int origY = 0;
        levelG = new GameObject("Level");
        foreach (System.IO.FileInfo f in ParentDirectory.GetFiles())
        {
            Match match = FileNameRegex.Match(f.Name);
            if (match.Success)
            {
                var xC = match.Groups[1].Value;
                var yC = match.Groups[2].Value;
                origX = (int)Convert.ToUInt32(xC, 16);
                origY = (int)Convert.ToUInt32(yC, 16);

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
        int origZ;
        int blockW = 0;
        string wBlock, wColumn, zheight, yheight;

        // Erstelle Ordner X-Y
        wColumn = "("+origX + ") - (" + origY +")";
        GameObject wColumnG = new GameObject(wColumn);
        wColumnG.transform.parent = levelG.transform;
        
        foreach (WorldBlock block in data.Blocks)
        {
            x = origX;
            y = origY;
            origZ = ZigZagDec(blockW) * 16;
            int z = origZ;
            // Erstelle Ordner block nummer
            wBlock = "B: "+blockW;
            GameObject wBlockG = new GameObject(wBlock);
            wBlockG.transform.parent = wColumnG.transform;
            blockW++;
            // Erstelle Ordner höhe 0
            zheight ="Z: " +z;
            GameObject zheightG = new GameObject(zheight);
            zheightG.transform.parent = wBlockG.transform;
            // Erstelle Ordner y = 0
            yheight = "Y: "+origY;
            GameObject yheightG = new GameObject(yheight);
            yheightG.transform.parent = zheightG.transform;

            

            foreach (ParticleField field in block.ParticleFields)
            {
                for (var NOP = 0; NOP < field.NumberOfParticles; ++NOP)
                {
                    if(field.Type != ParticleType.PtAir)
                    {
                        // Erstelle Game Object an x,y,z
                        GameObject voxel=Instantiate(Resources.Load<GameObject>("Prefabs/"+Enum.GetName(typeof(ParticleType), field.Type)), new Vector3(x, z, y), Quaternion.identity) as GameObject;
                        voxel.transform.parent = yheightG.transform;
                    }
                    if (x == origX+15)
                    {
                        // Erstelle Ordner y++

                        x = origX-1;
                        y++;
                        yheight ="Y: "+ y;
                        yheightG = new GameObject(yheight);
                        yheightG.transform.parent = zheightG.transform;
                    }
                    if (y == origY+16)
                    {
                        y = origY;
                        z++;
                        if (z != origZ+16)
                        {
                            // Erstelle Ordner z++
                            zheight = "Z: " + z;
                            zheightG = new GameObject(zheight);
                            zheightG.transform.parent = wBlockG.transform;
                            // Erstelle Ordner y
                            yheight = "Y: " + origY;
                            yheightG = new GameObject(yheight);
                            yheightG.transform.parent = zheightG.transform;
                        }
                    }
                    x++;
                }

            }
        }
    }

    public static int ZigZagEnc(int val)
    {
        return (val << 1) ^ (val >> 31);
    }

    public static int ZigZagDec(int val)
    {
        return (int)((uint)val >> 1) ^ -(val & 1);
    }
}
