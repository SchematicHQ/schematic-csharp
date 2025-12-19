using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<DataExportStatus>))]
[Serializable]
public readonly record struct DataExportStatus : IStringEnum
{
    public static readonly DataExportStatus Failure = new(Values.Failure);

    public static readonly DataExportStatus Pending = new(Values.Pending);

    public static readonly DataExportStatus Success = new(Values.Success);

    public DataExportStatus(string value)
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
    public static DataExportStatus FromCustom(string value)
    {
        return new DataExportStatus(value);
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

    public static bool operator ==(DataExportStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(DataExportStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(DataExportStatus value) => value.Value;

    public static explicit operator DataExportStatus(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Failure = "failure";

        public const string Pending = "pending";

        public const string Success = "success";
    }
}
