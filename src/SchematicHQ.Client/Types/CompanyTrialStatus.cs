using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(CompanyTrialStatus.CompanyTrialStatusSerializer))]
[Serializable]
public readonly record struct CompanyTrialStatus : IStringEnum
{
    public static readonly CompanyTrialStatus Active = new(Values.Active);

    public static readonly CompanyTrialStatus Canceled = new(Values.Canceled);

    public static readonly CompanyTrialStatus Converted = new(Values.Converted);

    public static readonly CompanyTrialStatus Expired = new(Values.Expired);

    public CompanyTrialStatus(string value)
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
    public static CompanyTrialStatus FromCustom(string value)
    {
        return new CompanyTrialStatus(value);
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

    public static bool operator ==(CompanyTrialStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CompanyTrialStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(CompanyTrialStatus value) => value.Value;

    public static explicit operator CompanyTrialStatus(string value) => new(value);

    internal class CompanyTrialStatusSerializer : JsonConverter<CompanyTrialStatus>
    {
        public override CompanyTrialStatus Read(
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
            return new CompanyTrialStatus(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            CompanyTrialStatus value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override CompanyTrialStatus ReadAsPropertyName(
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
            return new CompanyTrialStatus(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            CompanyTrialStatus value,
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
        public const string Active = "active";

        public const string Canceled = "canceled";

        public const string Converted = "converted";

        public const string Expired = "expired";
    }
}
