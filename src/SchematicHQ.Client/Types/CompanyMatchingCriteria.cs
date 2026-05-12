using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(CompanyMatchingCriteria.CompanyMatchingCriteriaSerializer))]
[Serializable]
public readonly record struct CompanyMatchingCriteria : IStringEnum
{
    public static readonly CompanyMatchingCriteria BillingMetaObject = new(
        Values.BillingMetaObject
    );

    public static readonly CompanyMatchingCriteria ManualUpsert = new(Values.ManualUpsert);

    public CompanyMatchingCriteria(string value)
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
    public static CompanyMatchingCriteria FromCustom(string value)
    {
        return new CompanyMatchingCriteria(value);
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

    public static bool operator ==(CompanyMatchingCriteria value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CompanyMatchingCriteria value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(CompanyMatchingCriteria value) => value.Value;

    public static explicit operator CompanyMatchingCriteria(string value) => new(value);

    internal class CompanyMatchingCriteriaSerializer : JsonConverter<CompanyMatchingCriteria>
    {
        public override CompanyMatchingCriteria Read(
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
            return new CompanyMatchingCriteria(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            CompanyMatchingCriteria value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override CompanyMatchingCriteria ReadAsPropertyName(
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
            return new CompanyMatchingCriteria(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            CompanyMatchingCriteria value,
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
        public const string BillingMetaObject = "billing_meta_object";

        public const string ManualUpsert = "manual_upsert";
    }
}
