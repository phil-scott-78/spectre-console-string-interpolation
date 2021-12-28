using BenchmarkDotNet.Attributes;
using StringInterpolation.InterpolationImplementations;

namespace StringInterpolation.Benchmarks;

[MemoryDiagnoser]
public class MarkupEscapeBenchmark
{
    private List<FakeTestData> _testData = null!;

    [GlobalSetup]
    public void Setup()
    {
        _testData = FakeTestData.DataFaker.Generate(100);
    }

    [Benchmark(Baseline = true)]
    public string ClassicMarkup()
    {
        var sb = new StringBuilder();
        foreach (var data in _testData)
        {
            var s = $"My name is {data.Name.EscapeMarkup()} and I was hired on {data.HireData}. My favorite number is {data.FavoriteNumber}. My catch phrase is {data.Description.EscapeMarkup()} and my secret info is {data.ImportantInfo.EscapeMarkup()}";
            sb.AppendLine(s);
        }

        return sb.ToString();
    }

    [Benchmark]
    public string FormattableString()
    {
        var sb = new StringBuilder();
        foreach (var data in _testData)
        {
            var s = EscapeHelpers.FormattableEscape($"My name is {data.Name} and I was hired on {data.HireData}. My favorite number is {data.FavoriteNumber}. My catch phrase is {data.Description} and my secret info is {data.ImportantInfo}");
            sb.AppendLine(s);
        }

        return sb.ToString();
    }

    [Benchmark]
    public string WrappedInterpolatedStringHandler()
    {
        var sb = new StringBuilder();
        foreach (var data in _testData)
        {
            var s = EscapeHelpers.WrappedMarkupInterpolatedEscape($"My name is {data.Name} and I was hired on {data.HireData}. My favorite number is {data.FavoriteNumber}. My catch phrase is {data.Description} and my secret info is {data.ImportantInfo}");
            sb.AppendLine(s);
        }

        return sb.ToString();
    }
}
