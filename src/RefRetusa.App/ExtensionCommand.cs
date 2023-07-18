using System.Collections.Generic;

namespace RefRetusa;

public class ExtensionCommand : Command
{
	public override string Description => throw new System.NotImplementedException();

	public override void Execute(IReadOnlyDictionary<ArgumentName, object> arguments)
	{
		throw null!;

		// Command cmd = CommandBuilder
		//	.Argument()
		//	.As("extension")
		//	.OptionalArgument(new string[] { "ext" }, new string[] { "output" }, ArgumentAction.ReadValue, ArgumentValueType.String)
		//	.SubCommand("list", null)
		//	.Build();
	}
}
