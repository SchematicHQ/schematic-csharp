using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record EventBodyTrack
{
    /// <summary>
    /// Key-value pairs to identify company associated with track event
    /// </summary>
    [JsonPropertyName("company")]
    public Dictionary<string, string>? Company { get; init; }

    /// <summary>
    /// The name of the type of track event
    /// </summary>
    [JsonPropertyName("event")]
    public required string Event { get; init; }

    /// <summary>
    /// A map of trait names to trait values
    /// </summary>
    [JsonPropertyName("traits")]
    public Dictionary<string, object>? Traits { get; init; }

    /// <summary>
    /// Key-value pairs to identify user associated with track event
    /// </summary>
    [JsonPropertyName("user")]
    public Dictionary<string, string>? User { get; init; }
}
