using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record UpdateAddOnRequestBody
{
    [JsonPropertyName("add_on_id")]
    public required string AddOnId { get; set; }

    [JsonPropertyName("price_id")]
    public required string PriceId { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
