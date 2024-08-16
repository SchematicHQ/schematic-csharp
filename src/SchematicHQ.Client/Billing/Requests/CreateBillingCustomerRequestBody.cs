using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CreateBillingCustomerRequestBody
{
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; init; }

    [JsonPropertyName("email")]
    public required string Email { get; init; }

    [JsonPropertyName("external_id")]
    public required string ExternalId { get; init; }

    [JsonPropertyName("failed_to_import")]
    public required bool FailedToImport { get; init; }

    [JsonPropertyName("meta")]
    public Dictionary<string, string> Meta { get; init; } = new Dictionary<string, string>();

    [JsonPropertyName("name")]
    public required string Name { get; init; }
}
