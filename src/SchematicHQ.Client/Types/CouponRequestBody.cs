using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record CouponRequestBody
{
    [JsonPropertyName("amount_off")]
    public required int AmountOff { get; set; }

    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    [JsonPropertyName("duration")]
    public required string Duration { get; set; }

    [JsonPropertyName("duration_in_months")]
    public required int DurationInMonths { get; set; }

    [JsonPropertyName("max_redemptions")]
    public required int MaxRedemptions { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("percent_off")]
    public required double PercentOff { get; set; }

    [JsonPropertyName("times_redeemed")]
    public required int TimesRedeemed { get; set; }

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
