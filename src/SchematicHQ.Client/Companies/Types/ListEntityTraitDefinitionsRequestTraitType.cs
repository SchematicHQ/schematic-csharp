using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<ListEntityTraitDefinitionsRequestTraitType>))]
[Serializable]
public readonly record struct ListEntityTraitDefinitionsRequestTraitType : IStringEnum
{
    public static readonly ListEntityTraitDefinitionsRequestTraitType Boolean = new(Values.Boolean);

    public static readonly ListEntityTraitDefinitionsRequestTraitType Currency = new(
        Values.Currency
    );

    public static readonly ListEntityTraitDefinitionsRequestTraitType Date = new(Values.Date);

    public static readonly ListEntityTraitDefinitionsRequestTraitType Number = new(Values.Number);

    public static readonly ListEntityTraitDefinitionsRequestTraitType String = new(Values.String);

    public static readonly ListEntityTraitDefinitionsRequestTraitType Url = new(Values.Url);

    public ListEntityTraitDefinitionsRequestTraitType(string value)
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
    public static ListEntityTraitDefinitionsRequestTraitType FromCustom(string value)
    {
        return new ListEntityTraitDefinitionsRequestTraitType(value);
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
        ListEntityTraitDefinitionsRequestTraitType value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        ListEntityTraitDefinitionsRequestTraitType value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(ListEntityTraitDefinitionsRequestTraitType value) =>
        value.Value;

    public static explicit operator ListEntityTraitDefinitionsRequestTraitType(string value) =>
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
