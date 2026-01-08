using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<PlanVersionStatus>))]
[Serializable]
public readonly record struct PlanVersionStatus : IStringEnum
{
    public static readonly PlanVersionStatus Published = new(Values.Published);

    public static readonly PlanVersionStatus Draft = new(Values.Draft);

    public static readonly PlanVersionStatus Archived = new(Values.Archived);

    public PlanVersionStatus(string value)
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
    public static PlanVersionStatus FromCustom(string value)
    {
        return new PlanVersionStatus(value);
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

    public static bool operator ==(PlanVersionStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PlanVersionStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PlanVersionStatus value) => value.Value;

    public static explicit operator PlanVersionStatus(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Published = "published";

        public const string Draft = "draft";

        public const string Archived = "archived";
    }
}
