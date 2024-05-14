using System.Text.Json.Serialization;

namespace Schematic.Client;

public class UpsertUserSubRequestBody
{
    /// <summary>
    /// Optionally specify company using Schematic company ID
    /// </summary>
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; init; }

    [JsonPropertyName("keys")]
    public Dictionary<string, object> Keys { get; init; }

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
