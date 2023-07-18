namespace RefRetusa.NiteCode;

public class NiteCodeExtension : Extension
{
	public NiteCodeExtension()
		: base(NiteCode.ExtensionId, NiteCode.ExtensionName)
	{
	}

	public override void Initialize(Engine engine)
	{
		RegistryCommands(engine);
		// engine.RegistryLanguage();
		// engine.RegisterProjectType("console", new NiteCodeConsoleProject);
		// engine.RegisterProjectType("library", new NiteCodeLibraryProject);
	}
	private static void RegistryCommands(Engine engine)
	{
		engine.AddCommand("nitecode-version", // Just test
			new Command.Builder()
				.Exec((_) =>
				{
					Console.WriteLine("Ananas");
				})
			.Build());
	}
}
