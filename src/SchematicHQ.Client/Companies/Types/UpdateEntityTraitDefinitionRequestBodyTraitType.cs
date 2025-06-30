using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<UpdateEntityTraitDefinitionRequestBodyTraitType>))]
[Serializable]
public readonly record struct UpdateEntityTraitDefinitionRequestBodyTraitType : IStringEnum
{
    public static readonly UpdateEntityTraitDefinitionRequestBodyTraitType Boolean = new(
        Values.Boolean
    );

    public static readonly UpdateEntityTraitDefinitionRequestBodyTraitType Currency = new(
        Values.Currency
    );

    public static readonly UpdateEntityTraitDefinitionRequestBodyTraitType Date = new(Values.Date);

    public static readonly UpdateEntityTraitDefinitionRequestBodyTraitType Number = new(
        Values.Number
    );

    public static readonly UpdateEntityTraitDefinitionRequestBodyTraitType String = new(
        Values.String
    );

    public static readonly UpdateEntityTraitDefinitionRequestBodyTraitType Url = new(Values.Url);

    public UpdateEntityTraitDefinitionRequestBodyTraitType(string value)
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
    public static UpdateEntityTraitDefinitionRequestBodyTraitType FromCustom(string value)
    {
        return new UpdateEntityTraitDefinitionRequestBodyTraitType(value);
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
        UpdateEntityTraitDefinitionRequestBodyTraitType value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        UpdateEntityTraitDefinitionRequestBodyTraitType value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(UpdateEntityTraitDefinitionRequestBodyTraitType value) =>
        value.Value;

    public static explicit operator UpdateEntityTraitDefinitionRequestBodyTraitType(string value) =>
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
