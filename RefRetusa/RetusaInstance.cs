using RefRetusa.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Core.Tokens;
using YamlDotNet.RepresentationModel;

namespace RefRetusa;

public class RetusaInstance : IDisposable
{
	private static Random rand = new();
	public readonly ushort HashCode;
	public Stack<FileInfo> Files { get; protected set; }
	public FileInfo CurrentFile { get; protected set; }
	public DirectoryInfo CurrentDirectory { get; protected set; }
	public RetusaInstance()
	{
		HashCode = (ushort)rand.Next(0x0000, 0xFFFF);
		Files = new(16);
	}

	public void Analys(string pathToFile)
	{
		FileInfo file = new(pathToFile);

		if (!file.Exists)
			Logger.Exception(new FileNotFoundException("Project file not found", file.FullName));

		Files.Push(file);
		CurrentFile = file;

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
			string? value = (entry.Value as YamlScalarNode)?.Value;

			if (key?.StartsWith('$') ?? false)
			{
				AnalysFunction(key.Substring(1));
			}
		}

		static void AnalysFunction(string funcName)
		{
		}
	}

	public void Dispose()
	{
		Logger.Debug($"Retusa [{Convert.ToString(HashCode, 16).PadLeft(4, '0')}] disposed");
	}
}