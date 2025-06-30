using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<CountEntityTraitDefinitionsResponseParamsTraitType>))]
[Serializable]
public readonly record struct CountEntityTraitDefinitionsResponseParamsTraitType : IStringEnum
{
    public static readonly CountEntityTraitDefinitionsResponseParamsTraitType Boolean = new(
        Values.Boolean
    );

    public static readonly CountEntityTraitDefinitionsResponseParamsTraitType Currency = new(
        Values.Currency
    );

    public static readonly CountEntityTraitDefinitionsResponseParamsTraitType Date = new(
        Values.Date
    );

    public static readonly CountEntityTraitDefinitionsResponseParamsTraitType Number = new(
        Values.Number
    );

    public static readonly CountEntityTraitDefinitionsResponseParamsTraitType String = new(
        Values.String
    );

    public static readonly CountEntityTraitDefinitionsResponseParamsTraitType Url = new(Values.Url);

    public CountEntityTraitDefinitionsResponseParamsTraitType(string value)
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
    public static CountEntityTraitDefinitionsResponseParamsTraitType FromCustom(string value)
    {
        return new CountEntityTraitDefinitionsResponseParamsTraitType(value);
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
        CountEntityTraitDefinitionsResponseParamsTraitType value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        CountEntityTraitDefinitionsResponseParamsTraitType value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(
        CountEntityTraitDefinitionsResponseParamsTraitType value
    ) => value.Value;

    public static explicit operator CountEntityTraitDefinitionsResponseParamsTraitType(
        string value
    ) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Boolean = "boolean";

        public const string Currency = "currency";

        public const string Date = "date";

        public const string Number = "number";

        public const string String = "string";

        public const string Url = "url";
    }
}
