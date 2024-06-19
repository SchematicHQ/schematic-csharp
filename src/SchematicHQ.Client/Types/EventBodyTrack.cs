using System.Text.Json.Serialization;
using OneOf;

#nullable enable

namespace SchematicHQ.Client;

public class EventBodyTrack
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
    public string Event { get; init; }

    /// <summary>
    /// A map of trait names to trait values
    /// </summary>
    [JsonPropertyName("traits")]
    public Dictionary<
        string,
        OneOf<string, double, bool, OneOf<string, double, bool>>
    >? Traits { get; init; }

    /// <summary>
    /// Key-value pairs to identify user associated with track event
    /// </summary>
    [JsonPropertyName("user")]
    public Dictionary<string, string>? User { get; init; }
}
