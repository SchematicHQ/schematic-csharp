using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<CountEntityTraitDefinitionsRequestTraitType>))]
[Serializable]
public readonly record struct CountEntityTraitDefinitionsRequestTraitType : IStringEnum
{
    public static readonly CountEntityTraitDefinitionsRequestTraitType Boolean = new(
        Values.Boolean
    );

    public static readonly CountEntityTraitDefinitionsRequestTraitType Currency = new(
        Values.Currency
    );

    public static readonly CountEntityTraitDefinitionsRequestTraitType Date = new(Values.Date);

    public static readonly CountEntityTraitDefinitionsRequestTraitType Number = new(Values.Number);

    public static readonly CountEntityTraitDefinitionsRequestTraitType String = new(Values.String);

    public static readonly CountEntityTraitDefinitionsRequestTraitType Url = new(Values.Url);

    public CountEntityTraitDefinitionsRequestTraitType(string value)
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
    public static CountEntityTraitDefinitionsRequestTraitType FromCustom(string value)
    {
        return new CountEntityTraitDefinitionsRequestTraitType(value);
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
        CountEntityTraitDefinitionsRequestTraitType value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        CountEntityTraitDefinitionsRequestTraitType value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(CountEntityTraitDefinitionsRequestTraitType value) =>
        value.Value;

    public static explicit operator CountEntityTraitDefinitionsRequestTraitType(string value) =>
        new(value);

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
