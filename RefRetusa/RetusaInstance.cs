using RefRetusa.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.RepresentationModel;

namespace RefRetusa;

public class RetusaInstance : IDisposable
{
	private ushort hashCode;
	public RetusaInstance()
	{
		hashCode = (ushort)new Random().Next(0x0000, 0xFFFF);
	}

	public void Analys(string pathToFile)
	{
		if (!File.Exists(pathToFile))
			Logger.Exception(new FileNotFoundException("Project file not found", pathToFile));

		using Stream file = new FileStream(RetusaArguments.EntryFileOrDirectoryPath, FileMode.Open);
		using TextReader tr = new StreamReader(file);

		YamlStream ystream = new();
		ystream.Load(tr);

		YamlMappingNode mapping =
				(YamlMappingNode)ystream.Documents[0].RootNode;

		foreach (KeyValuePair<YamlNode, YamlNode> entry in mapping.Children)
		{
			Console.WriteLine(((YamlScalarNode)entry.Key).Value);
		}
	}

	public void Dispose()
	{
		Logger.None($"Retusa [{Convert.ToString(hashCode, 16).PadLeft(4, '0')}] disposed");
	}
}