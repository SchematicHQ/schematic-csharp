using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(
    typeof(CompanyFeatureUsageExportMetadataSortOrderDirection.CompanyFeatureUsageExportMetadataSortOrderDirectionSerializer)
)]
[Serializable]
public readonly record struct CompanyFeatureUsageExportMetadataSortOrderDirection : IStringEnum
{
    public static readonly CompanyFeatureUsageExportMetadataSortOrderDirection Asc = new(
        Values.Asc
    );

    public static readonly CompanyFeatureUsageExportMetadataSortOrderDirection Desc = new(
        Values.Desc
    );

    public CompanyFeatureUsageExportMetadataSortOrderDirection(string value)
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
    public static CompanyFeatureUsageExportMetadataSortOrderDirection FromCustom(string value)
    {
        return new CompanyFeatureUsageExportMetadataSortOrderDirection(value);
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
        CompanyFeatureUsageExportMetadataSortOrderDirection value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        CompanyFeatureUsageExportMetadataSortOrderDirection value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(
        CompanyFeatureUsageExportMetadataSortOrderDirection value
    ) => value.Value;

    public static explicit operator CompanyFeatureUsageExportMetadataSortOrderDirection(
        string value
    ) => new(value);

    internal class CompanyFeatureUsageExportMetadataSortOrderDirectionSerializer
        : JsonConverter<CompanyFeatureUsageExportMetadataSortOrderDirection>
    {
        public override CompanyFeatureUsageExportMetadataSortOrderDirection Read(
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
            return new CompanyFeatureUsageExportMetadataSortOrderDirection(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            CompanyFeatureUsageExportMetadataSortOrderDirection value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override CompanyFeatureUsageExportMetadataSortOrderDirection ReadAsPropertyName(
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
            return new CompanyFeatureUsageExportMetadataSortOrderDirection(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            CompanyFeatureUsageExportMetadataSortOrderDirection value,
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
        public const string Asc = "asc";

        public const string Desc = "desc";
    }
}
