using System;
using YamlDotNet.RepresentationModel;

namespace RefRetusa;

internal static class EntryPoint
{
	public static void Main(string[] args)
	{
		RetusaArguments.Parse(args);

		Console.WriteLine(RetusaArguments.Verbose);

		using RetusaInstance retusa = new();

		retusa.Analys(RetusaArguments.EntryFileOrDirectoryPath);
	}
}
