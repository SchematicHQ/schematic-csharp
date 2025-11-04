using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<TraitDefinitionEntityType>))]
[Serializable]
public readonly record struct TraitDefinitionEntityType : IStringEnum
{
    public static readonly TraitDefinitionEntityType User = new(Values.User);

    public static readonly TraitDefinitionEntityType Company = new(Values.Company);

    public TraitDefinitionEntityType(string value)
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
    public static TraitDefinitionEntityType FromCustom(string value)
    {
        return new TraitDefinitionEntityType(value);
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

    public static bool operator ==(TraitDefinitionEntityType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(TraitDefinitionEntityType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(TraitDefinitionEntityType value) => value.Value;

    public static explicit operator TraitDefinitionEntityType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string User = "user";

        public const string Company = "company";
    }
}
