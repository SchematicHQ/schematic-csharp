using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<RulesengineConditionOperator>))]
[Serializable]
public readonly record struct RulesengineConditionOperator : IStringEnum
{
    public static readonly RulesengineConditionOperator Eq = new(Values.Eq);

    public static readonly RulesengineConditionOperator Ne = new(Values.Ne);

    public static readonly RulesengineConditionOperator Gt = new(Values.Gt);

    public static readonly RulesengineConditionOperator Lt = new(Values.Lt);

    public static readonly RulesengineConditionOperator Gte = new(Values.Gte);

    public static readonly RulesengineConditionOperator Lte = new(Values.Lte);

    public static readonly RulesengineConditionOperator IsEmpty = new(Values.IsEmpty);

    public static readonly RulesengineConditionOperator NotEmpty = new(Values.NotEmpty);

    public RulesengineConditionOperator(string value)
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
    public static RulesengineConditionOperator FromCustom(string value)
    {
        return new RulesengineConditionOperator(value);
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

    public static bool operator ==(RulesengineConditionOperator value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(RulesengineConditionOperator value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(RulesengineConditionOperator value) => value.Value;

    public static explicit operator RulesengineConditionOperator(string value) => new(value);

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
