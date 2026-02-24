using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record PublicPlansResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("active_add_ons")]
    public IEnumerable<PlanViewPublicResponseData> ActiveAddOns { get; set; } =
        new List<PlanViewPublicResponseData>();

    [JsonPropertyName("active_plans")]
    public IEnumerable<PlanViewPublicResponseData> ActivePlans { get; set; } =
        new List<PlanViewPublicResponseData>();

    [JsonPropertyName("add_on_compatibilities")]
    public IEnumerable<CompatiblePlans> AddOnCompatibilities { get; set; } =
        new List<CompatiblePlans>();

    [JsonPropertyName("capabilities")]
    public ComponentCapabilities? Capabilities { get; set; }

    [JsonPropertyName("display_settings")]
    public required ComponentDisplaySettings DisplaySettings { get; set; }

    [JsonPropertyName("show_as_monthly_prices")]
    public required bool ShowAsMonthlyPrices { get; set; }

    [JsonPropertyName("show_credits")]
    public required bool ShowCredits { get; set; }

    [JsonPropertyName("show_period_toggle")]
    public required bool ShowPeriodToggle { get; set; }

    [JsonPropertyName("show_zero_price_as_free")]
    public required bool ShowZeroPriceAsFree { get; set; }

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
