using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record BillingCustomerSubscription
{
    [JsonPropertyName("currency")]
    public required string Currency { get; set; }

    [JsonPropertyName("expired_at")]
    public DateTime? ExpiredAt { get; set; }

    [JsonPropertyName("total_price")]
    public required int TotalPrice { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
