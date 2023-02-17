using RefRetusa.Logging;
using System;
using System.Collections.Generic;

namespace RefRetusa.Tasks;

public class Properties
{
	private readonly Dictionary<string, object> values;
	public Properties()
	{
		values = new();
	}
	public void Add<T>(string key, T value)
		where T : notnull
	{
		values.Remove(key);
		values.Add(key, value);
	}
	public void Add<T>(string key, string value, int ln, int col)
		where T : notnull, IParsable<T>
	{
		if (T.TryParse(value, null, out T? result))
		{
			values.Remove(key);
			values.Add(key, value);
		}
		else
		{
			Logger.Error($"Unable to parse \"{key}\" value {(ln != -1 ? $"at {{{col}:{ln}}}" : string.Empty)}");
		}
	}
}