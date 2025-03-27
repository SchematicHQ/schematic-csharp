using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record ChangeSubscriptionInternalRequestBody
{
    [JsonPropertyName("add_on_ids")]
    public IEnumerable<UpdateAddOnRequestBody> AddOnIds { get; set; } =
        new List<UpdateAddOnRequestBody>();

    [JsonPropertyName("company_id")]
    public required string CompanyId { get; set; }

    [JsonPropertyName("coupon_external_id")]
    public string? CouponExternalId { get; set; }

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

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
