using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record PreviewComponentDataParams
{
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; set; }

    [JsonPropertyName("component_id")]
    public string? ComponentId { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
