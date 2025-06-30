using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

/// <summary>
/// The created resource
/// </summary>
[Serializable]
public record CrmDealResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("account_id")]
    public required string AccountId { get; set; }

    [JsonPropertyName("arr")]
    public required string Arr { get; set; }

    [JsonPropertyName("company_external_id")]
    public string? CompanyExternalId { get; set; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    [JsonPropertyName("deal_external_id")]
    public required string DealExternalId { get; set; }

    [JsonPropertyName("deal_id")]
    public required string DealId { get; set; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; set; }

    [JsonPropertyName("mrr")]
    public required string Mrr { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("product_external_id")]
    public string? ProductExternalId { get; set; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; set; }

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
