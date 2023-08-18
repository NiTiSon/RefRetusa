using System;
using System.IO;
using System.Linq;
using GlobExpressions;

namespace RefRetusa;

public static class EntryPoint
{
	private static void Main(string[] args)
	{
		Engine engine = new();

		if (args.Length == 0) // Show help message
		{
			engine.Execute("help()");
		}
		else if (args.Length == 1) // auto-find file .ret or .retproj and launch arg[0] expression
		{
			string retFiles = args[0];

			string[] files;
			if (File.Exists(retFiles))
			{
				files = new string[] { retFiles };
			}
			else
			{
				if (OperatingSystem.IsWindows() && Path.IsPathRooted(retFiles))
				{
					string root = Path.GetPathRoot(retFiles)!.Replace('\\', '/');
					string rootlesPath = retFiles[root.Length..^0];

					files = Glob.Files(new DirectoryInfo(root), rootlesPath).Select(f => f.FullName).ToArray();
				}
				else
				{
					files = Glob.Files(Environment.CurrentDirectory, retFiles).ToArray();
				}
			}

			
			if (files.Length == 0)
			{
				Console.WriteLine("File not found!");
			}
			else if (files.Length == 1)
			{
				// Analys file and execute it
			}
			else
			{
				Console.WriteLine("Found more that 1 &Retusa file:");
				int i = 0; const int DisplayLimit = 8;
				foreach (string file in files)
				{
					if (i++ >= DisplayLimit)
					{
						Console.WriteLine("\t...");
						break;
					}

					Console.Write('\t');
					Console.WriteLine(file);
				}
				return;
			}
		}
		else if (args.Length > 2) // execute 
		{

		}
	}
}