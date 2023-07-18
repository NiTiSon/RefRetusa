namespace RefRetusa;

public class Extension : IDisposable
{
	public readonly string Id;
	public readonly string Name;

	public Extension(string id, string name)
	{
		Id = id;
		Name = name;
	}

	public virtual void Initialize(Engine engine) { }
	public virtual void Dispose() { }

	internal static string KeyGet(Extension ext) => ext.Id;
}