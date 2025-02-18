using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record GetFeatureUsageByCompanyParams
{
    [JsonPropertyName("keys")]
    public object? Keys { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
