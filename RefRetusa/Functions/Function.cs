namespace RefRetusa.Functions;

public abstract class Function
{
	public readonly string Name;

	public Function(string name)
	{
		Name = name;
	}
	public abstract void Execute(RetusaInstance executor, __arglist);
}
