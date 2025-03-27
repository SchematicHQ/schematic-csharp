using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

/// <summary>
/// Input parameters
/// </summary>
public record ListPlansParams
{
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; set; }

    /// <summary>
    /// Filter out plans that do not have a billing product ID
    /// </summary>
    [JsonPropertyName("has_product_id")]
    public bool? HasProductId { get; set; }

    [JsonPropertyName("ids")]
    public IEnumerable<string>? Ids { get; set; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    [JsonPropertyName("limit")]
    public int? Limit { get; set; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    [JsonPropertyName("offset")]
    public int? Offset { get; set; }

    /// <summary>
    /// Filter by plan type
    /// </summary>
    [JsonPropertyName("plan_type")]
    public ListPlansResponseParamsPlanType? PlanType { get; set; }

    [JsonPropertyName("q")]
    public string? Q { get; set; }

    /// <summary>
    /// Filter out plans that already have a plan entitlement for the specified feature ID
    /// </summary>
    [JsonPropertyName("without_entitlement_for")]
    public string? WithoutEntitlementFor { get; set; }

    /// <summary>
    /// Filter out plans that have a paid billing product ID
    /// </summary>
    [JsonPropertyName("without_paid_product_id")]
    public bool? WithoutPaidProductId { get; set; }

    /// <summary>
    /// Filter out plans that have a billing product ID
    /// </summary>
    [JsonPropertyName("without_product_id")]
    public bool? WithoutProductId { get; set; }

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
