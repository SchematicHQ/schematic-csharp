using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<PlanVersionCompanyMigrationStatus>))]
[Serializable]
public readonly record struct PlanVersionCompanyMigrationStatus : IStringEnum
{
    public static readonly PlanVersionCompanyMigrationStatus Completed = new(Values.Completed);

    public static readonly PlanVersionCompanyMigrationStatus Failed = new(Values.Failed);

    public static readonly PlanVersionCompanyMigrationStatus InProgress = new(Values.InProgress);

    public static readonly PlanVersionCompanyMigrationStatus Pending = new(Values.Pending);

    public static readonly PlanVersionCompanyMigrationStatus Skipped = new(Values.Skipped);

    public PlanVersionCompanyMigrationStatus(string value)
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
    public static PlanVersionCompanyMigrationStatus FromCustom(string value)
    {
        return new PlanVersionCompanyMigrationStatus(value);
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

    public static bool operator ==(PlanVersionCompanyMigrationStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PlanVersionCompanyMigrationStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PlanVersionCompanyMigrationStatus value) => value.Value;

    public static explicit operator PlanVersionCompanyMigrationStatus(string value) => new(value);

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

        public const string Skipped = "skipped";
    }
}
