using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreateMigrationInput
{
    [JsonPropertyName("company_ids")]
    public IEnumerable<string>? CompanyIds { get; set; }

    [JsonPropertyName("excluded_company_ids")]
    public IEnumerable<string>? ExcludedCompanyIds { get; set; }

    [JsonPropertyName("plan_id")]
    public required string PlanId { get; set; }

    [JsonPropertyName("plan_version_id_to")]
    public required string PlanVersionIdTo { get; set; }

    [JsonPropertyName("plan_version_ids_from")]
    public IEnumerable<string>? PlanVersionIdsFrom { get; set; }

    /// <summary>
    /// How Stripe handles the price difference when companies are migrated. With strategy immediate, omitted means create_prorations. With end_of_billing_period only none is accepted and means the same as omitting it: the change lands on the renewal boundary, so there is nothing to prorate. With scheduled any value is accepted and omitted means none.
    /// </summary>
    [JsonPropertyName("proration_behavior")]
    public MigrationProrationBehavior? ProrationBehavior { get; set; }

    /// <summary>
    /// When every company moves, for strategy scheduled. Must be in the future; the migration runs within about a minute of this time. Not accepted with other strategies.
    /// </summary>
    [JsonPropertyName("scheduled_at")]
    public DateTime? ScheduledAt { get; set; }

    [JsonPropertyName("strategy")]
    public required PlanVersionMigrationStrategy Strategy { get; set; }

    [JsonPropertyName("target_plan_type")]
    public required PlanType TargetPlanType { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
