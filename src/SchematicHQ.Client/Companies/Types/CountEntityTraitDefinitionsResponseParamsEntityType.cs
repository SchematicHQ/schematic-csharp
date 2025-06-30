using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<CountEntityTraitDefinitionsResponseParamsEntityType>))]
[Serializable]
public readonly record struct CountEntityTraitDefinitionsResponseParamsEntityType : IStringEnum
{
    public static readonly CountEntityTraitDefinitionsResponseParamsEntityType Company = new(
        Values.Company
    );

    public static readonly CountEntityTraitDefinitionsResponseParamsEntityType User = new(
        Values.User
    );

    public CountEntityTraitDefinitionsResponseParamsEntityType(string value)
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
    public static CountEntityTraitDefinitionsResponseParamsEntityType FromCustom(string value)
    {
        return new CountEntityTraitDefinitionsResponseParamsEntityType(value);
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
        CountEntityTraitDefinitionsResponseParamsEntityType value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        CountEntityTraitDefinitionsResponseParamsEntityType value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(
        CountEntityTraitDefinitionsResponseParamsEntityType value
    ) => value.Value;

    public static explicit operator CountEntityTraitDefinitionsResponseParamsEntityType(
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
