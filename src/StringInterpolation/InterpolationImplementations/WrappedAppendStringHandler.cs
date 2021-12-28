namespace StringInterpolation.InterpolationImplementations;

[InterpolatedStringHandler]
public ref struct WrappedAppendStringHandler
{
    private StringBuilder.AppendInterpolatedStringHandler _innerHandler;

    public WrappedAppendStringHandler(int literalLength, int formattedCount, StringBuilder sb)
    {
        _innerHandler = new StringBuilder.AppendInterpolatedStringHandler(literalLength, formattedCount, sb);
    }

    // delegate directly to inner handler
    public void AppendLiteral(string value) =>
        _innerHandler.AppendLiteral(value);

    public void AppendFormatted<T>(T value) =>
        _innerHandler.AppendFormatted(value);

    public void AppendFormatted<T>(T value, string? format) =>
        _innerHandler.AppendFormatted(value, format);

    public void AppendFormatted<T>(T value, int alignment) =>
        _innerHandler.AppendFormatted(value, alignment);

    public void AppendFormatted<T>(T value, int alignment, string? format) =>
        _innerHandler.AppendFormatted(value, alignment, format);

    public void AppendFormatted(object? value, int alignment = 0, string? format = null) =>
        _innerHandler.AppendFormatted(value, alignment, format);

    // text data that we are going to escape before sending to the inner handler
    public void AppendFormatted(string? value)
    {
        AppendEscaped(value);
    }

    public void AppendFormatted(string? value, int alignment = 0, string? format = null)
    {
        _innerHandler.AppendFormatted(value?.EscapeMarkup(), alignment, format);
    }

    public void AppendFormatted(ReadOnlySpan<char> value)
    {
        AppendEscaped(value);
    }

    public void AppendFormatted(ReadOnlySpan<char> value, int alignment = 0, string? format = null)
    {
        AppendFormatted(value.ToString(), alignment, format);
    }

    public void AppendFormatted(RawText rawText)
    {
        _innerHandler.AppendFormatted(rawText.Text);
    }

    private void AppendEscaped(ReadOnlySpan<char> text)
    {
        if (text.Length == 0)
        {
            return;
        }

        var startPos = 0;
        for (var i = 0; i < text.Length; i++)
        {
            var c = text[i];
            if (c is ']' or '[')
            {
                if (i != startPos)
                {
                    _innerHandler.AppendFormatted(text[startPos..i]);
                }

                _innerHandler.AppendFormatted(c);
                _innerHandler.AppendFormatted(c);
                startPos = i + 1;
            }
        }

        _innerHandler.AppendFormatted(text[startPos..]);
    }
}
