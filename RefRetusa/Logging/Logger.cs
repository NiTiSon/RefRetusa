using NiTiS.Core;
using System;
using System.Diagnostics;
using System.Text;

namespace RefRetusa.Logging;

public static class Logger
{
	private static object sync = new();
	private static ConsoleColor defaultColour = Console.ForegroundColor;
	public static void Debug(string message)
		=> Log(message, Verbose.Debug);
	public static void Warn(string message)
		=> Log(message, Verbose.Warning);
	public static void Error(string message)
		=> Log(message, Verbose.Error);
	public static void Info(string message)
		=> Log(message, Verbose.Info);
	public static void Log(string message, Verbose verbose)
	{
		lock (sync)
		{
			if (verbose < RetusaArguments.Verbose)
				return;

			Console.ForegroundColor = verbose switch
			{
				Verbose.Error => ConsoleColor.Red,
				Verbose.Warning => ConsoleColor.Yellow,
				Verbose.Debug => ConsoleColor.DarkGray,
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

	public static void Exception(Exception? exception)
	{
		if (exception is null)
		{
			Log("Unknown exception...", Verbose.Error);
			Log(new StackTrace(1, true).ToString(), Verbose.Error);

			Environment.Exit(int.MinValue);
		}

		string msg = exception.Message;

		StringBuilder sb = new();

		sb.Append("Exception: ");
		sb.Append(exception.GetType());
		sb.AppendLine();
		sb.Append("Message: ");
		sb.AppendLine(msg);

		sb.AppendLine("Trace: ");
		sb.Append(exception.StackTrace ?? new StackTrace(1, true).ToString());

		Log(sb.ToString(), Verbose.Error);
		Environment.Exit(exception.HResult);
	}
}