namespace RefRetusa.Logging;

public static class Logger
{
	public static void Log(string message, Verbose verbose)
	{

	}
}
public enum Verbose
{
	Debug = 0,
	Info = 1,
	Error = 2,
	None = 3,
}