namespace RefRetusa.IO;

public interface WriteStream<in Data> : Stream
{
	public void Write(Data data);
}