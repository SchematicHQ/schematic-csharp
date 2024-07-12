using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CountResponse
{
    [JsonPropertyName("count")]
    public required int Count { get; init; }
}
