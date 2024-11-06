using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CrmProductResponseData
{
    [JsonPropertyName("account_id")]
    public required string AccountId { get; set; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    [JsonPropertyName("currency")]
    public required string Currency { get; set; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; set; }

    [JsonPropertyName("external_id")]
    public required string ExternalId { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("price")]
    public required string Price { get; set; }

    [JsonPropertyName("product_id")]
    public required string ProductId { get; set; }

    [JsonPropertyName("quantity")]
    public required double Quantity { get; set; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; set; }
}
