using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record ComponentSettingsResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("show_as_monthly_prices")]
    public required bool ShowAsMonthlyPrices { get; set; }

    [JsonPropertyName("show_credits")]
    public required bool ShowCredits { get; set; }

    [JsonPropertyName("show_feature_description")]
    public required bool ShowFeatureDescription { get; set; }

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
