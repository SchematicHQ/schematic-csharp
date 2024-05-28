using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public class CountResponse
{
    [JsonPropertyName("count")]
    public int Count { get; init; }
}
