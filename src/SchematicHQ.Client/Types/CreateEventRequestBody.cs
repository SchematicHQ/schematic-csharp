using System.Text.Json;
using System.Text.Json.Serialization;
using OneOf;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreateEventRequestBody : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("body")]
    public OneOf<
        CreateEventRequestBodyBodyEvent,
        CreateEventRequestBodyBodyCompanyId,
        CreateEventRequestBodyBodyKeys
    >? Body { get; set; }

    /// <summary>
    /// Either 'identify' or 'track'
    /// </summary>
    [JsonPropertyName("event_type")]
    public required CreateEventRequestBodyEventType EventType { get; set; }

    /// <summary>
    /// Optionally provide a timestamp at which the event was sent to Schematic
    /// </summary>
    [JsonPropertyName("sent_at")]
    public DateTime? SentAt { get; set; }

    [JsonIgnore]
    public ReadOnlyAdditionalProperties AdditionalProperties { get; private set; } = new();

    void IJsonOnDeserialized.OnDeserialized() =>
        AdditionalProperties.CopyFromExtensionData(_extensionData);

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
