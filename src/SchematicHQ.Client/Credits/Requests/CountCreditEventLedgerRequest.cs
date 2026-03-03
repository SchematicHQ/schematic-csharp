using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CountCreditEventLedgerRequest
{
    [JsonIgnore]
    public string? BillingCreditId { get; set; }

    [JsonIgnore]
    public required string CompanyId { get; set; }

    [JsonIgnore]
    public string? EndTime { get; set; }

    [JsonIgnore]
    public CreditEventType? EventType { get; set; }

    [JsonIgnore]
    public string? FeatureId { get; set; }

    [JsonIgnore]
    public string? StartTime { get; set; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    [JsonIgnore]
    public int? Limit { get; set; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    [JsonIgnore]
    public int? Offset { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
