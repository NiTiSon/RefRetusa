using System.IO;

namespace RefRetusa.Tasks;

public class Location
{
	public required FileInfo File { get; init; }
	public required DirectoryInfo Directory { get; init; }
}