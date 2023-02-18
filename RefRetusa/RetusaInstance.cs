using NiTiS.Collections;
using NiTiS.Collections.Generic;
using RefRetusa.Functions;
using RefRetusa.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using YamlDotNet.Core;
using YamlDotNet.Core.Tokens;
using YamlDotNet.RepresentationModel;

namespace RefRetusa;

public class RetusaInstance : IDisposable
{
	private static Random rand = new();
	protected SmartDictonary<string, Function> functions;
	public readonly ushort HashCode;
	public Stack<FileInfo> Files { get; protected set; }
	public FileInfo CurrentFile { get; protected set; }
	public string CurrentFileShort
	{
		get
		{
			string? directoryName = CurrentDirectory?.FullName;
			string res = CurrentFile.FullName.Substring(directoryName?.Length + 1 ?? 0);

			return res;
		}
	}
	public DirectoryInfo? CurrentDirectory { get; protected set; }
	public IReadOnlyDictionary<string, Function> Functions => functions;
	public RetusaInstance()
	{
		HashCode = (ushort)rand.Next(0x0000, 0xFFFF);

		Files = new(16);
		functions = new(func => func.Name, 16)
		{
			IncludeFunction.Instance,
		};
	}

	public void Analys(string pathToFile, CallerPath? caller = null)
	{
		FileInfo file;
		if (FileExists(pathToFile))
		{
			file = new(Combine(pathToFile));
		}
		else if (DirectoryExists(pathToFile))
		{
			IEnumerable<string> fitsFiles = from string path in Directory.GetFiles(pathToFile)
											where Path.GetExtension(path) is ".ret" or ".imp" or ".prj" or ".proj"
											select path;
			
			if (fitsFiles.Count() == 1)
			{
				file = new(Combine(fitsFiles.FirstOrDefault()!));
			}
			else
			{
				Logger.Error($"{caller?.ToString()}Files in directory {pathToFile} do not match the filter, the required file could not be found");
				return;
			}
		}
		else
		{
			Logger.Error($"{caller?.ToString()}File or directory {pathToFile} does not exists");
			return;
		}

		Files.Push(file);
		CurrentFile = file;
		CurrentDirectory = file.Directory;

		using Stream fileStream = file.OpenRead();
		using TextReader fileReader = new StreamReader(fileStream);

		Logger.Debug($"RefRetusa:Analys {pathToFile}");

		YamlStream ystream = new();
		ystream.Load(fileReader);

		YamlMappingNode mapping =
				(YamlMappingNode)ystream.Documents[0].RootNode;

		foreach (KeyValuePair<YamlNode, YamlNode> entry in mapping.Children)
		{
			string? key = ((YamlScalarNode)entry.Key).Value;
			YamlScalarNode? value = entry.Value as YamlScalarNode;
			YamlMappingNode? valueMap = entry.Value as YamlMappingNode;
			YamlSequenceNode? sequence = entry.Value as YamlSequenceNode;

			if (key?.StartsWith('$') ?? false)
			{
				key = key.Substring(1);
				Function? func = functions.GetValueOrDefault(key);


				if (func is null)
					throw new FunctionNotFoundException(key);

				func.Execute(this, sequence, valueMap, value);
			}
		}
	}
	protected string Combine(string path)
	{
		if (CurrentDirectory is null)
			return Path.Combine(Environment.CurrentDirectory, path);
		else
			return Path.Combine(CurrentDirectory.FullName, path);
	}
	protected bool FileExists(string filePath)
	{
		if (CurrentDirectory is null)
			return File.Exists(filePath);
		else
			return CurrentDirectory.GetFiles().Any(file => file.Name == filePath);
	}
	protected bool DirectoryExists(string directoryPath)
	{
		if (CurrentDirectory is null)
			return Directory.Exists(directoryPath);
		else
			return CurrentDirectory.GetDirectories(directoryPath).Any(dir =>
			{
				Logger.Info(dir.FullName);
				return dir.Name == directoryPath;
			});
	}
	public void Dispose()
	{
		Logger.Debug($"Retusa [{Convert.ToString(HashCode, 16).PadLeft(4, '0')}] disposed");
	}
}