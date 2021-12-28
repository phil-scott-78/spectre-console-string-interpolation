using StringInterpolation.InterpolationImplementations;

namespace StringInterpolation;

internal static class AnsiConsoleExtensions
{
    public static void Markup(this IAnsiConsole console, ref MarkupInterpolatedStringHandler handler)
        => console.Markup(handler.ToStringAndClear());

    public static void MarkupLine(this IAnsiConsole console, ref MarkupInterpolatedStringHandler handler)
        => console.MarkupLine(handler.ToStringAndClear());
}
