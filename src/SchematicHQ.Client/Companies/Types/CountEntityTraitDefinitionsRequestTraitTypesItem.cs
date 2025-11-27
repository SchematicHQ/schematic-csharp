using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<CountEntityTraitDefinitionsRequestTraitTypesItem>))]
[Serializable]
public readonly record struct CountEntityTraitDefinitionsRequestTraitTypesItem : IStringEnum
{
    public static readonly CountEntityTraitDefinitionsRequestTraitTypesItem Boolean = new(
        Values.Boolean
    );

    public static readonly CountEntityTraitDefinitionsRequestTraitTypesItem Currency = new(
        Values.Currency
    );

    public static readonly CountEntityTraitDefinitionsRequestTraitTypesItem Date = new(Values.Date);

    public static readonly CountEntityTraitDefinitionsRequestTraitTypesItem Number = new(
        Values.Number
    );

    public static readonly CountEntityTraitDefinitionsRequestTraitTypesItem String = new(
        Values.String
    );

    public static readonly CountEntityTraitDefinitionsRequestTraitTypesItem Url = new(Values.Url);

    public CountEntityTraitDefinitionsRequestTraitTypesItem(string value)
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
    public static CountEntityTraitDefinitionsRequestTraitTypesItem FromCustom(string value)
    {
        return new CountEntityTraitDefinitionsRequestTraitTypesItem(value);
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
        CountEntityTraitDefinitionsRequestTraitTypesItem value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        CountEntityTraitDefinitionsRequestTraitTypesItem value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(
        CountEntityTraitDefinitionsRequestTraitTypesItem value
    ) => value.Value;

    public static explicit operator CountEntityTraitDefinitionsRequestTraitTypesItem(
        string value
    ) => new(value);

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
