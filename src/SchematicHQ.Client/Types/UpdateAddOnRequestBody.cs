using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record UpdateAddOnRequestBody
{
    [JsonPropertyName("add_on_id")]
    public required string AddOnId { get; init; }

    [JsonPropertyName("price_id")]
    public required string PriceId { get; init; }
}
