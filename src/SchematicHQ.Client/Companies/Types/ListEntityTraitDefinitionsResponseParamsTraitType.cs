using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<ListEntityTraitDefinitionsResponseParamsTraitType>))]
[Serializable]
public readonly record struct ListEntityTraitDefinitionsResponseParamsTraitType : IStringEnum
{
    public static readonly ListEntityTraitDefinitionsResponseParamsTraitType Boolean = new(
        Values.Boolean
    );

    public static readonly ListEntityTraitDefinitionsResponseParamsTraitType Currency = new(
        Values.Currency
    );

    public static readonly ListEntityTraitDefinitionsResponseParamsTraitType Date = new(
        Values.Date
    );

    public static readonly ListEntityTraitDefinitionsResponseParamsTraitType Number = new(
        Values.Number
    );

    public static readonly ListEntityTraitDefinitionsResponseParamsTraitType String = new(
        Values.String
    );

    public static readonly ListEntityTraitDefinitionsResponseParamsTraitType Url = new(Values.Url);

    public ListEntityTraitDefinitionsResponseParamsTraitType(string value)
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
    public static ListEntityTraitDefinitionsResponseParamsTraitType FromCustom(string value)
    {
        return new ListEntityTraitDefinitionsResponseParamsTraitType(value);
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
        ListEntityTraitDefinitionsResponseParamsTraitType value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        ListEntityTraitDefinitionsResponseParamsTraitType value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(
        ListEntityTraitDefinitionsResponseParamsTraitType value
    ) => value.Value;

    public static explicit operator ListEntityTraitDefinitionsResponseParamsTraitType(
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
