using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record FeatureCompanyResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Whether further usage is permitted.
    /// </summary>
    [JsonPropertyName("access")]
    public required bool Access { get; set; }

    /// <summary>
    /// The maximum amount of usage that is permitted; a null value indicates that unlimited usage is permitted.
    /// </summary>
    [JsonPropertyName("allocation")]
    public int? Allocation { get; set; }

    /// <summary>
    /// The type of allocation that is being used.
    /// </summary>
    [JsonPropertyName("allocation_type")]
    public required FeatureCompanyResponseDataAllocationType AllocationType { get; set; }

    [JsonPropertyName("company")]
    public CompanyDetailResponseData? Company { get; set; }

    /// <summary>
    /// Rate at which credits are consumed per unit of usage for credit-based features.
    /// </summary>
    [JsonPropertyName("credit_consumption_rate")]
    public double? CreditConsumptionRate { get; set; }

    [JsonPropertyName("credit_usage")]
    public CreditUsage? CreditUsage { get; set; }

    [JsonPropertyName("entitlement_expiration_date")]
    public DateTime? EntitlementExpirationDate { get; set; }

    [JsonPropertyName("entitlement_id")]
    public required string EntitlementId { get; set; }

    [JsonPropertyName("entitlement_type")]
    public required string EntitlementType { get; set; }

    [JsonPropertyName("feature")]
    public FeatureDetailResponseData? Feature { get; set; }

    /// <summary>
    /// The time at which the metric will resets.
    /// </summary>
    [JsonPropertyName("metric_reset_at")]
    public DateTime? MetricResetAt { get; set; }

    /// <summary>
    /// If the period is current_month, when the month resets.
    /// </summary>
    [JsonPropertyName("month_reset")]
    public string? MonthReset { get; set; }

    /// <summary>
    /// The period over which usage is measured.
    /// </summary>
    [JsonPropertyName("period")]
    public string? Period { get; set; }

    [JsonPropertyName("plan")]
    public PlanResponseData? Plan { get; set; }

    /// <summary>
    /// The amount of usage that has been consumed; a null value indicates that usage is not being measured.
    /// </summary>
    [JsonPropertyName("usage")]
    public int? Usage { get; set; }

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
