using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreditUsageAggregation : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Credit usage within the current billing period
    /// </summary>
    [JsonPropertyName("usage_this_billing_period")]
    public double? UsageThisBillingPeriod { get; set; }

    /// <summary>
    /// Credit usage within the current calendar month
    /// </summary>
    [JsonPropertyName("usage_this_calendar_month")]
    public double? UsageThisCalendarMonth { get; set; }

    /// <summary>
    /// Credit usage within the current calendar week (Monday to Sunday)
    /// </summary>
    [JsonPropertyName("usage_this_week")]
    public double? UsageThisWeek { get; set; }

    /// <summary>
    /// Credit usage for the current day
    /// </summary>
    [JsonPropertyName("usage_today")]
    public double? UsageToday { get; set; }

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
