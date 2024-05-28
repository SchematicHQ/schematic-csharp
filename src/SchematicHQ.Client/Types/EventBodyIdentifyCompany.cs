using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public class EventBodyIdentifyCompany
{
    /// <summary>
    /// Key-value pairs to identify the company
    /// </summary>
    [JsonPropertyName("keys")]
    public Dictionary<string, object> Keys { get; init; }

    /// <summary>
    /// The display name of the company; required only if it is a new company
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// A map of company trait names to trait values
    /// </summary>
    [JsonPropertyName("traits")]
    public Dictionary<string, object>? Traits { get; init; }
}
