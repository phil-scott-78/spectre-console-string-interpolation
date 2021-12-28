namespace StringInterpolation.InterpolationImplementations;

public static class EscapeHelpers
{
    public static string FormattableEscape(FormattableString formattableString)
    {
        var args = formattableString
            .GetArguments()
            .Select(i => i is string s ? s.EscapeMarkup() : i)
            .ToArray();

        return string.Format(formattableString.Format, args);
    }

    public static string WrappedMarkupInterpolatedEscape(ref WrappedMarkupInterpolatedStringHandler handler)
    {
        return handler.ToStringAndClear();
    }

    public static string MarkupInterpolatedEscape(ref MarkupInterpolatedStringHandler handler)
    {
        return handler.ToStringAndClear();
    }

    public static StringBuilder AppendEscapeMarkup(
        this StringBuilder stringBuilder,
        [InterpolatedStringHandlerArgument("stringBuilder")]
        ref WrappedAppendStringHandler handler)
    {
        return stringBuilder;
    }
}
