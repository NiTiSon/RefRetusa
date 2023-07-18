using System.ComponentModel;

namespace RefRetusa;

public enum ArgumentValueType
{
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	Undefined,
	Int,
	UInt,
	String,
}