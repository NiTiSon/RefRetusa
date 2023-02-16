namespace RefRetusa.Tasks;

public abstract class TaskUnit
{
	public string Name { get; protected set; } = string.Empty;
	public string Description { get; protected set; } = string.Empty;

	public TaskUnit()
	{

	}
	public abstract void RunTask(RetusaInstance instance, TaskType type);
}