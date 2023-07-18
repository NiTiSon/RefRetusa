namespace RefRetusa;

public abstract class Command
{
	private readonly Dictionary<string, Command> subcommands;
	public abstract string Description { get; }
	public IEnumerable<string> SubCommandsAlias
		=> subcommands.Keys;

	public IEnumerable<Command> SubCommands
		=> subcommands.Values;


	public Command()
	{
		subcommands = new();
	}

	public Command? GetSubCommand(string name)
	{
		subcommands.TryGetValue(name, out Command? subcommand);

		return subcommand;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="arguments"></param>
	public abstract void Execute(IReadOnlyDictionary<ArgumentName, object> arguments);

	public class Builder
	{
		private string description;
		private Action<IReadOnlyDictionary<ArgumentName, object>>? action;
		private readonly Dictionary<ArgumentName, (string name, int argAction, int argKind)> arguments;
		public Builder()
		{
			arguments = new();
			description = string.Empty;
		}

		public Builder Exec(Action<IReadOnlyDictionary<ArgumentName, object>> action)
		{
			this.action = action;
			return this;
		}


		public Command Build()
		{
			return new DelegateCommand(description, action ?? static delegate (IReadOnlyDictionary<ArgumentName, object> action) { });
		}

		private class DelegateCommand : Command
		{
			public readonly Action<IReadOnlyDictionary<ArgumentName, object>> exec;

			public override string Description { get; }

			public DelegateCommand(string desc, Action<IReadOnlyDictionary<ArgumentName, object>> exec)
			{
				Description = desc;
				this.exec = exec;
			}

			public override void Execute(IReadOnlyDictionary<ArgumentName, object> arguments)
			{
				exec(arguments);
			}
		}
	}
}