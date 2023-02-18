using YamlDotNet.Core;

namespace RefRetusa;
public readonly struct CallerPath
{
	public readonly Mark? Position;
	public readonly string? ShortFilePath;
	public CallerPath(Mark? mark, string? shortFilePath)
	{
		Position = mark;
		ShortFilePath= shortFilePath;
	}
	public override string ToString()
	{
		return $"[{ShortFilePath} at {Position?.Line}:{Position?.Column}] ";
	}
}