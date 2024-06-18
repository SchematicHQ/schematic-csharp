using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public class CrmLineItemResponseData
{
    [JsonPropertyName("account_id")]
    public string AccountId { get; init; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; init; }

    [JsonPropertyName("deal_id")]
    public string? DealId { get; init; }

    [JsonPropertyName("deleted_at")]
    public DateTime? DeletedAt { get; init; }

    [JsonPropertyName("environment_id")]
    public string EnvironmentId { get; init; }

    [JsonPropertyName("product_external_id")]
    public string? ProductExternalId { get; init; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; init; }
}
