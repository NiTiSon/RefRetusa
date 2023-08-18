namespace RefRetusa.Logging;

public interface Logger
{
    void Log(string message, LogLevel level);
    void Warning(string message)
        => Log(message, LogLevel.Warning);
    void Error(string message)
        => Log(message, LogLevel.Error);
    void Information(string message)
        => Log(message, LogLevel.Information);
    void Details(string message)
        => Log(message, LogLevel.Details);
    void Critical(string message)
        => Log(message, LogLevel.Critical);
    void Debug(string message)
        => Log(message, LogLevel.Debug);
}