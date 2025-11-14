using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record GetWebhookEventRequest
{
    /// <summary>
    /// webhook_event_id
    /// </summary>
    [JsonIgnore]
    public required string WebhookEventId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
