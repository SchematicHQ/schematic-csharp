using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public class CreateBillingProductRequestBody
{
    [JsonPropertyName("currency")]
    public string Currency { get; init; }

    [JsonPropertyName("external_id")]
    public string ExternalId { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("price")]
    public double Price { get; init; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; init; }
}
