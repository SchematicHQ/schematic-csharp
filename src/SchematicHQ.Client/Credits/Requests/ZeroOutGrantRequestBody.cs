using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record ZeroOutGrantRequestBody
{
    [JsonPropertyName("reason")]
    public BillingCreditGrantZeroedOutReason? Reason { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
