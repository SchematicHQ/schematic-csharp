using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record UpsertBillingProductRequestBody
{
    [JsonPropertyName("billing_product_id")]
    public string? BillingProductId { get; set; }

    [JsonPropertyName("charge_type")]
    public required UpsertBillingProductRequestBodyChargeType ChargeType { get; set; }

    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    [JsonPropertyName("is_trialable")]
    public required bool IsTrialable { get; set; }

    [JsonPropertyName("monthly_price")]
    public int? MonthlyPrice { get; set; }

    [JsonPropertyName("monthly_price_id")]
    public string? MonthlyPriceId { get; set; }

    [JsonPropertyName("one_time_price")]
    public int? OneTimePrice { get; set; }

    [JsonPropertyName("one_time_price_id")]
    public string? OneTimePriceId { get; set; }

    [JsonPropertyName("trial_days")]
    public int? TrialDays { get; set; }

    [JsonPropertyName("yearly_price")]
    public int? YearlyPrice { get; set; }

    [JsonPropertyName("yearly_price_id")]
    public string? YearlyPriceId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
