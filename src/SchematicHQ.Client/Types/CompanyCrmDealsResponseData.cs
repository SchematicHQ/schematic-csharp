using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record CompanyCrmDealsResponseData
{
    [JsonPropertyName("deal_arr")]
    public required string DealArr { get; init; }

    [JsonPropertyName("deal_external_id")]
    public required string DealExternalId { get; init; }

    [JsonPropertyName("deal_mrr")]
    public required string DealMrr { get; init; }

    [JsonPropertyName("deal_name")]
    public string? DealName { get; init; }

    [JsonPropertyName("line_items")]
    public IEnumerable<CrmDealLineItem> LineItems { get; init; } = new List<CrmDealLineItem>();
}
