using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<CreateEntityTraitDefinitionRequestBodyTraitType>))]
[Serializable]
public readonly record struct CreateEntityTraitDefinitionRequestBodyTraitType : IStringEnum
{
    public static readonly CreateEntityTraitDefinitionRequestBodyTraitType Boolean = new(
        Values.Boolean
    );

    public static readonly CreateEntityTraitDefinitionRequestBodyTraitType Currency = new(
        Values.Currency
    );

    public static readonly CreateEntityTraitDefinitionRequestBodyTraitType Date = new(Values.Date);

    public static readonly CreateEntityTraitDefinitionRequestBodyTraitType Number = new(
        Values.Number
    );

    public static readonly CreateEntityTraitDefinitionRequestBodyTraitType String = new(
        Values.String
    );

    public static readonly CreateEntityTraitDefinitionRequestBodyTraitType Url = new(Values.Url);

    public CreateEntityTraitDefinitionRequestBodyTraitType(string value)
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
    public static CreateEntityTraitDefinitionRequestBodyTraitType FromCustom(string value)
    {
        return new CreateEntityTraitDefinitionRequestBodyTraitType(value);
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
        CreateEntityTraitDefinitionRequestBodyTraitType value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        CreateEntityTraitDefinitionRequestBodyTraitType value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(CreateEntityTraitDefinitionRequestBodyTraitType value) =>
        value.Value;

    public static explicit operator CreateEntityTraitDefinitionRequestBodyTraitType(string value) =>
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
