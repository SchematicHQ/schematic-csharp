using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public class CreateCrmDealRequestBody
{
    [JsonPropertyName("arr")]
    public string? Arr { get; init; }

    [JsonPropertyName("crm_company_id")]
    public string? CrmCompanyId { get; init; }

    [JsonPropertyName("crm_company_key")]
    public string CrmCompanyKey { get; init; }

    [JsonPropertyName("crm_product_id")]
    public string? CrmProductId { get; init; }

    [JsonPropertyName("crm_type")]
    public string CrmType { get; init; }

    [JsonPropertyName("deal_external_id")]
    public string DealExternalId { get; init; }

    [JsonPropertyName("deal_name")]
    public string? DealName { get; init; }

    [JsonPropertyName("deal_stage")]
    public string? DealStage { get; init; }

    [JsonPropertyName("mrr")]
    public string? Mrr { get; init; }
}
