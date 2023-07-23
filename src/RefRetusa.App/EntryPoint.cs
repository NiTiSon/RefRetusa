using System;
using RefRetusa.IO;
using RefRetusa.NiteCode;
using YamlDotNet.Serialization;

namespace RefRetusa;

public static class EntryPoint
{
	private static readonly ISerializer serializer = new SerializerBuilder()
		.Build();

	private static void Main(string[] args)
	{
		Engine engine = new();
		engine.InitializeExtension(new NiteCodeExtension());
		engine.Initialize();


		if (args.Length is 0)
		{
			Console.WriteLine($"""
&Retusa aka RefRetusa - a program to compile, create, clean your projects
Version: {typeof(EntryPoint).Assembly.GetName().Version?.ToString(3) ?? "unknown"}
""");
		}


		
		//	commands:
		//	ext | extension
		//		list (show all extensions applyed to &Retusa Engine)
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