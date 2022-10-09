using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace MartianRobots.SharedKernel;

public static class Guards
{
    public static Guid ThrowIfGuidEmpty(Guid argument, [CallerArgumentExpression("argument")] string? paramName = null)
    {
        if (argument == Guid.Empty)
        {
            ThrowOutOfRange(paramName);
        }

        return argument;
    }

    public static T ThrowIfNull<T>([NotNull] T? argument, [CallerArgumentExpression("argument")] string? paramName = null)
    {
        if (argument is null)
        {
            Throw(paramName);
        }

        return argument;
    }

    public static string ThrowIfNullOrEmpty([NotNull] string? argument, [CallerArgumentExpression("argument")] string? paramName = null)
    {
        if (string.IsNullOrEmpty(argument))
        {
            Throw(paramName);
        }

        return argument;
    }

    [DoesNotReturn]
    private static void ThrowOutOfRange(string? paramName)
    {
        throw new ArgumentOutOfRangeException(paramName);
    }

    [DoesNotReturn]
    private static void Throw(string? paramName)
    {
        throw new ArgumentNullException(paramName);
    }
}