using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CreateBillingCustomerRequestBody
{
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; set; }

    [JsonPropertyName("email")]
    public required string Email { get; set; }

    [JsonPropertyName("external_id")]
    public required string ExternalId { get; set; }

    [JsonPropertyName("failed_to_import")]
    public required bool FailedToImport { get; set; }

    [JsonPropertyName("meta")]
    public Dictionary<string, string> Meta { get; set; } = new Dictionary<string, string>();

    [JsonPropertyName("name")]
    public required string Name { get; set; }
}
