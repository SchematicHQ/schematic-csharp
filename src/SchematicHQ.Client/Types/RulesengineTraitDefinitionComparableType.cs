using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<RulesengineTraitDefinitionComparableType>))]
[Serializable]
public readonly record struct RulesengineTraitDefinitionComparableType : IStringEnum
{
    public static readonly RulesengineTraitDefinitionComparableType Bool = new(Values.Bool);

    public static readonly RulesengineTraitDefinitionComparableType Date = new(Values.Date);

    public static readonly RulesengineTraitDefinitionComparableType Int = new(Values.Int);

    public static readonly RulesengineTraitDefinitionComparableType String = new(Values.String);

    public RulesengineTraitDefinitionComparableType(string value)
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
    public static RulesengineTraitDefinitionComparableType FromCustom(string value)
    {
        return new RulesengineTraitDefinitionComparableType(value);
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
        RulesengineTraitDefinitionComparableType value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        RulesengineTraitDefinitionComparableType value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(RulesengineTraitDefinitionComparableType value) =>
        value.Value;

    public static explicit operator RulesengineTraitDefinitionComparableType(string value) =>
        new(value);

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
