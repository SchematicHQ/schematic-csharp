using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record UpsertUserRequestBody
{
    /// <summary>
    /// Optionally specify company using key/value pairs
    /// </summary>
    [JsonPropertyName("company")]
    public Dictionary<string, string> Company { get; init; } = new Dictionary<string, string>();

    /// <summary>
    /// Optionally specify company using Schematic company ID
    /// </summary>
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; init; }

    /// <summary>
    /// If you know the Schematic ID, you can use that here instead of keys
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("keys")]
    public Dictionary<string, string> Keys { get; init; } = new Dictionary<string, string>();

    [JsonPropertyName("last_seen_at")]
    public DateTime? LastSeenAt { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// A map of trait names to trait values
    /// </summary>
    [JsonPropertyName("traits")]
    public Dictionary<string, object>? Traits { get; init; }

    [JsonPropertyName("update_only")]
    public bool? UpdateOnly { get; init; }
}
