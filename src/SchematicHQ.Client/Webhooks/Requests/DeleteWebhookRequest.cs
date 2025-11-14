using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record DeleteWebhookRequest
{
    /// <summary>
    /// webhook_id
    /// </summary>
    [JsonIgnore]
    public required string WebhookId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
