using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record UpdateTrialEndRequestBody
{
    /// <summary>
    /// subscription_id
    /// </summary>
    [JsonIgnore]
    public required string SubscriptionId { get; set; }

    [JsonPropertyName("trial_end")]
    public DateTime? TrialEnd { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
