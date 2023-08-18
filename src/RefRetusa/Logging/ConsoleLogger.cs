using System;

namespace RefRetusa.Logging;

// TODO?: Replace with ConsoleColorful with multicolour support
internal class ConsoleLogger : Logger
{
	internal static readonly object consoleSync = new();

	private readonly string serviceName;

	public ConsoleLogger(string serviceName)
	{
		this.serviceName = serviceName;
	}

	public void Log(string message, LogLevel level)
	{
		DateTime time = DateTime.Now;
		lock (consoleSync)
		{
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.Write($"{time:HH:mm:ss} ");
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.Write('[');
			(ConsoleColor color, string threeLetterLevel) = GetLevelDisplay(level);
			Console.ForegroundColor = color;
			Console.Write(threeLetterLevel);
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.Write('/');
			Console.Write(serviceName);
			Console.Write("] ");

			Console.Write(message);
			Console.Write('\n');
		}
	}

	private static (ConsoleColor, string) GetLevelDisplay(LogLevel level)
	{
		return level switch
		{
			LogLevel.Information => (ConsoleColor.Blue, "INF"),
			LogLevel.Details => (ConsoleColor.DarkGray, "DLS"),
			LogLevel.Warning => (ConsoleColor.Yellow, "WRN"),
			LogLevel.Debug => (ConsoleColor.Gray, "DBG"),
			LogLevel.Error => (ConsoleColor.Red, "ERR"),
			LogLevel.Critical => (ConsoleColor.Red, "CRT"),
			_ => throw new ArgumentOutOfRangeException(nameof(level)),
		};
	}
}