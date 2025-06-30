using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<ListEntityTraitDefinitionsRequestEntityType>))]
[Serializable]
public readonly record struct ListEntityTraitDefinitionsRequestEntityType : IStringEnum
{
    public static readonly ListEntityTraitDefinitionsRequestEntityType Company = new(
        Values.Company
    );

    public static readonly ListEntityTraitDefinitionsRequestEntityType User = new(Values.User);

    public ListEntityTraitDefinitionsRequestEntityType(string value)
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
    public static ListEntityTraitDefinitionsRequestEntityType FromCustom(string value)
    {
        return new ListEntityTraitDefinitionsRequestEntityType(value);
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
        ListEntityTraitDefinitionsRequestEntityType value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        ListEntityTraitDefinitionsRequestEntityType value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(ListEntityTraitDefinitionsRequestEntityType value) =>
        value.Value;

    public static explicit operator ListEntityTraitDefinitionsRequestEntityType(string value) =>
        new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Company = "company";

        public const string User = "user";
    }
}
