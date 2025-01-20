using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record UpdatePayInAdvanceRequestBody
{
    [JsonPropertyName("price_id")]
    public required string PriceId { get; set; }

    [JsonPropertyName("quantity")]
    public required int Quantity { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
