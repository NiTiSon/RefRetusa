using NiTiS.Core;

namespace RefRetusa.Logging;

public static class Logger
{
	private static object sync = new();
	private static ConsoleColor defaultColour = Console.ForegroundColor;
	public static void Log(string message, Verbose verbose)
	{
		lock (sync)
		{
			Console.ForegroundColor = verbose switch
			{
				Verbose.Error => ConsoleColor.Red,
				Verbose.Warning => ConsoleColor.Yellow,
				_ => defaultColour,
			};
			Console.WriteLine(message);
			Console.ForegroundColor = defaultColour;
		}
	}
	public static void LogWithPadding(string message, int padding, Verbose verbose)
	{
		Span<string> msgs = message.Split("\n", StringSplitOptions.None);

		string paddingStr = " ".Multiply(padding);
		for (int i = 0; i < msgs.Length; i++)
		{
			string msg = msgs[i];

			Log(paddingStr + msg, verbose);
		}
	}
}