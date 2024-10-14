using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record BillingSubscriptionResponseData
{
    [JsonPropertyName("currency")]
    public required string Currency { get; set; }

    [JsonPropertyName("expired_at")]
    public DateTime? ExpiredAt { get; set; }

    [JsonPropertyName("external_id")]
    public required string ExternalId { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("interval")]
    public required string Interval { get; set; }

    [JsonPropertyName("total_price")]
    public required int TotalPrice { get; set; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
