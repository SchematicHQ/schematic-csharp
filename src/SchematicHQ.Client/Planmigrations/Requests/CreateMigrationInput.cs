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

    [JsonPropertyName("proration_behavior")]
    public MigrationProrationBehavior? ProrationBehavior { get; set; }

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
