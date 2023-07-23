namespace RefRetusa.IO;

public interface WriteStream<in Data> : Stream
{
	public void Write(Data data);

	public static WriteStream<T> CreateFromArray<T>()
	{


		throw null!;
	}
}