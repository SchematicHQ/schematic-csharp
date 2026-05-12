using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record StartDataImportRequestBody
{
    [JsonPropertyName("company_matching_criteria")]
    public CompanyMatchingCriteria? CompanyMatchingCriteria { get; set; }

    [JsonPropertyName("company_matching_field")]
    public string? CompanyMatchingField { get; set; }

    [JsonPropertyName("integration_id")]
    public required string IntegrationId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
