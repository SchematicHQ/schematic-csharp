using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CancelSubscriptionRequest
{
    /// <summary>
    /// If false, subscription cancels at period end. Defaults to true.
    /// </summary>
    [JsonPropertyName("cancel_immediately")]
    public bool? CancelImmediately { get; set; }

    [JsonPropertyName("company_id")]
    public required string CompanyId { get; set; }

    /// <summary>
    /// If true and cancel_immediately is true, issue prorated credit. Defaults to true.
    /// </summary>
    [JsonPropertyName("prorate")]
    public bool? Prorate { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
