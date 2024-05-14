using Schematic.Client;

namespace Schematic.Client;

public class CreateCompanyOverrideRequestBody
{
    public string CompanyId { get; init; }

    public string FeatureId { get; init; }

    public CreateCompanyOverrideRequestBodyMetricPeriod? MetricPeriod { get; init; }

    public bool? ValueBool { get; init; }

    public int? ValueNumeric { get; init; }

    public string? ValueTraitId { get; init; }

    public CreateCompanyOverrideRequestBodyValueType ValueType { get; init; }
}
