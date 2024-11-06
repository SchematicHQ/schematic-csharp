using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record GetFeatureUsageByCompanyParams
{
    [JsonPropertyName("keys")]
    public Dictionary<string, object?>? Keys { get; set; }
}
