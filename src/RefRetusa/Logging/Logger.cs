namespace RefRetusa.Logging;

public interface Logger
{
	void Log(string message, LogLevel verbose = LogLevel.Debug);
	void Log(Exception exception, LogLevel verbose = LogLevel.Error);
}