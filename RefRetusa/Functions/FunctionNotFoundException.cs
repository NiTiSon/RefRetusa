using System;


namespace RefRetusa.Functions;

public class FunctionNotFoundException : Exception
{
	public FunctionNotFoundException(string functionName) : base($"Function ${functionName} not found") { }
}
