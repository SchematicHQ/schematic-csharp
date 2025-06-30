using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<ListEntityTraitDefinitionsResponseParamsEntityType>))]
[Serializable]
public readonly record struct ListEntityTraitDefinitionsResponseParamsEntityType : IStringEnum
{
    public static readonly ListEntityTraitDefinitionsResponseParamsEntityType Company = new(
        Values.Company
    );

    public static readonly ListEntityTraitDefinitionsResponseParamsEntityType User = new(
        Values.User
    );

    public ListEntityTraitDefinitionsResponseParamsEntityType(string value)
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
    public static ListEntityTraitDefinitionsResponseParamsEntityType FromCustom(string value)
    {
        return new ListEntityTraitDefinitionsResponseParamsEntityType(value);
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
        ListEntityTraitDefinitionsResponseParamsEntityType value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        ListEntityTraitDefinitionsResponseParamsEntityType value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(
        ListEntityTraitDefinitionsResponseParamsEntityType value
    ) => value.Value;

    public static explicit operator ListEntityTraitDefinitionsResponseParamsEntityType(
        string value
    ) => new(value);

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
