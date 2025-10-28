using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<PlanChangeResponseDataActorType>))]
[Serializable]
public readonly record struct PlanChangeResponseDataActorType : IStringEnum
{
    public static readonly PlanChangeResponseDataActorType AppUser = new(Values.AppUser);

    public static readonly PlanChangeResponseDataActorType ApiKey = new(Values.ApiKey);

    public static readonly PlanChangeResponseDataActorType System = new(Values.System);

    public static readonly PlanChangeResponseDataActorType TemporaryAccessToken = new(
        Values.TemporaryAccessToken
    );

    public PlanChangeResponseDataActorType(string value)
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
    public static PlanChangeResponseDataActorType FromCustom(string value)
    {
        return new PlanChangeResponseDataActorType(value);
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

    public static bool operator ==(PlanChangeResponseDataActorType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(PlanChangeResponseDataActorType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(PlanChangeResponseDataActorType value) => value.Value;

    public static explicit operator PlanChangeResponseDataActorType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string AppUser = "app_user";

        public const string ApiKey = "api_key";

        public const string System = "system";

        public const string TemporaryAccessToken = "temporary_access_token";
    }
}
