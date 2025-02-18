using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

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

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
