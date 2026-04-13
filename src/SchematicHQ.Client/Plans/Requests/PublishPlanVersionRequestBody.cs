using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record PublishPlanVersionRequestBody
{
    [JsonPropertyName("activation_strategy")]
    public CustomPlanActivationStrategy? ActivationStrategy { get; set; }

    [JsonPropertyName("customer_email")]
    public string? CustomerEmail { get; set; }

    [JsonPropertyName("days_until_due")]
    public long? DaysUntilDue { get; set; }

    [JsonPropertyName("excluded_company_ids")]
    public IEnumerable<string> ExcludedCompanyIds { get; set; } = new List<string>();

    [JsonPropertyName("migration_strategy")]
    public required PlanVersionMigrationStrategy MigrationStrategy { get; set; }

    [JsonPropertyName("pay_in_advance")]
    public IEnumerable<UpdatePayInAdvanceRequestBody> PayInAdvance { get; set; } =
        new List<UpdatePayInAdvanceRequestBody>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
