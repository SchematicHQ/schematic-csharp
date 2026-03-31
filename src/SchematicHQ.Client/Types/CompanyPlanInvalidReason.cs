using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(CompanyPlanInvalidReason.CompanyPlanInvalidReasonSerializer))]
[Serializable]
public readonly record struct CompanyPlanInvalidReason : IStringEnum
{
    public static readonly CompanyPlanInvalidReason DowngradeNotPermitted = new(
        Values.DowngradeNotPermitted
    );

    public static readonly CompanyPlanInvalidReason FeatureUsageExceeded = new(
        Values.FeatureUsageExceeded
    );

    public CompanyPlanInvalidReason(string value)
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
    public static CompanyPlanInvalidReason FromCustom(string value)
    {
        return new CompanyPlanInvalidReason(value);
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

    public static bool operator ==(CompanyPlanInvalidReason value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CompanyPlanInvalidReason value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(CompanyPlanInvalidReason value) => value.Value;

    public static explicit operator CompanyPlanInvalidReason(string value) => new(value);

    internal class CompanyPlanInvalidReasonSerializer : JsonConverter<CompanyPlanInvalidReason>
    {
        public override CompanyPlanInvalidReason Read(
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
            return new CompanyPlanInvalidReason(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            CompanyPlanInvalidReason value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override CompanyPlanInvalidReason ReadAsPropertyName(
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
            return new CompanyPlanInvalidReason(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            CompanyPlanInvalidReason value,
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
        public const string DowngradeNotPermitted = "downgrade_not_permitted";

        public const string FeatureUsageExceeded = "feature_usage_exceeded";
    }
}
