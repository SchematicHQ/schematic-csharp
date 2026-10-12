using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record GetCreditBalanceTimeSeriesRequest
{
    [JsonIgnore]
    public required string BillingCreditId { get; set; }

    [JsonIgnore]
    public required string CompanyId { get; set; }

    [JsonIgnore]
    public required DateTime EndTime { get; set; }

    [JsonIgnore]
    public required DateTime StartTime { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
