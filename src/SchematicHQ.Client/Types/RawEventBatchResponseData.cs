using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record RawEventBatchResponseData
{
    [JsonPropertyName("events")]
    public IEnumerable<RawEventResponseData> Events { get; set; } =
        new List<RawEventResponseData>();
}
