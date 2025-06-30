using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CompanyCrmDealsResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

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

    [JsonIgnore]
    public ReadOnlyAdditionalProperties AdditionalProperties { get; private set; } = new();

    void IJsonOnDeserialized.OnDeserialized() =>
        AdditionalProperties.CopyFromExtensionData(_extensionData);

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
