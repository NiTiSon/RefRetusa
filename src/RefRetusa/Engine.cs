using RefRetusa.Logging;
using static RefRetusa.TaskResult;

namespace RefRetusa;

public sealed class Engine
{
	private readonly Logger logger;

	public Engine()
	{
		logger = Log.CreateLogger("&Ret");
	}

	public TaskResult Execute(string expression)
	{
		if (string.IsNullOrWhiteSpace(expression))
		{
			logger.Warning("Expression is missed!");
			return Fail;
		}

		return Success;
	}
}