using System.Text.Json;

namespace SchematicHQ.Client.Test.Credits.Leases;

/// <summary>
/// Locates and parses the language-agnostic vectors in <c>conformance/</c>.
/// The vector schema and the semantics they pin are documented in
/// <c>conformance/SPEC.md</c>; the runner is the only language-specific piece.
/// </summary>
public static class ConformanceVectors
{
    /// <summary>
    /// The fixed virtual instant every vector starts at. Each <c>*_at_ms</c>
    /// field is an offset from it, and only <c>advance_clock</c> moves the
    /// clock.
    /// </summary>
    public static readonly DateTime T0 = new(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public static string Directory()
    {
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir != null)
        {
            var candidate = Path.Combine(dir.FullName, "conformance", "vectors");
            if (System.IO.Directory.Exists(candidate))
            {
                return candidate;
            }
            dir = dir.Parent;
        }
        throw new DirectoryNotFoundException(
            "conformance/vectors was not found above " + AppContext.BaseDirectory
        );
    }

    public static IEnumerable<ConformanceCase> Cases(IEnumerable<string> fileNames)
    {
        var directory = Directory();
        foreach (var fileName in fileNames)
        {
            var path = Path.Combine(directory, fileName);
            using var document = JsonDocument.Parse(File.ReadAllText(path));
            var category = document.RootElement.GetProperty("category").GetString()!;
            foreach (var vector in document.RootElement.GetProperty("vectors").EnumerateArray())
            {
                var name = vector.GetProperty("name").GetString()!;
                foreach (var backend in Backends(vector))
                {
                    yield return new ConformanceCase(fileName, category, name, backend);
                }
            }
        }
    }

    public static JsonElement Load(string fileName, string vectorName)
    {
        var path = Path.Combine(Directory(), fileName);
        var document = JsonDocument.Parse(File.ReadAllText(path));
        foreach (var vector in document.RootElement.GetProperty("vectors").EnumerateArray())
        {
            if (vector.GetProperty("name").GetString() == vectorName)
            {
                return vector.Clone();
            }
        }
        throw new ArgumentException($"vector {vectorName} not found in {fileName}");
    }

    private static IEnumerable<string> Backends(JsonElement vector)
    {
        if (!vector.TryGetProperty("backends", out var backends))
        {
            // Omitted means the vector must pass against every backend the SDK
            // ships.
            return new[] { "in_memory", "redis" };
        }
        return backends.EnumerateArray().Select(entry => entry.GetString()!).ToArray();
    }
}

/// <summary>
/// One vector run against one backend. NUnit prints <see cref="ToString"/> as
/// the test name, so a failure names the vector and the backend it failed on.
/// </summary>
public sealed class ConformanceCase
{
    public ConformanceCase(string fileName, string category, string vector, string backend)
    {
        FileName = fileName;
        Category = category;
        Vector = vector;
        Backend = backend;
    }

    public string FileName { get; }

    public string Category { get; }

    public string Vector { get; }

    public string Backend { get; }

    public override string ToString() => $"{Category}/{Vector} [{Backend}]";
}
