using System.Reflection;

namespace SchematicHQ.Client.RulesEngine.Utils;

/// <summary>
/// Provides the canonical rules-engine schema version used to version the datastream cache.
/// </summary>
public static class SchemaVersionGenerator
{
    // Fern requires the generated RulesEngineSchemaVersion enum to carry a placeholder member
    // alongside the single real member, which holds the canonical rules-engine schema version.
    private const string FernPlaceholderValue = "placeholder-for-fern-compatibility";
    private const string FallbackVersion = "1";

    private static readonly string _schemaVersion = ResolveSchemaVersion();

    /// <summary>
    /// Gets the global schema version for all rules-engine models.
    ///
    /// <para>Sourced from the Fern-generated <see cref="RulesEngineSchemaVersion"/> enum (the
    /// canonical version emitted by codegen, which changes whenever the model schema changes).
    /// Resolved by reflection rather than a direct symbol reference because Fern encodes the
    /// version into the member <em>name</em> (e.g. <c>V5B3E7220</c>), so a direct reference
    /// would break on every schema bump; reflecting for the non-placeholder value is stable.</para>
    /// </summary>
    public static string GetGlobalSchemaVersion() => _schemaVersion;

    private static string ResolveSchemaVersion()
    {
        var versions = typeof(RulesEngineSchemaVersion)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(f => f.FieldType == typeof(RulesEngineSchemaVersion))
            .Select(f => ((RulesEngineSchemaVersion)f.GetValue(null)!).Value)
            .Where(v => !string.IsNullOrEmpty(v) && v != FernPlaceholderValue)
            .Distinct()
            .ToList();

        // Exactly one real value is expected. Anything else means Fern changed the enum's
        // shape — fall back to a stable constant so cache keys remain well-formed.
        return versions.Count == 1 ? versions[0] : FallbackVersion;
    }
}
