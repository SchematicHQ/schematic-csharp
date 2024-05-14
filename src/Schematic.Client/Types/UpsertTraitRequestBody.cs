using System.Text.Json.Serialization;

namespace Schematic.Client;

public class UpsertTraitRequestBody
{
    /// <summary>
    /// Amount to increment the trait by (positive or negative)
    /// </summary>
    [JsonPropertyName("incr")]
    public int? Incr { get; init; }

    /// <summary>
    /// Key/value pairs too identify a company or user
    /// </summary>
    [JsonPropertyName("keys")]
    public Dictionary<string, object> Keys { get; init; }

    /// <summary>
    /// Value to set the trait to
    /// </summary>
    [JsonPropertyName("set")]
    public string? Set { get; init; }

    /// <summary>
    /// Name of the trait to update
    /// </summary>
    [JsonPropertyName("trait")]
    public string Trait { get; init; }

    /// <summary>
    /// Unless this is set, the company or user will be created if it does not already exist
    /// </summary>
    [JsonPropertyName("update_only")]
    public bool? UpdateOnly { get; init; }
}
