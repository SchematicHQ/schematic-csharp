using System.Text.Json.Serialization;
using OneOf;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public class CreateEventRequestBody
{
    [JsonPropertyName("body")]
    public OneOf<EventBodyTrack, EventBodyIdentify>? Body { get; init; }

    /// <summary>
    /// Either 'identify' or 'track'
    /// </summary>
    [JsonPropertyName("event_type")]
    public CreateEventRequestBodyEventType EventType { get; init; }

    /// <summary>
    /// Optionally provide a timestamp at which the event was sent to Schematic
    /// </summary>
    [JsonPropertyName("sent_at")]
    public DateTime? SentAt { get; init; }
}
