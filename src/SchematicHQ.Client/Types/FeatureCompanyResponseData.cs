using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record FeatureCompanyResponseData
{
    /// <summary>
    /// Whether further usage is permitted.
    /// </summary>
    [JsonPropertyName("access")]
    public required bool Access { get; init; }

    /// <summary>
    /// The maximum amount of usage that is permitted; a null value indicates that unlimited usage is permitted.
    /// </summary>
    [JsonPropertyName("allocation")]
    public int? Allocation { get; init; }

    /// <summary>
    /// The type of allocation that is being used.
    /// </summary>
    [JsonPropertyName("allocation_type")]
    public required FeatureCompanyResponseDataAllocationType AllocationType { get; init; }

    [JsonPropertyName("company")]
    public CompanyDetailResponseData? Company { get; init; }

    [JsonPropertyName("entitlement_expiration_date")]
    public DateTime? EntitlementExpirationDate { get; init; }

    [JsonPropertyName("entitlement_id")]
    public required string EntitlementId { get; init; }

    [JsonPropertyName("entitlement_type")]
    public required string EntitlementType { get; init; }

    [JsonPropertyName("feature")]
    public FeatureDetailResponseData? Feature { get; init; }

    /// <summary>
    /// The time at which the metric will resets.
    /// </summary>
    [JsonPropertyName("metric_reset_at")]
    public DateTime? MetricResetAt { get; init; }

    /// <summary>
    /// If the period is current_month, when the month resets.
    /// </summary>
    [JsonPropertyName("month_reset")]
    public string? MonthReset { get; init; }

    /// <summary>
    /// The period over which usage is measured.
    /// </summary>
    [JsonPropertyName("period")]
    public string? Period { get; init; }

    [JsonPropertyName("plan")]
    public PlanResponseData? Plan { get; init; }

    /// <summary>
    /// The amount of usage that has been consumed; a null value indicates that usage is not being measured.
    /// </summary>
    [JsonPropertyName("usage")]
    public int? Usage { get; init; }
}
