namespace RefRetusa;

public readonly struct ArgumentName
{
	private readonly string value__;

	private ArgumentName(string value)
	{
		value__ = value;
	}

	public static implicit operator string(ArgumentName value)
		=> value.value__;

	public static implicit operator ArgumentName(string value)
		=> new(value);
}