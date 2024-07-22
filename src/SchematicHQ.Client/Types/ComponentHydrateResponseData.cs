using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record ComponentHydrateResponseData
{
    [JsonPropertyName("company")]
    public CompanyDetailResponseData? Company { get; init; }

    [JsonPropertyName("component")]
    public ComponentResponseData? Component { get; init; }

    [JsonPropertyName("feature_usage")]
    public FeatureUsageDetailResponseData? FeatureUsage { get; init; }
}
