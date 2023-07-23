namespace RefRetusa.Commands;

public sealed class ArgumentNode : LiteralNode
{
	public string Name => Value.TrimStart('-');
	public int Level
	{
		get
		{
			int count = 0;
			for (int i = 0; i < Value.Length; i++)
			{
				if (Value[i] == '-')
					count++;
				else
					break;
			}
			return count;
		}
	}

	public ArgumentNode(string value) : base(value)
	{
	}
}