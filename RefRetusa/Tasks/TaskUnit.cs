﻿using System.IO;

namespace RefRetusa.Tasks;

public abstract class TaskUnit
{
	protected readonly RetusaInstance runner;
	public readonly Location Location;
	public string Name { get; protected set; }
	public string Description { get; protected set; }
	public FileInfo? RelatedFile { get; protected set; }
	public Properties Properties { get; protected set; }
	public TaskUnit(RetusaInstance runner, Location location)
	{
		this.runner = runner;
		Location = location;
		Name = string.Empty;
		Description = string.Empty;
		Properties = new();
	}
	public abstract void RunTask(TaskType type);
}