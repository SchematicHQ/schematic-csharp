using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record PauseSubscriptionRequest
{
    [JsonPropertyName("company_id")]
    public required string CompanyId { get; set; }

    /// <summary>
    /// How many months to pause for, counted from the end of the current billing period. Between 1 and 6.
    /// </summary>
    [JsonPropertyName("months")]
    public required long Months { get; set; }

    /// <summary>
    /// The payment provider's ID for the subscription to pause. Required when the company has more than one subscription; otherwise defaults to its only one.
    /// </summary>
    [JsonPropertyName("subscription_external_id")]
    public string? SubscriptionExternalId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
