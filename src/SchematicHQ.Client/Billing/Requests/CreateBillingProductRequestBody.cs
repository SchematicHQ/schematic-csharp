using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CreateBillingProductRequestBody
{
    [JsonPropertyName("currency")]
    public required string Currency { get; init; }

    [JsonPropertyName("external_id")]
    public required string ExternalId { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("price")]
    public required double Price { get; init; }

    [JsonPropertyName("quantity")]
    public required int Quantity { get; init; }
}
