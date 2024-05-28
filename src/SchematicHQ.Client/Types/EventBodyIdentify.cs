using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public class EventBodyIdentify
{
    /// <summary>
    /// Information about the company associated with the user; required only if it is a new user
    /// </summary>
    [JsonPropertyName("company")]
    public EventBodyIdentifyCompany? Company { get; init; }

    /// <summary>
    /// Key-value pairs to identify the user
    /// </summary>
    [JsonPropertyName("keys")]
    public Dictionary<string, object> Keys { get; init; }

    /// <summary>
    /// The display name of the user being identified; required only if it is a new user
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// A map of user trait names to trait values
    /// </summary>
    [JsonPropertyName("traits")]
    public Dictionary<string, object>? Traits { get; init; }
}
