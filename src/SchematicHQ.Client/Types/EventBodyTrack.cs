using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record EventBodyTrack
{
    /// <summary>
    /// Key-value pairs to identify company associated with track event
    /// </summary>
    [JsonPropertyName("company")]
    public Dictionary<string, string>? Company { get; set; }

    /// <summary>
    /// The name of the type of track event
    /// </summary>
    [JsonPropertyName("event")]
    public required string Event { get; set; }

    /// <summary>
    /// Optionally specify the quantity of the event
    /// </summary>
    [JsonPropertyName("quantity")]
    public int? Quantity { get; set; }

    /// <summary>
    /// A map of trait names to trait values
    /// </summary>
    [JsonPropertyName("traits")]
    public object? Traits { get; set; }

    /// <summary>
    /// Key-value pairs to identify user associated with track event
    /// </summary>
    [JsonPropertyName("user")]
    public Dictionary<string, string>? User { get; set; }

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
