using System.Text.Json.Serialization;
using OneOf;

#nullable enable

namespace SchematicHQ.Client;

public class EventBodyIdentifyCompany
{
    /// <summary>
    /// Key-value pairs to identify the company
    /// </summary>
    [JsonPropertyName("keys")]
    public Dictionary<string, string> Keys { get; init; }

    /// <summary>
    /// The display name of the company; required only if it is a new company
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// A map of trait names to trait values
    /// </summary>
    [JsonPropertyName("traits")]
    public Dictionary<
        string,
        OneOf<string, double, bool, OneOf<string, double, bool>>
    >? Traits { get; init; }
}
