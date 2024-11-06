using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record PreviewComponentDataParams
{
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; set; }

    [JsonPropertyName("component_id")]
    public string? ComponentId { get; set; }
}
