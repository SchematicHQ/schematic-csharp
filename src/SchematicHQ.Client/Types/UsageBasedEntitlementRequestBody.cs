using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record UsageBasedEntitlementRequestBody
{
    [JsonPropertyName("monthly_metered_price_id")]
    public string? MonthlyMeteredPriceId { get; set; }

    [JsonPropertyName("price_behavior")]
    public string? PriceBehavior { get; set; }

    [JsonPropertyName("yearly_metered_price_id")]
    public string? YearlyMeteredPriceId { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
