namespace StringInterpolation;

public record RawText(string Text);

public static class EscapeMarkupHelpers
{
    public static RawText RawText(string text) => new(text);
}
