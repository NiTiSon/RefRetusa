using System;
using System.Collections.Generic;
using System.IO;
using Serilog;
using Serilog.Core;
using YamlDotNet.RepresentationModel;
using SystemStringReader = System.IO.StringReader;

namespace RefRetusa;

public static class EntryPoint
{
	private static void Main(string[] args)
	{
		Log.Logger = new LoggerConfiguration()
			.WriteTo.Console()
			.CreateLogger();
		
		Engine engine = new();

		try
		{
			string optionsFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".refretusa.yml");

			string options;
			if (!File.Exists(optionsFilePath))
			{
				options = string.Empty;
				File.Create(optionsFilePath).Close();
				File.SetAttributes(optionsFilePath, File.GetAttributes(optionsFilePath) | FileAttributes.Hidden); // Cleaner user directory
			}
			else
			{
				options = File.ReadAllText(optionsFilePath);
			}

			SystemStringReader sr = new(options);

			YamlStream ys = new();
			ys.Load(sr);

			YamlDocument settings = ys.Documents[0];
			YamlMappingNode? node = settings.RootNode as YamlMappingNode;

			foreach (KeyValuePair<YamlNode, YamlNode> nodes in node.Children)
			{
			}
		}
		catch (ArgumentOutOfRangeException) // Ignore cause of empty options file
		{
			engine.SetDefaultOptions();
		}
		catch (Exception ex)
		{
			Log.Error("Error \"{0}\" handled during options file reading/initializing: {1}", ex.GetType(), ex.Message);
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