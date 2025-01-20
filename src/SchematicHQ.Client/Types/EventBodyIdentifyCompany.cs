using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record EventBodyIdentifyCompany
{
    /// <summary>
    /// Key-value pairs to identify the company
    /// </summary>
    [JsonPropertyName("keys")]
    public Dictionary<string, string> Keys { get; set; } = new Dictionary<string, string>();

    /// <summary>
    /// The display name of the company; required only if it is a new company
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// A map of trait names to trait values
    /// </summary>
    [JsonPropertyName("traits")]
    public object? Traits { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
