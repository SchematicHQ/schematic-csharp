using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CreateBillingProductRequestBody
{
    [JsonPropertyName("currency")]
    public required string Currency { get; set; }

    [JsonPropertyName("external_id")]
    public required string ExternalId { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("price")]
    public required double Price { get; set; }

    [JsonPropertyName("quantity")]
    public required int Quantity { get; set; }
}
