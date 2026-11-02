using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(InferenceCostSource.InferenceCostSourceSerializer))]
[Serializable]
public readonly record struct InferenceCostSource : IStringEnum
{
    public static readonly InferenceCostSource ListPrice = new(Values.ListPrice);

    public static readonly InferenceCostSource Provided = new(Values.Provided);

    public static readonly InferenceCostSource Unpriced = new(Values.Unpriced);

    public InferenceCostSource(string value)
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
    public static InferenceCostSource FromCustom(string value)
    {
        return new InferenceCostSource(value);
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

    public static bool operator ==(InferenceCostSource value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(InferenceCostSource value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(InferenceCostSource value) => value.Value;

    public static explicit operator InferenceCostSource(string value) => new(value);

    internal class InferenceCostSourceSerializer : JsonConverter<InferenceCostSource>
    {
        public override InferenceCostSource Read(
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
            return new InferenceCostSource(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            InferenceCostSource value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override InferenceCostSource ReadAsPropertyName(
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
            return new InferenceCostSource(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            InferenceCostSource value,
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
        public const string ListPrice = "list_price";

        public const string Provided = "provided";

        public const string Unpriced = "unpriced";
    }
}
