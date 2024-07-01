using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public class UpdateWebhookRequestBody
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("request_types")]
    public IEnumerable<UpdateWebhookRequestBodyRequestTypesItem>? RequestTypes { get; init; }

    [JsonPropertyName("status")]
    public UpdateWebhookRequestBodyStatus? Status { get; init; }

    [JsonPropertyName("url")]
    public string? Url { get; init; }
}
