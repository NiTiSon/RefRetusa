using System;
using System.Collections.Generic;

namespace RefRetusa.Structs.Collections;

public sealed class ConfigurationTable
{
	private readonly Dictionary<string, string> table = new(24);
	public string Configuration
	{
		get => Get("configuration");
		set => Set("configuration", value);
	}
	public string? this[string key]
	{
		get => Get(key);
		set => Set(key, value);
	}

	public string? Get(string key)
	{
		if (table.ContainsKey(key))
		{
			return table[key];
		}

		return Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.Process);
	}
	public void Set(string key, string? value)
	{
		if (Environment.GetEnvironmentVariable(key, EnvironmentVariableTarget.Process) is null)
		{
			if (value is null)
				table.Remove(key);
			else
				table[key] = value;
		}
		else
			Environment.SetEnvironmentVariable(key, value, EnvironmentVariableTarget.Process);
	}
}
