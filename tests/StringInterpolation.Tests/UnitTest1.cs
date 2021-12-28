using System;
using System.Text;
using Shouldly;
using Spectre.Console;
using StringInterpolation.InterpolationImplementations;
using Xunit;
using static StringInterpolation.EscapeMarkupHelpers;

namespace StringInterpolation.Tests;

public class FormattableStringTests
{
    [Fact]
    public void Can_format()
    {
        var name = "Phil";
        var date = new DateTime(1978, 12, 29);
        var number = 3025;
        var data = "[t]est[";

        const string FullExpected =
            "My name is       Phil, born on 12/29/1978. My Favorite number is 3,025. My data is [[t]]est[[.";

        EscapeHelpers
            .FormattableEscape(
                $"My name is {name,10}, born on {date:d}. My Favorite number is {number:N0}. My data is {data}.")
            .ShouldBe(FullExpected);

        EscapeHelpers
            .WrappedMarkupInterpolatedEscape(
                $"My name is {name,10}, born on {date:d}. My Favorite number is {number:N0}. My data is {data}.")
            .ShouldBe(FullExpected);

        // not test for InterpolatedStringHandlerEscape, it doesn't support formatting.
    }

    [Fact]
    public void Can_escape()
    {
        var name = "Phil";
        var date = new DateTime(1978, 12, 29);
        var number = 3025;
        var data = "[t]est[";

        const string FullExpected =
            "My name is Phil, born on 12/29/1978 12:00:00 AM. My Favorite number is 3025. My data is [[t]]est[[.";

        EscapeHelpers
            .FormattableEscape($"My name is {name}, born on {date}. My Favorite number is {number}. My data is {data}.")
            .ShouldBe(FullExpected);

        EscapeHelpers
            .MarkupInterpolatedEscape(
                $"My name is {name}, born on {date}. My Favorite number is {number}. My data is {data}.")
            .ShouldBe(FullExpected);

        EscapeHelpers
            .WrappedMarkupInterpolatedEscape(
                $"My name is {name}, born on {date}. My Favorite number is {number}. My data is {data}.")
            .ShouldBe(FullExpected);
    }

    [Fact]
    public void Can_reuse_string_builder_instance()
    {
        var name = "Phil";
        var number = 3025;

        var sb = new StringBuilder();
        sb.AppendEscapeMarkup($"My name is {name}. ");
        sb.AppendEscapeMarkup($"My favorite number is {number}");

        sb.ToString()
            .ShouldBe("My name is Phil. My favorite number is 3025");
    }

    [Fact]
    public void Can_reuse_string_builder_instance_with_wrapped_handler()
    {
        var name = "Phil";
        var number = 3025;

        var sb = new StringBuilder();
        sb.AppendEscapeMarkup($"My name is {name}. ");
        sb.AppendEscapeMarkup($"My favorite number is {number}");

        sb.ToString()
            .ShouldBe("My name is Phil. My favorite number is 3025");
    }

    [Fact]
    public void Can_handle_raw_text()
    {
        var name = "Phil";
        var color = "[blue]";

        EscapeHelpers.WrappedMarkupInterpolatedEscape($"My name is {new RawText(color)}{name}[/].")
            .ShouldBe("My name is [blue]Phil[/].");
    }

    [Fact]
    public void Can_handle_ReadOnlySpan_text()
    {
        var name = "Phil";
        var data = "My data [ is good data[, ]right?";

        EscapeHelpers.WrappedMarkupInterpolatedEscape($"My name is {name.AsSpan()}. {data.AsSpan()}")
            .ShouldBe("My name is Phil. My data [[ is good data[[, ]]right?");
    }
}
