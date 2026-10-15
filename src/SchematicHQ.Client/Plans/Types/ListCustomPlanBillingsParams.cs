using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

/// <summary>
/// Input parameters
/// </summary>
[Serializable]
public record ListCustomPlanBillingsParams : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Filter by company ID
    /// </summary>
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; set; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    [JsonPropertyName("limit")]
    public long? Limit { get; set; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    [JsonPropertyName("offset")]
    public long? Offset { get; set; }

    /// <summary>
    /// Filter by the flow that created the billing record: custom_plan for a first publish, amendment for a republish, manage_plan for a standard plan assigned by invoice. Defaults to custom_plan unless plan_billing_sources is set.
    /// </summary>
    [JsonPropertyName("plan_billing_source")]
    public PlanBillingSource? PlanBillingSource { get; set; }

    /// <summary>
    /// Filter by multiple billing sources. Takes precedence over plan_billing_source.
    /// </summary>
    [JsonPropertyName("plan_billing_sources")]
    public IEnumerable<PlanBillingSource>? PlanBillingSources { get; set; }

    /// <summary>
    /// Filter by plan ID
    /// </summary>
    [JsonPropertyName("plan_id")]
    public string? PlanId { get; set; }

    /// <summary>
    /// Filter by the plan version the billing record was created for
    /// </summary>
    [JsonPropertyName("plan_version_id")]
    public string? PlanVersionId { get; set; }

    /// <summary>
    /// Filter by billing status
    /// </summary>
    [JsonPropertyName("status")]
    public CustomPlanBillingStatus? Status { get; set; }

    /// <summary>
    /// Filter by multiple billing statuses
    /// </summary>
    [JsonPropertyName("statuses")]
    public IEnumerable<CustomPlanBillingStatus>? Statuses { get; set; }

    [JsonIgnore]
    public ReadOnlyAdditionalProperties AdditionalProperties { get; private set; } = new();

    void IJsonOnDeserialized.OnDeserialized() =>
        AdditionalProperties.CopyFromExtensionData(_extensionData);

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
