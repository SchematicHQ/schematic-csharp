using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CrmDealResponseData
{
    [JsonPropertyName("account_id")]
    public required string AccountId { get; init; }

    [JsonPropertyName("arr")]
    public required string Arr { get; init; }

    [JsonPropertyName("company_external_id")]
    public string? CompanyExternalId { get; init; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; init; }

    [JsonPropertyName("deal_external_id")]
    public required string DealExternalId { get; init; }

    [JsonPropertyName("deal_id")]
    public required string DealId { get; init; }

    [JsonPropertyName("deleted_at")]
    public DateTime? DeletedAt { get; init; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; init; }

    [JsonPropertyName("mrr")]
    public required string Mrr { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("product_external_id")]
    public string? ProductExternalId { get; init; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; init; }
}
