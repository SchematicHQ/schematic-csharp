using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record ChangeSubscriptionRequestBody
{
    [JsonPropertyName("add_on_ids")]
    public IEnumerable<UpdateAddOnRequestBody> AddOnIds { get; init; } =
        new List<UpdateAddOnRequestBody>();

    [JsonPropertyName("new_plan_id")]
    public required string NewPlanId { get; init; }

    [JsonPropertyName("new_price_id")]
    public required string NewPriceId { get; init; }

    [JsonPropertyName("pay_in_advance")]
    public IEnumerable<UpdatePayInAdvanceRequestBody> PayInAdvance { get; init; } =
        new List<UpdatePayInAdvanceRequestBody>();

    [JsonPropertyName("payment_method_id")]
    public string? PaymentMethodId { get; init; }

    [JsonPropertyName("promo_code")]
    public string? PromoCode { get; init; }
}
