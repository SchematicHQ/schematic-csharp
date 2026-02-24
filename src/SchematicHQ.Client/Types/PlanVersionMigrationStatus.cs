using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<PlanVersionMigrationStatus>))]
[Serializable]
public readonly record struct PlanVersionMigrationStatus : IStringEnum
{
    public static readonly PlanVersionMigrationStatus Completed = new(Values.Completed);

    public static readonly PlanVersionMigrationStatus Failed = new(Values.Failed);

    public static readonly PlanVersionMigrationStatus InProgress = new(Values.InProgress);

    public static readonly PlanVersionMigrationStatus Pending = new(Values.Pending);

    public PlanVersionMigrationStatus(string value)
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
    public static PlanVersionMigrationStatus FromCustom(string value)
    {
        return new PlanVersionMigrationStatus(value);
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

    public static bool operator ==(PlanVersionMigrationStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PlanVersionMigrationStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PlanVersionMigrationStatus value) => value.Value;

    public static explicit operator PlanVersionMigrationStatus(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Completed = "completed";

        public const string Failed = "failed";

        public const string InProgress = "in_progress";

        public const string Pending = "pending";
    }
}
