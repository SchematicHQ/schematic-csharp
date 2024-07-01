using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public class CreateWebhookRequestBody
{
    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("request_types")]
    public IEnumerable<CreateWebhookRequestBodyRequestTypesItem> RequestTypes { get; init; }

    [JsonPropertyName("url")]
    public string Url { get; init; }
}
