using RefRetusa.Logging;
using RefRetusa.Structs.Collections;
using System;
using System.Diagnostics;

namespace RefRetusa;

public static class RetusaArguments
{
	public static RetusaArgument<Verbose> Verbose;
	public static ConfigurationTable EnvironmentValues;
	public static string Path;
	public static string EntryFileOrDirectoryPath;
	internal static bool Parse(string[] args)
	{
		Span<string> _args = new(args);

		if (args[0] is "-h" or "--help")
		{
			Logger.Log(
				$$"""
				RefRetusa {{Meta.Version}}
				Usage:
					RefRetusa{{Meta.ExecutionExtension}} [--help|arguments...] {project file|project directory}

				Arguments:
					-v:{VALUE} | --verbose:{VALUE}
					Controls the amount of information output during a build
					VALUE:
						NONE - display only help menu 
						ERROR - display only errors (default)
						WARN - display errors and warnings
						INFO - display everything

					-p:{NAME}[={VALUE}]
					Set value to environment variable
					NAME: any string (without whitespaces)
					VALUE: any string (without whitespaces)
				""", Logging.Verbose.None);
			Environment.Exit(0);
			return true;
		}

		for (int i = 0; i < _args.Length; i++)
		{
			string arg = _args[i];

			if (arg.StartsWith("-v:") || arg.StartsWith("--verbose:"))
			{
				Verbose.Parse(arg.Substring(arg[1] == '-' ? 10 : 3));
			}
			else if (arg.StartsWith("-p:"))
			{
				string txt = arg.Substring(3);

				int indexOfEq = txt.IndexOf('=');

				if (indexOfEq == -1)
				{
					EnvironmentValues[txt] = string.Empty;
				}
				else
				{
					string key = txt.Substring(0, indexOfEq);
					string val = txt.Substring(indexOfEq + 1);
					EnvironmentValues[key] = val;
				}
			}
			else if (arg.StartsWith('-'))
			{
				Logger.Log($"Unknown argument: {arg}", Logging.Verbose.Error);
				return false;
			}
			else
			{
				if (i < _args.Length - 1)
				{
					Logger.Log("Path to project file or directory must be last argument", Logging.Verbose.Error);
					return false;
				}
				else
				{
					EntryFileOrDirectoryPath = arg;
				}
			}

			if (i == _args.Length-1 && EntryFileOrDirectoryPath is null)
			{
				Logger.Log("Path to project file or directory must be set", Logging.Verbose.Error);
				return false;
			}

		}
		return true;
	}

	static RetusaArguments()
	{
		Verbose = new(verboseParse);
		EnvironmentValues = new();

		static ActionResult<Verbose, string> verboseParse(string text)
		{
			switch (text.ToLower())
			{
				case "none":
				case "n":
				case "0":
					return Logging.Verbose.None;
				case "error":
				case "e":
				case "1":
					return Logging.Verbose.Error;
				case "warn":
				case "warning":
				case "w":
				case "2":
					return Logging.Verbose.Warning;
				case "info":
				case "all":
				case "i":
				case "3":
					return Logging.Verbose.Info;

				default:
					return ActionResult<Verbose, string>.FromError($"Unknown verbose type");
			}
		}
	}
}