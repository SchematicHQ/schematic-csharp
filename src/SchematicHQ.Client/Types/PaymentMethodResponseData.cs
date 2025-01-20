using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record PaymentMethodResponseData
{
    [JsonPropertyName("account_last4")]
    public string? AccountLast4 { get; init; }

    [JsonPropertyName("account_name")]
    public string? AccountName { get; init; }

    [JsonPropertyName("bank_name")]
    public string? BankName { get; init; }

    [JsonPropertyName("billing_email")]
    public string? BillingEmail { get; init; }

    [JsonPropertyName("billing_name")]
    public string? BillingName { get; init; }

    [JsonPropertyName("card_brand")]
    public string? CardBrand { get; init; }

    [JsonPropertyName("card_exp_month")]
    public int? CardExpMonth { get; init; }

    [JsonPropertyName("card_exp_year")]
    public int? CardExpYear { get; init; }

    [JsonPropertyName("card_last4")]
    public string? CardLast4 { get; init; }

    [JsonPropertyName("company_id")]
    public string? CompanyId { get; init; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; init; }

    [JsonPropertyName("customer_external_id")]
    public required string CustomerExternalId { get; init; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; init; }

    [JsonPropertyName("external_id")]
    public required string ExternalId { get; init; }

    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("payment_method_type")]
    public required string PaymentMethodType { get; init; }

    [JsonPropertyName("subscription_external_id")]
    public string? SubscriptionExternalId { get; init; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; init; }
}
