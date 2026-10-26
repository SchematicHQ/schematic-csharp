using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(RenewalPolicy.RenewalPolicySerializer))]
[Serializable]
public readonly record struct RenewalPolicy : IStringEnum
{
    public static readonly RenewalPolicy AutoRenew = new(Values.AutoRenew);

    public static readonly RenewalPolicy ExpireToDefault = new(Values.ExpireToDefault);

    public static readonly RenewalPolicy HoldWithGrace = new(Values.HoldWithGrace);

    public RenewalPolicy(string value)
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
    public static RenewalPolicy FromCustom(string value)
    {
        return new RenewalPolicy(value);
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

    public static bool operator ==(RenewalPolicy value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(RenewalPolicy value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(RenewalPolicy value) => value.Value;

    public static explicit operator RenewalPolicy(string value) => new(value);

    internal class RenewalPolicySerializer : JsonConverter<RenewalPolicy>
    {
        public override RenewalPolicy Read(
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
            return new RenewalPolicy(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            RenewalPolicy value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override RenewalPolicy ReadAsPropertyName(
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
            return new RenewalPolicy(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            RenewalPolicy value,
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
        public const string AutoRenew = "auto_renew";

        public const string ExpireToDefault = "expire_to_default";

        public const string HoldWithGrace = "hold_with_grace";
    }
}
