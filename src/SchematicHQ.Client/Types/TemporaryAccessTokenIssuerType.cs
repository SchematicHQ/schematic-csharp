using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(TemporaryAccessTokenIssuerType.TemporaryAccessTokenIssuerTypeSerializer))]
[Serializable]
public readonly record struct TemporaryAccessTokenIssuerType : IStringEnum
{
    public static readonly TemporaryAccessTokenIssuerType ApiKey = new(Values.ApiKey);

    public static readonly TemporaryAccessTokenIssuerType PricingRoadmap = new(
        Values.PricingRoadmap
    );

    public TemporaryAccessTokenIssuerType(string value)
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
    public static TemporaryAccessTokenIssuerType FromCustom(string value)
    {
        return new TemporaryAccessTokenIssuerType(value);
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

    public static bool operator ==(TemporaryAccessTokenIssuerType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(TemporaryAccessTokenIssuerType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(TemporaryAccessTokenIssuerType value) => value.Value;

    public static explicit operator TemporaryAccessTokenIssuerType(string value) => new(value);

    internal class TemporaryAccessTokenIssuerTypeSerializer
        : JsonConverter<TemporaryAccessTokenIssuerType>
    {
        public override TemporaryAccessTokenIssuerType Read(
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
            return new TemporaryAccessTokenIssuerType(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            TemporaryAccessTokenIssuerType value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override TemporaryAccessTokenIssuerType ReadAsPropertyName(
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
            return new TemporaryAccessTokenIssuerType(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            TemporaryAccessTokenIssuerType value,
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
        public const string ApiKey = "api_key";

        public const string PricingRoadmap = "pricing_roadmap";
    }
}
