using SchematicHQ.Client;

namespace SchematicHQ.Client;

public class CreatePlanEntitlementRequestBody
{
    public string FeatureId { get; init; }

    public CreatePlanEntitlementRequestBodyMetricPeriod? MetricPeriod { get; init; }

    public string PlanId { get; init; }

    public bool? ValueBool { get; init; }

    public int? ValueNumeric { get; init; }

    public string? ValueTraitId { get; init; }

    public CreatePlanEntitlementRequestBodyValueType ValueType { get; init; }
}
