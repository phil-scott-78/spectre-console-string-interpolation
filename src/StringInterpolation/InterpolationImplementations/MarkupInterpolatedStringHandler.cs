namespace StringInterpolation.InterpolationImplementations;

/// <summary>
/// Helper for building markup text via a string builder.
/// </summary>
[InterpolatedStringHandler]
public readonly ref struct MarkupInterpolatedStringHandler
{
    private readonly StringBuilder _markupStringBuilder;

    public MarkupInterpolatedStringHandler(int literalLength, int formattedCount)
    {
        _markupStringBuilder = new StringBuilder(literalLength + formattedCount * 11);
    }

    public void AppendLiteral(string s) => _markupStringBuilder.Append(s);

    public void AppendFormatted(string s) => _markupStringBuilder.Append(s.EscapeMarkup());

    public void AppendFormatted<T>(T value)
    {
        _markupStringBuilder.Append(value);
    }

    public string ToStringAndClear()
    {
        return _markupStringBuilder.ToString();
    }
}
