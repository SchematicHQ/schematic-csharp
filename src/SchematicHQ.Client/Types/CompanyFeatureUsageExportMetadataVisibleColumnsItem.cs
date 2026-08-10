using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(
    typeof(CompanyFeatureUsageExportMetadataVisibleColumnsItem.CompanyFeatureUsageExportMetadataVisibleColumnsItemSerializer)
)]
[Serializable]
public readonly record struct CompanyFeatureUsageExportMetadataVisibleColumnsItem : IStringEnum
{
    public static readonly CompanyFeatureUsageExportMetadataVisibleColumnsItem Plan = new(
        Values.Plan
    );

    public static readonly CompanyFeatureUsageExportMetadataVisibleColumnsItem Subscription = new(
        Values.Subscription
    );

    public static readonly CompanyFeatureUsageExportMetadataVisibleColumnsItem Users = new(
        Values.Users
    );

    public static readonly CompanyFeatureUsageExportMetadataVisibleColumnsItem LastSeenAt = new(
        Values.LastSeenAt
    );

    public CompanyFeatureUsageExportMetadataVisibleColumnsItem(string value)
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
    public static CompanyFeatureUsageExportMetadataVisibleColumnsItem FromCustom(string value)
    {
        return new CompanyFeatureUsageExportMetadataVisibleColumnsItem(value);
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
        CompanyFeatureUsageExportMetadataVisibleColumnsItem value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        CompanyFeatureUsageExportMetadataVisibleColumnsItem value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(
        CompanyFeatureUsageExportMetadataVisibleColumnsItem value
    ) => value.Value;

    public static explicit operator CompanyFeatureUsageExportMetadataVisibleColumnsItem(
        string value
    ) => new(value);

    internal class CompanyFeatureUsageExportMetadataVisibleColumnsItemSerializer
        : JsonConverter<CompanyFeatureUsageExportMetadataVisibleColumnsItem>
    {
        public override CompanyFeatureUsageExportMetadataVisibleColumnsItem Read(
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
            return new CompanyFeatureUsageExportMetadataVisibleColumnsItem(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            CompanyFeatureUsageExportMetadataVisibleColumnsItem value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override CompanyFeatureUsageExportMetadataVisibleColumnsItem ReadAsPropertyName(
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
            return new CompanyFeatureUsageExportMetadataVisibleColumnsItem(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            CompanyFeatureUsageExportMetadataVisibleColumnsItem value,
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
        public const string Plan = "plan";

        public const string Subscription = "subscription";

        public const string Users = "users";

        public const string LastSeenAt = "last_seen_at";
    }
}
