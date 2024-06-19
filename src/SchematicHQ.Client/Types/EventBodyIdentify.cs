using System.Text.Json.Serialization;
using OneOf;
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
    public Dictionary<string, string> Keys { get; init; }

    /// <summary>
    /// The display name of the user being identified; required only if it is a new user
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
