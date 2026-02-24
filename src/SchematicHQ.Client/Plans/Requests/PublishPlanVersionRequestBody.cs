using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record PublishPlanVersionRequestBody
{
    [JsonPropertyName("excluded_company_ids")]
    public IEnumerable<string> ExcludedCompanyIds { get; set; } = new List<string>();

    [JsonPropertyName("migration_strategy")]
    public required PlanVersionMigrationStrategy MigrationStrategy { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
