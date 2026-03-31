using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(EntitlementType.EntitlementTypeSerializer))]
[Serializable]
public readonly record struct EntitlementType : IStringEnum
{
    public static readonly EntitlementType CompanyOverride = new(Values.CompanyOverride);

    public static readonly EntitlementType PlanEntitlement = new(Values.PlanEntitlement);

    public static readonly EntitlementType Unknown = new(Values.Unknown);

    public EntitlementType(string value)
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
    public static EntitlementType FromCustom(string value)
    {
        return new EntitlementType(value);
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

    public static bool operator ==(EntitlementType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(EntitlementType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(EntitlementType value) => value.Value;

    public static explicit operator EntitlementType(string value) => new(value);

    internal class EntitlementTypeSerializer : JsonConverter<EntitlementType>
    {
        public override EntitlementType Read(
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
            return new EntitlementType(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            EntitlementType value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override EntitlementType ReadAsPropertyName(
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
            return new EntitlementType(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            EntitlementType value,
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
        public const string CompanyOverride = "company_override";

        public const string PlanEntitlement = "plan_entitlement";

        public const string Unknown = "unknown";
    }
}
