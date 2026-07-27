using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record UpdateCompanyBillingDetailsRequestBody
{
    [JsonPropertyName("address")]
    public CustomerBillingAddress? Address { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    [JsonPropertyName("values")]
    public IEnumerable<CheckoutFieldValue> Values { get; set; } = new List<CheckoutFieldValue>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
