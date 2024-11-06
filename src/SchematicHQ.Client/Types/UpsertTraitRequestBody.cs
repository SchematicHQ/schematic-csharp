using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record UpsertTraitRequestBody
{
    /// <summary>
    /// Amount to increment the trait by (positive or negative)
    /// </summary>
    [JsonPropertyName("incr")]
    public int? Incr { get; set; }

    /// <summary>
    /// Key/value pairs too identify a company or user
    /// </summary>
    [JsonPropertyName("keys")]
    public Dictionary<string, string> Keys { get; set; } = new Dictionary<string, string>();

    /// <summary>
    /// Value to set the trait to
    /// </summary>
    [JsonPropertyName("set")]
    public string? Set { get; set; }

    /// <summary>
    /// Name of the trait to update
    /// </summary>
    [JsonPropertyName("trait")]
    public required string Trait { get; set; }

    /// <summary>
    /// Unless this is set, the company or user will be created if it does not already exist
    /// </summary>
    [JsonPropertyName("update_only")]
    public bool? UpdateOnly { get; set; }
}
