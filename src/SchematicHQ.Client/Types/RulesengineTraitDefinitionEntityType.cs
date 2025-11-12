using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<RulesengineTraitDefinitionEntityType>))]
[Serializable]
public readonly record struct RulesengineTraitDefinitionEntityType : IStringEnum
{
    public static readonly RulesengineTraitDefinitionEntityType User = new(Values.User);

    public static readonly RulesengineTraitDefinitionEntityType Company = new(Values.Company);

    public RulesengineTraitDefinitionEntityType(string value)
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
    public static RulesengineTraitDefinitionEntityType FromCustom(string value)
    {
        return new RulesengineTraitDefinitionEntityType(value);
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

    public static bool operator ==(RulesengineTraitDefinitionEntityType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(RulesengineTraitDefinitionEntityType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(RulesengineTraitDefinitionEntityType value) =>
        value.Value;

    public static explicit operator RulesengineTraitDefinitionEntityType(string value) =>
        new(value);

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
