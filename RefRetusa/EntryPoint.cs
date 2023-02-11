using YamlDotNet.Core.Events;
using YamlDotNet.RepresentationModel;

namespace RefRetusa;

internal static class EntryPoint
{
	public static void Main(string[] args)
	{
		RetusaArguments.Parse(args);

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
}
