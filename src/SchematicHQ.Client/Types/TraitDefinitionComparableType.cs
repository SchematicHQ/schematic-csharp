using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<TraitDefinitionComparableType>))]
[Serializable]
public readonly record struct TraitDefinitionComparableType : IStringEnum
{
    public static readonly TraitDefinitionComparableType Bool = new(Values.Bool);

    public static readonly TraitDefinitionComparableType Date = new(Values.Date);

    public static readonly TraitDefinitionComparableType Int = new(Values.Int);

    public static readonly TraitDefinitionComparableType String = new(Values.String);

    public TraitDefinitionComparableType(string value)
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
    public static TraitDefinitionComparableType FromCustom(string value)
    {
        return new TraitDefinitionComparableType(value);
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

    public static bool operator ==(TraitDefinitionComparableType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(TraitDefinitionComparableType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(TraitDefinitionComparableType value) => value.Value;

    public static explicit operator TraitDefinitionComparableType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Bool = "bool";

        public const string Date = "date";

        public const string Int = "int";

        public const string String = "string";
    }
}
