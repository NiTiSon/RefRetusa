namespace RefRetusa;

public class Language
{
	public readonly string Id;
	public readonly string Name;

	public Language(string id, string name)
	{
		Id = id;
		Name = name;
	}

	internal static string KeyGet(Language lng) => lng.Id;
}