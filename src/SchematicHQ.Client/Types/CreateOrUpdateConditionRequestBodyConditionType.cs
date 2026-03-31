using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(
    typeof(CreateOrUpdateConditionRequestBodyConditionType.CreateOrUpdateConditionRequestBodyConditionTypeSerializer)
)]
[Serializable]
public readonly record struct CreateOrUpdateConditionRequestBodyConditionType : IStringEnum
{
    public static readonly CreateOrUpdateConditionRequestBodyConditionType Company = new(
        Values.Company
    );

    public static readonly CreateOrUpdateConditionRequestBodyConditionType Metric = new(
        Values.Metric
    );

    public static readonly CreateOrUpdateConditionRequestBodyConditionType Trait = new(
        Values.Trait
    );

    public static readonly CreateOrUpdateConditionRequestBodyConditionType User = new(Values.User);

    public static readonly CreateOrUpdateConditionRequestBodyConditionType Plan = new(Values.Plan);

    public static readonly CreateOrUpdateConditionRequestBodyConditionType BillingProduct = new(
        Values.BillingProduct
    );

    public static readonly CreateOrUpdateConditionRequestBodyConditionType BasePlan = new(
        Values.BasePlan
    );

    public CreateOrUpdateConditionRequestBodyConditionType(string value)
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
    public static CreateOrUpdateConditionRequestBodyConditionType FromCustom(string value)
    {
        return new CreateOrUpdateConditionRequestBodyConditionType(value);
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
        CreateOrUpdateConditionRequestBodyConditionType value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        CreateOrUpdateConditionRequestBodyConditionType value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(CreateOrUpdateConditionRequestBodyConditionType value) =>
        value.Value;

    public static explicit operator CreateOrUpdateConditionRequestBodyConditionType(string value) =>
        new(value);

    internal class CreateOrUpdateConditionRequestBodyConditionTypeSerializer
        : JsonConverter<CreateOrUpdateConditionRequestBodyConditionType>
    {
        public override CreateOrUpdateConditionRequestBodyConditionType Read(
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
            return new CreateOrUpdateConditionRequestBodyConditionType(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            CreateOrUpdateConditionRequestBodyConditionType value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override CreateOrUpdateConditionRequestBodyConditionType ReadAsPropertyName(
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
            return new CreateOrUpdateConditionRequestBodyConditionType(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            CreateOrUpdateConditionRequestBodyConditionType value,
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

        public const string Metric = "metric";

        public const string Trait = "trait";

        public const string User = "user";

        public const string Plan = "plan";

        public const string BillingProduct = "billing_product";

        public const string BasePlan = "base_plan";
    }
}
