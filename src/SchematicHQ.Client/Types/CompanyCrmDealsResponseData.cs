using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CompanyCrmDealsResponseData
{
    [JsonPropertyName("deal_arr")]
    public required string DealArr { get; set; }

    [JsonPropertyName("deal_external_id")]
    public required string DealExternalId { get; set; }

    [JsonPropertyName("deal_mrr")]
    public required string DealMrr { get; set; }

    [JsonPropertyName("deal_name")]
    public string? DealName { get; set; }

    [JsonPropertyName("line_items")]
    public IEnumerable<CrmDealLineItem> LineItems { get; set; } = new List<CrmDealLineItem>();
}
