using System.Diagnostics;
using System;

namespace RefRetusa.IO;

public class ArrayStream<T> : ReadStream<T>, WriteStream<T>, Seekable
{
	private readonly T[] array;
	private int position;

	public int Length => array.Length;
	public int Position => position;

	public ArrayStream(T?[] array)
	{
		ArgumentNullException.ThrowIfNull(array, nameof(array));
		this.array = array!;
	}

	public ArrayStream(int size)
	{
		if (size < 0)
			throw new ArgumentOutOfRangeException(nameof(size));

		array = size is 0 ? Array.Empty<T>() : new T[size];
	}

	public void Write(T data)
	{
		ThrowIfOutOfBounds();
		array[position++] = data;
	}

	public T Read()
	{
		ThrowIfOutOfBounds();
		return array[position++];
	}

	public T Peek()
	{
		ThrowIfOutOfBounds();
		return array[position];
	}

	[StackTraceHidden]
	private void ThrowIfOutOfBounds()
	{
		if (position < 0 || position >= array.Length)
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

	public void CopyTo(Array array, int index)
	{
		throw new NotImplementedException();
	}
}
