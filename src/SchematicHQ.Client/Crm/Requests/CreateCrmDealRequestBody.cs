using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CreateCrmDealRequestBody
{
    [JsonPropertyName("arr")]
    public string? Arr { get; set; }

    [JsonPropertyName("crm_company_id")]
    public string? CrmCompanyId { get; set; }

    [JsonPropertyName("crm_company_key")]
    public required string CrmCompanyKey { get; set; }

    [JsonPropertyName("crm_product_id")]
    public string? CrmProductId { get; set; }

    [JsonPropertyName("crm_type")]
    public required string CrmType { get; set; }

    [JsonPropertyName("deal_external_id")]
    public required string DealExternalId { get; set; }

    [JsonPropertyName("deal_name")]
    public string? DealName { get; set; }

    [JsonPropertyName("deal_stage")]
    public string? DealStage { get; set; }

    [JsonPropertyName("mrr")]
    public string? Mrr { get; set; }
}
