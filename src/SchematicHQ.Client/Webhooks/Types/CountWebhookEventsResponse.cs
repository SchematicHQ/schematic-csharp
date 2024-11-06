using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CountWebhookEventsResponse
{
    [JsonPropertyName("data")]
    public required CountResponse Data { get; set; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required CountWebhookEventsParams Params { get; set; }
}
