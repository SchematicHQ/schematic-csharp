using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(
    typeof(StringEnumSerializer<ListEntityTraitDefinitionsResponseParamsTraitTypesItem>)
)]
[Serializable]
public readonly record struct ListEntityTraitDefinitionsResponseParamsTraitTypesItem : IStringEnum
{
    public static readonly ListEntityTraitDefinitionsResponseParamsTraitTypesItem Boolean = new(
        Values.Boolean
    );

    public static readonly ListEntityTraitDefinitionsResponseParamsTraitTypesItem Currency = new(
        Values.Currency
    );

    public static readonly ListEntityTraitDefinitionsResponseParamsTraitTypesItem Date = new(
        Values.Date
    );

    public static readonly ListEntityTraitDefinitionsResponseParamsTraitTypesItem Number = new(
        Values.Number
    );

    public static readonly ListEntityTraitDefinitionsResponseParamsTraitTypesItem String = new(
        Values.String
    );

    public static readonly ListEntityTraitDefinitionsResponseParamsTraitTypesItem Url = new(
        Values.Url
    );

    public ListEntityTraitDefinitionsResponseParamsTraitTypesItem(string value)
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
    public static ListEntityTraitDefinitionsResponseParamsTraitTypesItem FromCustom(string value)
    {
        return new ListEntityTraitDefinitionsResponseParamsTraitTypesItem(value);
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
        ListEntityTraitDefinitionsResponseParamsTraitTypesItem value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        ListEntityTraitDefinitionsResponseParamsTraitTypesItem value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(
        ListEntityTraitDefinitionsResponseParamsTraitTypesItem value
    ) => value.Value;

    public static explicit operator ListEntityTraitDefinitionsResponseParamsTraitTypesItem(
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
