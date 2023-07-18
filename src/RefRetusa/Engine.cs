using NiTiS.Collections.Generic;

namespace RefRetusa;

public sealed class Engine
{
	private readonly Dictionary<string, Command> commands;
	private readonly SmartDictonary<string, Extension> extensions;
	private readonly SmartDictonary<string, Language> languages;
	private readonly Dictionary<string, string> options;
	[Obsolete("Not ready yet.")]
	private readonly object? projectTemplates;
	private bool initialized;

	public IEnumerable<Command> Commands
		=> commands.Values;
	public IEnumerable<string> CommandNames
		=> commands.Keys;

	public Engine()
	{
		commands = new();
		languages = new(Language.KeyGet);
		extensions = new(Extension.KeyGet);
		options = new(32);
	}

	public void Initialize()
	{
		lock (extensions)
		{
			// TOOD: Add exception handling & logging
			foreach ((_, Extension extension) in extensions)
				extension.Initialize(this);
		}
		initialized = true;
	}

	public void InitializeExtension(Extension ext)
	{
		lock (extensions)
		{
			if (initialized)
			{
				ext.Initialize(this);
			}

			extensions.Add(ext);
		}
	}

	public void RegistryLanguage(Language lang)
	{
		lock (languages)
		{
			languages.Add(lang);
		}
	}

	public void AddCommand(string commandName, Command command)
	{
		lock (commands)
		{
			if (commands.TryGetValue(commandName, out Command? linkedCommand))
			{
				Console.WriteLine($"RegistryError: Command alias {commandName} already link to {linkedCommand}");
			}
			else
			{
				commands.Add(commandName, command);
			}
		}
	}

	public Command? GetCommand(string commandName)
	{
		lock (commands)
		{
			if (commands.TryGetValue(commandName, out Command? cmd))
			{
				return cmd;
			}
			else
			{
				return null;
			}
		}
	}
}