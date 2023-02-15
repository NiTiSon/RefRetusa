using RefRetusa.Logging;
using System;

namespace RefRetusa;

public class RetusaArgument<T>
{
	public T Value { get; set; }
	private Func<string, ActionResult<T, string>> parser;
	public RetusaArgument(Func<string, ActionResult<T, string>> parser)
	{
		this.parser = parser;
		Value = default!;
	}
	public bool Parse(string input)
	{
		ActionResult<T, string> result = parser(input);

		Value = result.Return;
		
		if (result.Success)
			return true;

		Logger.LogWithPadding(result.Error, 2, Verbose.Error);

		return false;
	}

	public override string ToString()
		=> $"Argument<{typeof(T).Name}> = {Value}";

	public static implicit operator T(RetusaArgument<T> argument)
		=> argument.Value;
}
