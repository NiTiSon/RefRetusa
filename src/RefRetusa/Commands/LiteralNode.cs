namespace RefRetusa.Commands;

public abstract class LiteralNode
{
	public string Value { get; private set; }

	protected LiteralNode(string value)
	{
		Value = value;
	}
}