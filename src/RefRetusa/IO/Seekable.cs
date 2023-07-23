namespace RefRetusa.IO;

public interface Seekable
{
	public void Seek(SeekOrigin origin, long offset);
}