using RefRetusa.Logging;

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
}
