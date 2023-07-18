namespace RefRetusa;

public abstract class Project
{
	public string Path { get; }
}

/// <summary>
/// ConsoleApplication, Exe, e.t.c.
/// </summary>
public abstract class ProjectKind : IEquatable<ProjectKind>
{
	/// <summary>
	/// Kind identifier.
	/// </summary>
	public readonly string Id;
	public readonly string Name;

	public ProjectKind(string id, string name)
	{
		if (id.Contains(' '))
			throw new ArgumentException(null, nameof(id));

		Id = id;
		Name = name;
	}

	public bool Equals(ProjectKind? other)
		=> other?.Id == Id;

	public override bool Equals(object? obj)
		=> obj is ProjectKind projKind && Equals(projKind);

	public override int GetHashCode()
		=> Id.GetHashCode();
}