using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record UpdateScheduledCheckoutRequest
{
    [JsonPropertyName("execute_after")]
    public DateTime? ExecuteAfter { get; set; }

    [JsonPropertyName("status")]
    public ScheduledCheckoutStatus? Status { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
