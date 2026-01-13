using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record PlanBundleCreditGrantRequestBody : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("action")]
    public required PlanBundleAction Action { get; set; }

    [JsonPropertyName("create_req")]
    public CreateBillingPlanCreditGrantRequestBody? CreateReq { get; set; }

    [JsonPropertyName("credit_grant_id")]
    public string? CreditGrantId { get; set; }

    [JsonPropertyName("delete_req")]
    public DeleteBillingPlanCreditGrantRequestBody? DeleteReq { get; set; }

    [JsonPropertyName("update_req")]
    public UpdateBillingPlanCreditGrantRequestBody? UpdateReq { get; set; }

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
