using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(CreditSpendPolicyScope.CreditSpendPolicyScopeSerializer))]
[Serializable]
public readonly record struct CreditSpendPolicyScope : IStringEnum
{
    public static readonly CreditSpendPolicyScope Company = new(Values.Company);

    public static readonly CreditSpendPolicyScope User = new(Values.User);

    public static readonly CreditSpendPolicyScope Group = new(Values.Group);

    public CreditSpendPolicyScope(string value)
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
    public static CreditSpendPolicyScope FromCustom(string value)
    {
        return new CreditSpendPolicyScope(value);
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

    public static bool operator ==(CreditSpendPolicyScope value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CreditSpendPolicyScope value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(CreditSpendPolicyScope value) => value.Value;

    public static explicit operator CreditSpendPolicyScope(string value) => new(value);

    internal class CreditSpendPolicyScopeSerializer : JsonConverter<CreditSpendPolicyScope>
    {
        public override CreditSpendPolicyScope Read(
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
            return new CreditSpendPolicyScope(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            CreditSpendPolicyScope value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override CreditSpendPolicyScope ReadAsPropertyName(
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
            return new CreditSpendPolicyScope(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            CreditSpendPolicyScope value,
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
