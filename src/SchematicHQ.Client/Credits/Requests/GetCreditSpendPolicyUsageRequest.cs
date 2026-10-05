using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record GetCreditSpendPolicyUsageRequest
{
    [JsonIgnore]
    public string? BillingCreditId { get; set; }

    [JsonIgnore]
    public required string CompanyId { get; set; }

    [JsonIgnore]
    public IEnumerable<string> UserIds { get; set; } = new List<string>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
