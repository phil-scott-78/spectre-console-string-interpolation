using StringInterpolation;
using StringInterpolation.InterpolationImplementations;

var name = "Phil";
var number = 42;
var date = DateTime.Now;
var badDataFormat = "My info [";
MarkupLineInterpolated($"Today is {date:f}.");
MarkupLineInterpolated($"My name is [blue]{name}[/], my favorite number is {number}. Today is {date} and the data is {badDataFormat}");


BenchmarkDotNet.Running.BenchmarkSwitcher
    .FromAssembly(typeof(Program).Assembly)
    .Run(args);

void MarkupLineInterpolated(ref WrappedMarkupInterpolatedStringHandler handler)
{
    AnsiConsole.MarkupLine(handler.ToStringAndClear());
}
