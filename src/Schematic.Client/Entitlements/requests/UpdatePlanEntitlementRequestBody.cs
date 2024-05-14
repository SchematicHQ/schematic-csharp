using Schematic.Client;

namespace Schematic.Client;

public class UpdatePlanEntitlementRequestBody
{
    public UpdatePlanEntitlementRequestBodyMetricPeriod? MetricPeriod { get; init; }

    public bool? ValueBool { get; init; }

    public int? ValueNumeric { get; init; }

    public string? ValueTraitId { get; init; }

    public UpdatePlanEntitlementRequestBodyValueType ValueType { get; init; }
}
