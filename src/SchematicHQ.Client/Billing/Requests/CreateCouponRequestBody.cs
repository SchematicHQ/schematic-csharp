using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CreateCouponRequestBody
{
    [JsonPropertyName("amount_off")]
    public required int AmountOff { get; init; }

    [JsonPropertyName("currency")]
    public string? Currency { get; init; }

    [JsonPropertyName("duration")]
    public required string Duration { get; init; }

    [JsonPropertyName("duration_in_months")]
    public required int DurationInMonths { get; init; }

    [JsonPropertyName("external_id")]
    public required string ExternalId { get; init; }

    [JsonPropertyName("max_redemptions")]
    public required int MaxRedemptions { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("percent_off")]
    public required double PercentOff { get; init; }

    [JsonPropertyName("times_redeemed")]
    public required int TimesRedeemed { get; init; }
}
