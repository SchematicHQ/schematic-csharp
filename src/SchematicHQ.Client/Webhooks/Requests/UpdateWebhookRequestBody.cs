using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record UpdateWebhookRequestBody
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("request_types")]
    public IEnumerable<UpdateWebhookRequestBodyRequestTypesItem>? RequestTypes { get; set; }

    [JsonPropertyName("status")]
    public UpdateWebhookRequestBodyStatus? Status { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }
}
