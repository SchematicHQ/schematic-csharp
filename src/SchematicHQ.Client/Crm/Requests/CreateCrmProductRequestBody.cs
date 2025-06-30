using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreateCrmProductRequestBody
{
    [JsonPropertyName("currency")]
    public required string Currency { get; set; }

    [JsonPropertyName("description")]
    public required string Description { get; set; }

    [JsonPropertyName("external_id")]
    public required string ExternalId { get; set; }

    [JsonPropertyName("interval")]
    public required string Interval { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("price")]
    public required string Price { get; set; }

    [JsonPropertyName("quantity")]
    public required int Quantity { get; set; }

    [JsonPropertyName("sku")]
    public required string Sku { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
