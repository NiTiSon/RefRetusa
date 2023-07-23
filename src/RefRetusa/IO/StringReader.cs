using System.Diagnostics;

namespace RefRetusa.IO;

public class StringReader : ReadStream<char>, Seekable
{
	private readonly string content;
	private int index;

	public int Length => content.Length;
	public int Position => index;

	public StringReader(string content)
	{
		this.content = content;
	}

	public char Peek()
	{
		ThrowIfOutOfBounds();
		return this.content[index];
	}

	public char Read()
	{
		ThrowIfOutOfBounds();
		return this.content[index++];
	}

	[StackTraceHidden]
	private void ThrowIfOutOfBounds()
	{
		if (index < 0 || index >= this.content.Length)
			throw new IndexOutOfRangeException();
	}

	public void Seek(SeekOrigin origin, long offset)
	{
		switch (origin)
		{
			case SeekOrigin.Begin:
				index = (int)offset;
				break;
			case SeekOrigin.Current:
				index += (int)offset;
				break;
			case SeekOrigin.End:
				index = Length - (int)offset - 1; // length more then final index by one
				break;
		}
		ThrowIfOutOfBounds();
	}
}
