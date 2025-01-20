using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record UpdatePayInAdvanceRequestBody
{
    [JsonPropertyName("price_id")]
    public required string PriceId { get; init; }

    [JsonPropertyName("quantity")]
    public required int Quantity { get; init; }
}
