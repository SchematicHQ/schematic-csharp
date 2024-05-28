using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public class UpsertCompanyRequestBody
{
    [JsonPropertyName("id")]
    public string? Id { get; init; }

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
