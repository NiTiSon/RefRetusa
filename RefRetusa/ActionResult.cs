namespace RefRetusa;

public readonly struct ActionResult<Result, Fault>
{
	public readonly Result Return;
	public readonly Fault Error;
	public readonly bool Success;

	private ActionResult(Result s, Fault e, bool isSuccess)
	{
		this.Error = e;
		this.Return = s;
		this.Success = isSuccess;
	}

	public static ActionResult<Result, Fault> FromError(Fault fault)
		=> new(default!, fault, false);

	public static ActionResult<Result, Fault> FromResult(Result success)
		=> new(success, default!, true);

	public static implicit operator ActionResult<Result, Fault>(Result result)
		=> FromResult(result);
}