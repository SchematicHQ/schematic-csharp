using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CrmLineItemResponseData
{
    [JsonPropertyName("account_id")]
    public required string AccountId { get; init; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; init; }

    [JsonPropertyName("deal_id")]
    public string? DealId { get; init; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; init; }

    [JsonPropertyName("product_external_id")]
    public string? ProductExternalId { get; init; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; init; }
}
