using System;
using System.IO;
using YamlDotNet.RepresentationModel;
using SystemStringReader = System.IO.StringReader;

namespace RefRetusa;

public static class EntryPoint
{
	private static void Main(string[] args)
	{
		Engine engine = new();
		try
		{
			string optionsFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".refretusa.yml");

			string options;
			if (!File.Exists(optionsFilePath))
			{
				options = string.Empty;
				File.Create(optionsFilePath).Close();
			}
			else
			{
				options = File.ReadAllText(optionsFilePath);
			}

			SystemStringReader sr = new(options);

			YamlStream ys = new();
			ys.Load(sr);

			YamlDocument settings = ys.Documents[0];
		}
		catch (ArgumentOutOfRangeException)
		{
			logger.Log("Mda");
			Environment.Exit(-1);
		}

		if (args.Length is 0)
		{
			Console.WriteLine($"""
&Retusa aka RefRetusa - a program to compile, create, clean your projects
Version: {typeof(EntryPoint).Assembly.GetName().Version?.ToString(3) ?? "unknown"}
""");
		}
		
		//	commands:
		//	ext | extension
		//		list (show all extensions applied to &Retusa Engine)
		//		deactivate (deactivate extension by name or id)
		//		activate (activate extension by name or id)
		//
		//	build
		//		-o | --out {string} (sets output directory)
		//
		//	config
		//		[options] (sets specific value to specific option)
	}
}