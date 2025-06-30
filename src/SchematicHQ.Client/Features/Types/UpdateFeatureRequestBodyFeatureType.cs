using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<UpdateFeatureRequestBodyFeatureType>))]
[Serializable]
public readonly record struct UpdateFeatureRequestBodyFeatureType : IStringEnum
{
    public static readonly UpdateFeatureRequestBodyFeatureType Boolean = new(Values.Boolean);

    public static readonly UpdateFeatureRequestBodyFeatureType Event = new(Values.Event);

    public static readonly UpdateFeatureRequestBodyFeatureType Trait = new(Values.Trait);

    public UpdateFeatureRequestBodyFeatureType(string value)
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
    public static UpdateFeatureRequestBodyFeatureType FromCustom(string value)
    {
        return new UpdateFeatureRequestBodyFeatureType(value);
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

    public static bool operator ==(UpdateFeatureRequestBodyFeatureType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(UpdateFeatureRequestBodyFeatureType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(UpdateFeatureRequestBodyFeatureType value) =>
        value.Value;

    public static explicit operator UpdateFeatureRequestBodyFeatureType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Boolean = "boolean";

        public const string Event = "event";

        public const string Trait = "trait";
    }
}
