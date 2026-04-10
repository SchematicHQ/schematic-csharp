using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(PlanIcon.PlanIconSerializer))]
[Serializable]
public readonly record struct PlanIcon : IStringEnum
{
    public static readonly PlanIcon Amber = new(Values.Amber);

    public static readonly PlanIcon Blue = new(Values.Blue);

    public static readonly PlanIcon BlueGray = new(Values.BlueGray);

    public static readonly PlanIcon BlueGreen = new(Values.BlueGreen);

    public static readonly PlanIcon Cyan = new(Values.Cyan);

    public static readonly PlanIcon Emerald = new(Values.Emerald);

    public static readonly PlanIcon Fuchsia = new(Values.Fuchsia);

    public static readonly PlanIcon Gray = new(Values.Gray);

    public static readonly PlanIcon Green = new(Values.Green);

    public static readonly PlanIcon Indigo = new(Values.Indigo);

    public static readonly PlanIcon LightBlue = new(Values.LightBlue);

    public static readonly PlanIcon Lime = new(Values.Lime);

    public static readonly PlanIcon Orange = new(Values.Orange);

    public static readonly PlanIcon Pink = new(Values.Pink);

    public static readonly PlanIcon Purple = new(Values.Purple);

    public static readonly PlanIcon Red = new(Values.Red);

    public static readonly PlanIcon RedOrange = new(Values.RedOrange);

    public static readonly PlanIcon Rose = new(Values.Rose);

    public static readonly PlanIcon Sky = new(Values.Sky);

    public static readonly PlanIcon Slate = new(Values.Slate);

    public static readonly PlanIcon Teal = new(Values.Teal);

    public static readonly PlanIcon TrueGray = new(Values.TrueGray);

    public static readonly PlanIcon Violet = new(Values.Violet);

    public static readonly PlanIcon WarmGray = new(Values.WarmGray);

    public static readonly PlanIcon Yellow = new(Values.Yellow);

    public PlanIcon(string value)
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
    public static PlanIcon FromCustom(string value)
    {
        return new PlanIcon(value);
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

    public static bool operator ==(PlanIcon value1, string value2) => value1.Value.Equals(value2);

    public static bool operator !=(PlanIcon value1, string value2) => !value1.Value.Equals(value2);

    public static explicit operator string(PlanIcon value) => value.Value;

    public static explicit operator PlanIcon(string value) => new(value);

    internal class PlanIconSerializer : JsonConverter<PlanIcon>
    {
        public override PlanIcon Read(
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
            return new PlanIcon(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            PlanIcon value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override PlanIcon ReadAsPropertyName(
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
            return new PlanIcon(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            PlanIcon value,
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
        public const string Amber = "amber";

        public const string Blue = "blue";

        public const string BlueGray = "blueGray";

        public const string BlueGreen = "blueGreen";

        public const string Cyan = "cyan";

        public const string Emerald = "emerald";

        public const string Fuchsia = "fuchsia";

        public const string Gray = "gray";

        public const string Green = "green";

        public const string Indigo = "indigo";

        public const string LightBlue = "lightBlue";

        public const string Lime = "lime";

        public const string Orange = "orange";

        public const string Pink = "pink";

        public const string Purple = "purple";

        public const string Red = "red";

        public const string RedOrange = "redOrange";

        public const string Rose = "rose";

        public const string Sky = "sky";

        public const string Slate = "slate";

        public const string Teal = "teal";

        public const string TrueGray = "trueGray";

        public const string Violet = "violet";

        public const string WarmGray = "warmGray";

        public const string Yellow = "yellow";
    }
}
