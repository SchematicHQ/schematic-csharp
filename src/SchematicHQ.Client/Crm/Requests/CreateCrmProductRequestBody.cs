using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public class CreateCrmProductRequestBody
{
    [JsonPropertyName("currency")]
    public string Currency { get; init; }

    [JsonPropertyName("description")]
    public string Description { get; init; }

    [JsonPropertyName("external_id")]
    public string ExternalId { get; init; }

    [JsonPropertyName("interval")]
    public string Interval { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("price")]
    public string Price { get; init; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; init; }

    [JsonPropertyName("sku")]
    public string Sku { get; init; }
}
