using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record DeleteProductPriceRequest
{
    /// <summary>
    /// billing_id
    /// </summary>
    [JsonIgnore]
    public required string BillingId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
