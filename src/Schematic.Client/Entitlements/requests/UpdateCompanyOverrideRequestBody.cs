using Schematic.Client;

namespace Schematic.Client;

public class UpdateCompanyOverrideRequestBody
{
    public UpdateCompanyOverrideRequestBodyMetricPeriod? MetricPeriod { get; init; }

    public bool? ValueBool { get; init; }

    public int? ValueNumeric { get; init; }

    public string? ValueTraitId { get; init; }

    public UpdateCompanyOverrideRequestBodyValueType ValueType { get; init; }
}
