using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<ConditionOperator>))]
[Serializable]
public readonly record struct ConditionOperator : IStringEnum
{
    public static readonly ConditionOperator Eq = new(Values.Eq);

    public static readonly ConditionOperator Ne = new(Values.Ne);

    public static readonly ConditionOperator Gt = new(Values.Gt);

    public static readonly ConditionOperator Lt = new(Values.Lt);

    public static readonly ConditionOperator Gte = new(Values.Gte);

    public static readonly ConditionOperator Lte = new(Values.Lte);

    public static readonly ConditionOperator IsEmpty = new(Values.IsEmpty);

    public static readonly ConditionOperator NotEmpty = new(Values.NotEmpty);

    public ConditionOperator(string value)
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
    public static ConditionOperator FromCustom(string value)
    {
        return new ConditionOperator(value);
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

    public static bool operator ==(ConditionOperator value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ConditionOperator value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ConditionOperator value) => value.Value;

    public static explicit operator ConditionOperator(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Eq = "eq";

        public const string Ne = "ne";

        public const string Gt = "gt";

        public const string Lt = "lt";

        public const string Gte = "gte";

        public const string Lte = "lte";

        public const string IsEmpty = "is_empty";

        public const string NotEmpty = "not_empty";
    }
}
