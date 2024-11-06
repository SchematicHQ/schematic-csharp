using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record UpsertBillingProductRequestBody
{
    [JsonPropertyName("billing_product_id")]
    public string? BillingProductId { get; set; }

    [JsonPropertyName("monthly_price_id")]
    public string? MonthlyPriceId { get; set; }

    [JsonPropertyName("yearly_price_id")]
    public string? YearlyPriceId { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
