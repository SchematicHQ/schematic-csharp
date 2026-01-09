using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreateBillingCustomerRequestBody
{
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; set; }

    [JsonPropertyName("default_payment_method_id")]
    public string? DefaultPaymentMethodId { get; set; }

    [JsonPropertyName("email")]
    public required string Email { get; set; }

    [JsonPropertyName("external_id")]
    public required string ExternalId { get; set; }

    [JsonPropertyName("meta")]
    public Dictionary<string, string> Meta { get; set; } = new Dictionary<string, string>();

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("provider_type")]
    public BillingProviderType? ProviderType { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
