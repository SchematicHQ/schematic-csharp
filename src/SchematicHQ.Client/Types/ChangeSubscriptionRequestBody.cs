using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record ChangeSubscriptionRequestBody
{
    [JsonPropertyName("add_on_ids")]
    public IEnumerable<UpdateAddOnRequestBody> AddOnIds { get; set; } =
        new List<UpdateAddOnRequestBody>();

    [JsonPropertyName("new_plan_id")]
    public required string NewPlanId { get; set; }

    [JsonPropertyName("new_price_id")]
    public required string NewPriceId { get; set; }

    [JsonPropertyName("pay_in_advance")]
    public IEnumerable<UpdatePayInAdvanceRequestBody> PayInAdvance { get; set; } =
        new List<UpdatePayInAdvanceRequestBody>();

    [JsonPropertyName("payment_method_id")]
    public string? PaymentMethodId { get; set; }

    [JsonPropertyName("promo_code")]
    public string? PromoCode { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
