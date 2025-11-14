using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record SoftDeleteBillingCreditRequest
{
    /// <summary>
    /// credit_id
    /// </summary>
    [JsonIgnore]
    public required string CreditId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
