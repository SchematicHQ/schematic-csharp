using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(CustomPlanBillingStatus.CustomPlanBillingStatusSerializer))]
[Serializable]
public readonly record struct CustomPlanBillingStatus : IStringEnum
{
    public static readonly CustomPlanBillingStatus Active = new(Values.Active);

    public static readonly CustomPlanBillingStatus Paid = new(Values.Paid);

    public static readonly CustomPlanBillingStatus Pending = new(Values.Pending);

    public CustomPlanBillingStatus(string value)
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
    public static CustomPlanBillingStatus FromCustom(string value)
    {
        return new CustomPlanBillingStatus(value);
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

    public static bool operator ==(CustomPlanBillingStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CustomPlanBillingStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(CustomPlanBillingStatus value) => value.Value;

    public static explicit operator CustomPlanBillingStatus(string value) => new(value);

    internal class CustomPlanBillingStatusSerializer : JsonConverter<CustomPlanBillingStatus>
    {
        public override CustomPlanBillingStatus Read(
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
            return new CustomPlanBillingStatus(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            CustomPlanBillingStatus value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override CustomPlanBillingStatus ReadAsPropertyName(
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
            return new CustomPlanBillingStatus(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            CustomPlanBillingStatus value,
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

        public const string Paid = "paid";

        public const string Pending = "pending";
    }
}
