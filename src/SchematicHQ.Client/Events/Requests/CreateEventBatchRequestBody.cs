using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public class CreateEventBatchRequestBody
{
    [JsonPropertyName("events")]
    public List<CreateEventRequestBody> Events { get; init; }
}
