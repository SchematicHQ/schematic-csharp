using System.Text.Json.Serialization;

namespace Schematic.Client;

public class BillingProductResponseData
{
    [JsonPropertyName("account_id")]
    public string AccountId { get; init; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; init; }

    [JsonPropertyName("deleted_at")]
    public DateTime? DeletedAt { get; init; }

    [JsonPropertyName("environment_id")]
    public string EnvironmentId { get; init; }

    [JsonPropertyName("external_id")]
    public string ExternalId { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("price")]
    public double Price { get; init; }

    [JsonPropertyName("product_id")]
    public string ProductId { get; init; }

    [JsonPropertyName("quantity")]
    public double Quantity { get; init; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; init; }
}
