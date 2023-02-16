using RefRetusa.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.RepresentationModel;

namespace RefRetusa;

public class RetusaInstance : IDisposable
{
	public readonly ushort HashCode;
	public Stack<FileInfo> Files { get; protected set; }
	public FileInfo CurrentFile { get; protected set; }
	public DirectoryInfo CurrentDirectory { get; protected set; }
	public RetusaInstance()
	{
		HashCode = (ushort)new Random().Next(0x0000, 0xFFFF);
		Files = new(16);
	}

	public void Analys(string pathToFile)
	{
		FileInfo file = new(pathToFile);

		if (!file.Exists)
			Logger.Exception(new FileNotFoundException("Project file not found", pathToFile));

		Files.Push(file);
		CurrentFile = file;

		using Stream fileStream = file.OpenRead();
		using TextReader fileReader = new StreamReader(fileStream);

		Logger.None($"RefRetusa:Analys {pathToFile}");

		YamlStream ystream = new();
		ystream.Load(fileReader);

		YamlMappingNode mapping =
				(YamlMappingNode)ystream.Documents[0].RootNode;

		foreach (KeyValuePair<YamlNode, YamlNode> entry in mapping.Children)
		{
			Console.WriteLine(((YamlScalarNode)entry.Key).Value + ": " + ((YamlScalarNode)entry.Value).Value);
		}
	}

	public void Dispose()
	{
		Logger.None($"Retusa [{Convert.ToString(HashCode, 16).PadLeft(4, '0')}] disposed");
	}
}