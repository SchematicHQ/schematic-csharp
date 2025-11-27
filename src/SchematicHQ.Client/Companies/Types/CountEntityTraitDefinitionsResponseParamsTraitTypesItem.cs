using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(
    typeof(StringEnumSerializer<CountEntityTraitDefinitionsResponseParamsTraitTypesItem>)
)]
[Serializable]
public readonly record struct CountEntityTraitDefinitionsResponseParamsTraitTypesItem : IStringEnum
{
    public static readonly CountEntityTraitDefinitionsResponseParamsTraitTypesItem Boolean = new(
        Values.Boolean
    );

    public static readonly CountEntityTraitDefinitionsResponseParamsTraitTypesItem Currency = new(
        Values.Currency
    );

    public static readonly CountEntityTraitDefinitionsResponseParamsTraitTypesItem Date = new(
        Values.Date
    );

    public static readonly CountEntityTraitDefinitionsResponseParamsTraitTypesItem Number = new(
        Values.Number
    );

    public static readonly CountEntityTraitDefinitionsResponseParamsTraitTypesItem String = new(
        Values.String
    );

    public static readonly CountEntityTraitDefinitionsResponseParamsTraitTypesItem Url = new(
        Values.Url
    );

    public CountEntityTraitDefinitionsResponseParamsTraitTypesItem(string value)
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
    public static CountEntityTraitDefinitionsResponseParamsTraitTypesItem FromCustom(string value)
    {
        return new CountEntityTraitDefinitionsResponseParamsTraitTypesItem(value);
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
        CountEntityTraitDefinitionsResponseParamsTraitTypesItem value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        CountEntityTraitDefinitionsResponseParamsTraitTypesItem value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(
        CountEntityTraitDefinitionsResponseParamsTraitTypesItem value
    ) => value.Value;

    public static explicit operator CountEntityTraitDefinitionsResponseParamsTraitTypesItem(
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
