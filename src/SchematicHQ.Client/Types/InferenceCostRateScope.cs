using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(InferenceCostRateScope.InferenceCostRateScopeSerializer))]
[Serializable]
public readonly record struct InferenceCostRateScope : IStringEnum
{
    public static readonly InferenceCostRateScope Account = new(Values.Account);

    public static readonly InferenceCostRateScope ListPrice = new(Values.ListPrice);

    public InferenceCostRateScope(string value)
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
    public static InferenceCostRateScope FromCustom(string value)
    {
        return new InferenceCostRateScope(value);
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

    public static bool operator ==(InferenceCostRateScope value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(InferenceCostRateScope value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(InferenceCostRateScope value) => value.Value;

    public static explicit operator InferenceCostRateScope(string value) => new(value);

    internal class InferenceCostRateScopeSerializer : JsonConverter<InferenceCostRateScope>
    {
        public override InferenceCostRateScope Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var stringValue =
                reader.GetString()
                ?? throw new global::System.Exception(
                    "The JSON value could not be read as a string."
                );
            return new InferenceCostRateScope(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            InferenceCostRateScope value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override InferenceCostRateScope ReadAsPropertyName(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var stringValue =
                reader.GetString()
                ?? throw new global::System.Exception(
                    "The JSON property name could not be read as a string."
                );
            return new InferenceCostRateScope(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            InferenceCostRateScope value,
            JsonSerializerOptions options
        )
        {
            writer.WritePropertyName(value.Value);
        }
    }

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Account = "account";

        public const string ListPrice = "list_price";
    }
}
