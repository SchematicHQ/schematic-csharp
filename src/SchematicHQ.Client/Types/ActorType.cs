using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<ActorType>))]
[Serializable]
public readonly record struct ActorType : IStringEnum
{
    public static readonly ActorType ApiKey = new(Values.ApiKey);

    public static readonly ActorType AppUser = new(Values.AppUser);

    public static readonly ActorType StripeApp = new(Values.StripeApp);

    public static readonly ActorType System = new(Values.System);

    public static readonly ActorType TemporaryAccessToken = new(Values.TemporaryAccessToken);

    public ActorType(string value)
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
    public static ActorType FromCustom(string value)
    {
        return new ActorType(value);
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

    public static bool operator ==(ActorType value1, string value2) => value1.Value.Equals(value2);

    public static bool operator !=(ActorType value1, string value2) => !value1.Value.Equals(value2);

    public static explicit operator string(ActorType value) => value.Value;

    public static explicit operator ActorType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string ApiKey = "api_key";

        public const string AppUser = "app_user";

        public const string StripeApp = "stripe_app";

        public const string System = "system";

        public const string TemporaryAccessToken = "temporary_access_token";
    }
}
