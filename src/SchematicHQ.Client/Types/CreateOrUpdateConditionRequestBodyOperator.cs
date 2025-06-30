using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<CreateOrUpdateConditionRequestBodyOperator>))]
[Serializable]
public readonly record struct CreateOrUpdateConditionRequestBodyOperator : IStringEnum
{
    public static readonly CreateOrUpdateConditionRequestBodyOperator Eq = new(Values.Eq);

    public static readonly CreateOrUpdateConditionRequestBodyOperator Ne = new(Values.Ne);

    public static readonly CreateOrUpdateConditionRequestBodyOperator Gt = new(Values.Gt);

    public static readonly CreateOrUpdateConditionRequestBodyOperator Gte = new(Values.Gte);

    public static readonly CreateOrUpdateConditionRequestBodyOperator Lt = new(Values.Lt);

    public static readonly CreateOrUpdateConditionRequestBodyOperator Lte = new(Values.Lte);

    public static readonly CreateOrUpdateConditionRequestBodyOperator IsEmpty = new(Values.IsEmpty);

    public static readonly CreateOrUpdateConditionRequestBodyOperator NotEmpty = new(
        Values.NotEmpty
    );

    public CreateOrUpdateConditionRequestBodyOperator(string value)
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
    public static CreateOrUpdateConditionRequestBodyOperator FromCustom(string value)
    {
        return new CreateOrUpdateConditionRequestBodyOperator(value);
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

    public static bool operator ==(
        CreateOrUpdateConditionRequestBodyOperator value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        CreateOrUpdateConditionRequestBodyOperator value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(CreateOrUpdateConditionRequestBodyOperator value) =>
        value.Value;

    public static explicit operator CreateOrUpdateConditionRequestBodyOperator(string value) =>
        new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Eq = "eq";

        public const string Ne = "ne";

        public const string Gt = "gt";

        public const string Gte = "gte";

        public const string Lt = "lt";

        public const string Lte = "lte";

        public const string IsEmpty = "is_empty";

        public const string NotEmpty = "not_empty";
    }
}
