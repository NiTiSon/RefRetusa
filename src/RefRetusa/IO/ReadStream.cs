namespace RefRetusa.IO;

public interface ReadStream<out Data> : Stream
{
	public Data Read();
	public Data Peek();
}
