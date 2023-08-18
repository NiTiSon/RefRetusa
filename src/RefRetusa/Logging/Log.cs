namespace RefRetusa.Logging;

public static class Log
{
	public static Logger CreateLogger(string serviceName)
	{
		Guard.AgainstNull(serviceName);

		return new ConsoleLogger(serviceName);
	}
}