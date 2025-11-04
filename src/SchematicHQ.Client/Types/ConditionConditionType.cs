using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<ConditionConditionType>))]
[Serializable]
public readonly record struct ConditionConditionType : IStringEnum
{
    public static readonly ConditionConditionType BasePlan = new(Values.BasePlan);

    public static readonly ConditionConditionType BillingProduct = new(Values.BillingProduct);

    public static readonly ConditionConditionType Company = new(Values.Company);

    public static readonly ConditionConditionType Credit = new(Values.Credit);

    public static readonly ConditionConditionType CrmProduct = new(Values.CrmProduct);

    public static readonly ConditionConditionType Metric = new(Values.Metric);

    public static readonly ConditionConditionType Plan = new(Values.Plan);

    public static readonly ConditionConditionType Trait = new(Values.Trait);

    public static readonly ConditionConditionType User = new(Values.User);

    public ConditionConditionType(string value)
    {
        Value = value;
    }

    /// <summary>
    /// The string value of the enum.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Create a string enum with the given value.
    /// </summary>
    public static ConditionConditionType FromCustom(string value)
    {
        return new ConditionConditionType(value);
    }

    public bool Equals(string? other)
    {
        return Value.Equals(other);
    }

    /// <summary>
    /// Returns the string value of the enum.
    /// </summary>
    public override string ToString()
    {
        return Value;
    }

    public static bool operator ==(ConditionConditionType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ConditionConditionType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ConditionConditionType value) => value.Value;

    public static explicit operator ConditionConditionType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string BasePlan = "base_plan";

        public const string BillingProduct = "billing_product";

        public const string Company = "company";

        public const string Credit = "credit";

        public const string CrmProduct = "crm_product";

        public const string Metric = "metric";

        public const string Plan = "plan";

        public const string Trait = "trait";

        public const string User = "user";
    }
}
