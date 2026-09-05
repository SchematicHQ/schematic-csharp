using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CountCreditSpendPoliciesRequest
{
    [JsonIgnore]
    public string? BillingCreditId { get; set; }

    [JsonIgnore]
    public string? CompanyId { get; set; }

    [JsonIgnore]
    public CreditSpendPolicyScope? ScopeType { get; set; }

    [JsonIgnore]
    public string? UserId { get; set; }

    [JsonIgnore]
    public IEnumerable<string> UserIds { get; set; } = new List<string>();

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    [JsonIgnore]
    public long? Limit { get; set; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    [JsonIgnore]
    public long? Offset { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
