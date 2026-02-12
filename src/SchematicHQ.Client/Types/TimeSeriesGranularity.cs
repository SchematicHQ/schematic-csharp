using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<TimeSeriesGranularity>))]
[Serializable]
public readonly record struct TimeSeriesGranularity : IStringEnum
{
    public static readonly TimeSeriesGranularity Daily = new(Values.Daily);

    public static readonly TimeSeriesGranularity Hourly = new(Values.Hourly);

    public static readonly TimeSeriesGranularity Monthly = new(Values.Monthly);

    public static readonly TimeSeriesGranularity Weekly = new(Values.Weekly);

    public TimeSeriesGranularity(string value)
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
    public static TimeSeriesGranularity FromCustom(string value)
    {
        return new TimeSeriesGranularity(value);
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

    public static bool operator ==(TimeSeriesGranularity value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(TimeSeriesGranularity value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(TimeSeriesGranularity value) => value.Value;

    public static explicit operator TimeSeriesGranularity(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Daily = "daily";

        public const string Hourly = "hourly";

        public const string Monthly = "monthly";

        public const string Weekly = "weekly";
    }
}
