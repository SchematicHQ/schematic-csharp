using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record ListInferenceCostRatesRequest
{
    [JsonIgnore]
    public string? Provider { get; set; }

    [JsonIgnore]
    public string? ResponseModel { get; set; }

    /// <summary>
    /// Only list rates from this sheet: list_price for Schematic's list prices, account for the environment's own rate sheet.
    /// </summary>
    [JsonIgnore]
    public InferenceCostRateScope? Scope { get; set; }

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
