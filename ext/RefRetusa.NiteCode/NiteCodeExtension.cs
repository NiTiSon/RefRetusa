namespace RefRetusa.NiteCode;

public class NiteCodeExtension : Extension
{
	public NiteCodeExtension()
		: base(NiteCode.ExtensionId, NiteCode.ExtensionName)
	{
	}

	public override void Initialize(Engine engine)
	{
		// engine.RegistryLanguage();
		// engine.RegisterProjectType("console", new NiteCodeConsoleProject);
		// engine.RegisterProjectType("library", new NiteCodeLibraryProject);
	}
}
