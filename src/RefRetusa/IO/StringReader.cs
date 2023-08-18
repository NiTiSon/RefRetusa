using System;
using System.Diagnostics;

namespace RefRetusa.IO;

public class StringReader : ReadStream<char>, Seekable
{
	private readonly string content;
	private int position;

	public int Length => content.Length;
	public int Position => position;

	public StringReader(string content)
	{
		this.content = content;
	}

	public char Peek()
	{
		ThrowIfOutOfBounds();
		return content[position];
	}

	public char Read()
	{
		ThrowIfOutOfBounds();
		return content[position++];
	}

	[StackTraceHidden]
	private void ThrowIfOutOfBounds()
	{
		if (position < 0 || position >= content.Length)
			throw new IndexOutOfRangeException();
	}

	public void Seek(SeekOrigin origin, long offset)
	{
		switch (origin)
		{
			case SeekOrigin.Begin:
				position = (int)offset;
				break;
			case SeekOrigin.Current:
				position += (int)offset;
				break;
			case SeekOrigin.End:
				position = Length - (int)offset - 1; // length more then final index by one
				break;
		}
		ThrowIfOutOfBounds();
	}
}
