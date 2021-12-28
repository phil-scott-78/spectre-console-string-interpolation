using Bogus;

namespace StringInterpolation.Benchmarks;

internal class FakeTestData
{
    public static readonly Faker<FakeTestData> DataFaker;
    public string Name { get; }
    public string Description { get; }
    public int FavoriteNumber { get; }
    public DateTime HireData { get; }
    public string ImportantInfo { get; }

    private FakeTestData(string name, string description, int favoriteNumber, DateTime hireData, string importantInfo)
    {
        this.Name = name;
        this.Description = description;
        this.FavoriteNumber = favoriteNumber;
        this.HireData = hireData;
        this.ImportantInfo = importantInfo;
    }

    static FakeTestData()
    {
        // make sure our data is consistent between runs.
        Randomizer.Seed = new Random(2456165);
        DataFaker = new Faker<FakeTestData>()
            .CustomInstantiator(f => new FakeTestData(
                f.Person.FullName,
                f.Company.CatchPhrase(),
                f.Random.Number(0, 100),
                f.Date.Past(100),
                f.Random.String2(50, 150, "abcdefghijklmnopqrstuvwxyz!@#$[]{}")));
    }
}
