using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(
    typeof(RulesengineCreditSpendPolicyScope.RulesengineCreditSpendPolicyScopeSerializer)
)]
[Serializable]
public readonly record struct RulesengineCreditSpendPolicyScope : IStringEnum
{
    public static readonly RulesengineCreditSpendPolicyScope Company = new(Values.Company);

    public static readonly RulesengineCreditSpendPolicyScope User = new(Values.User);

    public static readonly RulesengineCreditSpendPolicyScope Group = new(Values.Group);

    public RulesengineCreditSpendPolicyScope(string value)
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
    public static RulesengineCreditSpendPolicyScope FromCustom(string value)
    {
        return new RulesengineCreditSpendPolicyScope(value);
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

    public static bool operator ==(RulesengineCreditSpendPolicyScope value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(RulesengineCreditSpendPolicyScope value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(RulesengineCreditSpendPolicyScope value) => value.Value;

    public static explicit operator RulesengineCreditSpendPolicyScope(string value) => new(value);

    internal class RulesengineCreditSpendPolicyScopeSerializer
        : JsonConverter<RulesengineCreditSpendPolicyScope>
    {
        public override RulesengineCreditSpendPolicyScope Read(
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
            return new RulesengineCreditSpendPolicyScope(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            RulesengineCreditSpendPolicyScope value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override RulesengineCreditSpendPolicyScope ReadAsPropertyName(
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
            return new RulesengineCreditSpendPolicyScope(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            RulesengineCreditSpendPolicyScope value,
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
        public const string Company = "company";

        public const string User = "user";

        public const string Group = "group";
    }
}
