using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<CreateEntityTraitDefinitionRequestBodyEntityType>))]
[Serializable]
public readonly record struct CreateEntityTraitDefinitionRequestBodyEntityType : IStringEnum
{
    public static readonly CreateEntityTraitDefinitionRequestBodyEntityType Company = new(
        Values.Company
    );

    public static readonly CreateEntityTraitDefinitionRequestBodyEntityType User = new(Values.User);

    public CreateEntityTraitDefinitionRequestBodyEntityType(string value)
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
    public static CreateEntityTraitDefinitionRequestBodyEntityType FromCustom(string value)
    {
        return new CreateEntityTraitDefinitionRequestBodyEntityType(value);
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
        CreateEntityTraitDefinitionRequestBodyEntityType value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        CreateEntityTraitDefinitionRequestBodyEntityType value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(
        CreateEntityTraitDefinitionRequestBodyEntityType value
    ) => value.Value;

    public static explicit operator CreateEntityTraitDefinitionRequestBodyEntityType(
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
