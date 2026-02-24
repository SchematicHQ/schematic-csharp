using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<PlanVersionMigrationStrategy>))]
[Serializable]
public readonly record struct PlanVersionMigrationStrategy : IStringEnum
{
    public static readonly PlanVersionMigrationStrategy Immediate = new(Values.Immediate);

    public static readonly PlanVersionMigrationStrategy Leave = new(Values.Leave);

    public PlanVersionMigrationStrategy(string value)
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
    public static PlanVersionMigrationStrategy FromCustom(string value)
    {
        return new PlanVersionMigrationStrategy(value);
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

    public static bool operator ==(PlanVersionMigrationStrategy value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PlanVersionMigrationStrategy value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PlanVersionMigrationStrategy value) => value.Value;

    public static explicit operator PlanVersionMigrationStrategy(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Immediate = "immediate";

        public const string Leave = "leave";
    }
}
