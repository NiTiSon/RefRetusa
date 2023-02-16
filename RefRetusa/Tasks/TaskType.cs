using System;

namespace RefRetusa.Tasks;

[Flags]
public enum TaskType
{
	/// <summary>
	/// None
	/// </summary>
	None = 0,
	/// <summary>
	/// Compile Task
	/// </summary>
	Compile = 1,
	/// <summary>
	/// Run task
	/// </summary>
	Run = 2,
	/// <summary>
	/// Analyze
	/// </summary>
	Analys = 4,
	/// <summary>
	/// Generate docs
	/// </summary>
	Document = 8,
}