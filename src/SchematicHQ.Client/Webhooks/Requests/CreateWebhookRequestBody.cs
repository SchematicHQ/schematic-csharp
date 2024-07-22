using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record CreateWebhookRequestBody
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("request_types")]
    public IEnumerable<CreateWebhookRequestBodyRequestTypesItem> RequestTypes { get; init; } =
        new List<CreateWebhookRequestBodyRequestTypesItem>();

    [JsonPropertyName("url")]
    public required string Url { get; init; }
}
