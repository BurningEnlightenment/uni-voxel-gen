using System;
using CommandLine;
using CSharpx;

namespace UniDortmund.FaProSS17P3G1.MapGenerator
{
    internal class CmdOptions
    {
        [Option('o', "output-file", HelpText = "The Level Output File")]
        public string OutputFilePath { get; set; }

        [Option('l', "level", HelpText = "An existing level file (level.dat).")]
        public string LevelFile { get; set; }

        [Option("debug-directory", HelpText = "The directory where all debug files will be written to.")]
        public string DebugDirectoryPath { get; set; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            if (Parser.Default.ParseArguments<CmdOptions>(args)
                    .ToMaybe()
                    .MatchJust(out var options))
            {
                
            }
            else
            {
                Console.WriteLine("Invalid Options");
            }
        }
    }
}