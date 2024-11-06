using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record PaymentMethodResponseData
{
    [JsonPropertyName("account_last4")]
    public string? AccountLast4 { get; set; }

    [JsonPropertyName("account_name")]
    public string? AccountName { get; set; }

    [JsonPropertyName("bank_name")]
    public string? BankName { get; set; }

    [JsonPropertyName("billing_email")]
    public string? BillingEmail { get; set; }

    [JsonPropertyName("billing_name")]
    public string? BillingName { get; set; }

    [JsonPropertyName("card_brand")]
    public string? CardBrand { get; set; }

    [JsonPropertyName("card_exp_month")]
    public int? CardExpMonth { get; set; }

    [JsonPropertyName("card_exp_year")]
    public int? CardExpYear { get; set; }

    [JsonPropertyName("card_last4")]
    public string? CardLast4 { get; set; }

    [JsonPropertyName("company_id")]
    public string? CompanyId { get; set; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    [JsonPropertyName("customer_external_id")]
    public required string CustomerExternalId { get; set; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; set; }

    [JsonPropertyName("external_id")]
    public required string ExternalId { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("payment_method_type")]
    public required string PaymentMethodType { get; set; }

    [JsonPropertyName("subscription_external_id")]
    public string? SubscriptionExternalId { get; set; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; set; }
}
