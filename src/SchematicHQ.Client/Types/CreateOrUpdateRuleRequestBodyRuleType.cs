using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(
    typeof(CreateOrUpdateRuleRequestBodyRuleType.CreateOrUpdateRuleRequestBodyRuleTypeSerializer)
)]
[Serializable]
public readonly record struct CreateOrUpdateRuleRequestBodyRuleType : IStringEnum
{
    public static readonly CreateOrUpdateRuleRequestBodyRuleType GlobalOverride = new(
        Values.GlobalOverride
    );

    public static readonly CreateOrUpdateRuleRequestBodyRuleType CompanyOverride = new(
        Values.CompanyOverride
    );

    public static readonly CreateOrUpdateRuleRequestBodyRuleType PlanEntitlement = new(
        Values.PlanEntitlement
    );

    public static readonly CreateOrUpdateRuleRequestBodyRuleType Standard = new(Values.Standard);

    public static readonly CreateOrUpdateRuleRequestBodyRuleType Default = new(Values.Default);

    public CreateOrUpdateRuleRequestBodyRuleType(string value)
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
    public static CreateOrUpdateRuleRequestBodyRuleType FromCustom(string value)
    {
        return new CreateOrUpdateRuleRequestBodyRuleType(value);
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

    public static bool operator ==(CreateOrUpdateRuleRequestBodyRuleType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CreateOrUpdateRuleRequestBodyRuleType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(CreateOrUpdateRuleRequestBodyRuleType value) =>
        value.Value;

    public static explicit operator CreateOrUpdateRuleRequestBodyRuleType(string value) =>
        new(value);

    internal class CreateOrUpdateRuleRequestBodyRuleTypeSerializer
        : JsonConverter<CreateOrUpdateRuleRequestBodyRuleType>
    {
        public override CreateOrUpdateRuleRequestBodyRuleType Read(
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
            return new CreateOrUpdateRuleRequestBodyRuleType(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            CreateOrUpdateRuleRequestBodyRuleType value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override CreateOrUpdateRuleRequestBodyRuleType ReadAsPropertyName(
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
            return new CreateOrUpdateRuleRequestBodyRuleType(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            CreateOrUpdateRuleRequestBodyRuleType value,
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
        public const string GlobalOverride = "global_override";

        public const string CompanyOverride = "company_override";

        public const string PlanEntitlement = "plan_entitlement";

        public const string Standard = "standard";

        public const string Default = "default";
    }
}
