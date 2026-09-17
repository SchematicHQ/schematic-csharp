using NUnit.Framework;

namespace SchematicHQ.Client.Test.Credits.Leases;

/// <summary>
/// Runs the conformance vectors against both backends the SDK ships: the
/// per-process in-memory stores and the Redis stores.
/// </summary>
[TestFixture]
public class LeaseConformanceTests
{
    private static readonly string[] Files =
    {
        "lease-lifecycle.json",
        "reservation-lifecycle.json",
        "expiry.json",
        "crash-windows.json",
        "lease-manager.json",
        "check-flow.json",
        "track-settle.json",
    };

    public static IEnumerable<ConformanceCase> Cases() => ConformanceVectors.Cases(Files);

    [TestCaseSource(nameof(Cases))]
    public async Task Vector(ConformanceCase testCase)
    {
        var vector = ConformanceVectors.Load(testCase.FileName, testCase.Vector);
        var runner = new ConformanceRunner(testCase.Backend, vector);
        await runner.RunAsync(vector);
    }
}
