using System.Text.Json.Serialization;
using OneOf;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record CreateEventRequestBody
{
    [JsonPropertyName("body")]
    public OneOf<EventBodyTrack, EventBodyFlagCheck, EventBodyIdentify>? Body { get; set; }

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

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
