namespace RefRetusa;

public static class RetusaArguments
{

	internal static void Parse(string[] args)
	{
		Span<string> _args = new(args);

		for (int i = 0; i < args.Length; i++)
		{
			ReadOnlySpan<char> arg = _args[i];


		}
	}
}
public static class RetusaInitialMessageBox
{
	public static void Throw(params RetusaArgument[] arguments)
	{

	}
}