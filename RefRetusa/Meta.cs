using System;
using System.IO;
using System.Reflection;

namespace RefRetusa;

public static class Meta
{
	private const string EnvRefRetusaDirectory = "REF_RETUSA_DIRECTORY";
	public static Version Version;
	public static string TempDirectory;

	static Meta()
	{
		Assembly asm = typeof(Meta).Assembly;
		AssemblyName name = asm.GetName();

		Version = name.Version ?? new(0, 0, 0, int.MaxValue);

		TempDirectory = Environment.GetEnvironmentVariable(EnvRefRetusaDirectory) ?? Path.Combine(Path.GetDirectoryName(Environment.ProcessPath) ?? Environment.CurrentDirectory, "refretusa" + Path.DirectorySeparatorChar);
	}

	public static readonly string ExecutionExtension = Environment.OSVersion.Platform == PlatformID.Win32NT ? ".exe" : "";
}
