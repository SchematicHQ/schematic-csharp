using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public class CreateWebhookRequestBody
{
    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("request_types")]
    public List<string> RequestTypes { get; init; }

    [JsonPropertyName("url")]
    public string Url { get; init; }
}
