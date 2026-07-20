using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(DataExportType.DataExportTypeSerializer))]
[Serializable]
public readonly record struct DataExportType : IStringEnum
{
    public static readonly DataExportType AuditLog = new(Values.AuditLog);

    public static readonly DataExportType CompanyFeatureUsage = new(Values.CompanyFeatureUsage);

    public DataExportType(string value)
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
    public static DataExportType FromCustom(string value)
    {
        return new DataExportType(value);
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

    public static bool operator ==(DataExportType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(DataExportType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(DataExportType value) => value.Value;

    public static explicit operator DataExportType(string value) => new(value);

    internal class DataExportTypeSerializer : JsonConverter<DataExportType>
    {
        public override DataExportType Read(
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
            return new DataExportType(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            DataExportType value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override DataExportType ReadAsPropertyName(
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
            return new DataExportType(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            DataExportType value,
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
        public const string AuditLog = "audit-log";

        public const string CompanyFeatureUsage = "company-feature-usage";
    }
}
