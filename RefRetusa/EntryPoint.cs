using RefRetusa.Logging;
using System;
using YamlDotNet.RepresentationModel;

namespace RefRetusa;

internal static class EntryPoint
{
	public static void Main(string[] args)
	{
		try
		{
			RetusaArguments.Parse(args);

			Console.WriteLine(RetusaArguments.Verbose);

			using RetusaInstance retusa = new();

			retusa.Analys(RetusaArguments.EntryFileOrDirectoryPath);

		}
		catch (Exception exception)
		{
			Logger.Exception(exception);
		}
		finally
		{

		}
	}
}
