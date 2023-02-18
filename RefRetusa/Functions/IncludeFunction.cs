using System.Collections.Generic;
using System.IO;
using YamlDotNet.Core;
using YamlDotNet.RepresentationModel;

namespace RefRetusa.Functions;

public sealed class IncludeFunction : Function
{
	public static readonly Function Instance = new IncludeFunction();
	private IncludeFunction() : base("include")
	{
	}

	public override void Execute(RetusaInstance executor, YamlSequenceNode? args, YamlMappingNode? kargs, YamlScalarNode? inline)
	{
		List<(string, Mark)> path = new(args?.Children?.Count ?? kargs?.Children?.Count ?? 1);

		if (inline is not null)
		{
			path[0] = (inline.Value ?? string.Empty, inline.Start);
		}
		else if (kargs is not null)
		{
			foreach (KeyValuePair<YamlNode, YamlNode> arg in kargs!)
			{
				arg.Deconstruct(out YamlNode key, out YamlNode value);

				if (key.ToString() == "path")
				{
					path.Add((value.ToString(), key.Start));
				}
			}
		}
		else
		{
			foreach (YamlNode arg in args!)
			{
				path.Add((arg.ToString(), arg.Start));
			}
		}

		foreach ((string Path, Mark Position) filePath in path)
		{
			executor.Analys(filePath.Path, new(filePath.Position, executor.CurrentFileShort));
		}
	}
}
