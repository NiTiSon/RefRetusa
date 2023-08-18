namespace RefRetusa;

public enum TaskResult
{
	Fail,
	Success,
}

public readonly struct TaskResult<Result>
{
	private readonly Result? value;
	public Result? Value
	{
		get
		{
			if (IsSuccess)
				return value;

			return default;
		}
	}
	public bool IsSuccess { get; }

	private TaskResult(Result result, bool isSuccess)
	{
		value = result;
		IsSuccess = isSuccess;
	}

	public TaskResult<Result> Success(Result result)
	{
		return new(result, true);
	}

	public TaskResult<Result> Fail()
	{
		return new(default!, false);
	}
}