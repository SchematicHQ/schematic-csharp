using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CreateEventBatchRequestBody
{
    [JsonPropertyName("events")]
    public IEnumerable<CreateEventRequestBody> Events { get; set; } =
        new List<CreateEventRequestBody>();
}
