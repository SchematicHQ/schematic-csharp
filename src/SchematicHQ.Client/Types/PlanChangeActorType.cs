using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<PlanChangeActorType>))]
[Serializable]
public readonly record struct PlanChangeActorType : IStringEnum
{
    public static readonly PlanChangeActorType ApiKey = new(Values.ApiKey);

    public static readonly PlanChangeActorType AppUser = new(Values.AppUser);

    public static readonly PlanChangeActorType System = new(Values.System);

    public static readonly PlanChangeActorType TemporaryAccessToken = new(
        Values.TemporaryAccessToken
    );

    public PlanChangeActorType(string value)
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
    public static PlanChangeActorType FromCustom(string value)
    {
        return new PlanChangeActorType(value);
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

    public static bool operator ==(PlanChangeActorType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PlanChangeActorType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PlanChangeActorType value) => value.Value;

    public static explicit operator PlanChangeActorType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string ApiKey = "api_key";

        public const string AppUser = "app_user";

        public const string System = "system";

        public const string TemporaryAccessToken = "temporary_access_token";
    }
}
