using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record FeatureUsageResponseData
{
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
    public required FeatureUsageResponseDataAllocationType AllocationType { get; set; }

    [JsonPropertyName("entitlement_id")]
    public required string EntitlementId { get; set; }

    [JsonPropertyName("entitlement_type")]
    public required string EntitlementType { get; set; }

    [JsonPropertyName("feature")]
    public FeatureDetailResponseData? Feature { get; set; }

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
}
