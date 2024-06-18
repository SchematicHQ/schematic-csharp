using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public class WebhookResponseData
{
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; init; }

    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("request_types")]
    public List<string> RequestTypes { get; init; }

    [JsonPropertyName("status")]
    public string Status { get; init; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; init; }

    [JsonPropertyName("url")]
    public string Url { get; init; }
}
