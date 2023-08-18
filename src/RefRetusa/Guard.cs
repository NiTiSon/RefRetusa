using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace RefRetusa;

internal static class Guard
{
	public static T AgainstNull<T>(
		[NotNull] T? argument,
		[CallerArgumentExpression("argument")] string? paramName = null
		) where T : class
		=> argument ?? throw new ArgumentNullException(paramName);

}