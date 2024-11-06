using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CreateWebhookRequestBody
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("request_types")]
    public IEnumerable<CreateWebhookRequestBodyRequestTypesItem> RequestTypes { get; set; } =
        new List<CreateWebhookRequestBodyRequestTypesItem>();

    [JsonPropertyName("url")]
    public required string Url { get; set; }
}
