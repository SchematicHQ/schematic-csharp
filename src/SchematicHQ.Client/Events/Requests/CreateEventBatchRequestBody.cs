using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public class CreateEventBatchRequestBody
{
    [JsonPropertyName("events")]
    public IEnumerable<CreateEventRequestBody> Events { get; init; }
}
