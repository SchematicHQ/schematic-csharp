using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(PlanBundleAction.PlanBundleActionSerializer))]
[Serializable]
public readonly record struct PlanBundleAction : IStringEnum
{
    public static readonly PlanBundleAction Create = new(Values.Create);

    public static readonly PlanBundleAction Update = new(Values.Update);

    public static readonly PlanBundleAction Delete = new(Values.Delete);

    public PlanBundleAction(string value)
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
    public static PlanBundleAction FromCustom(string value)
    {
        return new PlanBundleAction(value);
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

    public static bool operator ==(PlanBundleAction value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PlanBundleAction value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PlanBundleAction value) => value.Value;

    public static explicit operator PlanBundleAction(string value) => new(value);

    internal class PlanBundleActionSerializer : JsonConverter<PlanBundleAction>
    {
        public override PlanBundleAction Read(
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
            return new PlanBundleAction(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            PlanBundleAction value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override PlanBundleAction ReadAsPropertyName(
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
            return new PlanBundleAction(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            PlanBundleAction value,
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
        public const string Create = "create";

        public const string Update = "update";

        public const string Delete = "delete";
    }
}
