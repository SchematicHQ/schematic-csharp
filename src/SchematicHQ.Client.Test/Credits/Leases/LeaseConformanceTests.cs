using NUnit.Framework;

namespace SchematicHQ.Client.Test.Credits.Leases;

/// <summary>
/// Runs the conformance vectors against both backends the SDK ships: the
/// per-process in-memory stores and the Redis stores.
/// </summary>
[TestFixture]
public class LeaseConformanceTests
{
    public static IEnumerable<ConformanceCase> Cases() => ConformanceVectors.Cases();

    [TestCaseSource(nameof(Cases))]
    public async Task Vector(ConformanceCase testCase)
    {
        var vector = ConformanceVectors.Load(testCase.FileName, testCase.Vector);
        var runner = new ConformanceRunner(testCase.Backend, vector);
        await runner.RunAsync(vector);
    }

    [Test]
    public void An_Expect_Key_The_Runner_Does_Not_Assert_On_Fails()
    {
        var op = System.Text.Json.JsonDocument
            .Parse("{\"op\":\"get_lease\",\"expect\":{\"granted_amounts\":1}}")
            .RootElement;

        // A typo or a key from a newer reference implementation has to fail
        // loudly: skipping it leaves a vector that passes while pinning nothing.
        Assert.Throws<AssertionException>(
            () => ConformanceRunner.AssertExpectKeysRecognized("get_lease", op)
        );
    }

    [Test]
    public void Every_Vector_File_On_Disk_Is_Run()
    {
        var onDisk = System.IO.Directory
            .GetFiles(ConformanceVectors.Directory(), "*.json")
            .Select(Path.GetFileName)
            .ToArray();
        var run = Cases().Select(testCase => testCase.FileName).Distinct().ToArray();

        // A vector file synced from the reference implementation has to run
        // here without anyone enrolling it, so nothing can be added upstream
        // and silently skipped.
        Assert.That(run, Is.EquivalentTo(onDisk));
        Assert.That(run, Is.Not.Empty);
    }
}
