using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(MigrationErrorCode.MigrationErrorCodeSerializer))]
[Serializable]
public readonly record struct MigrationErrorCode : IStringEnum
{
    public static readonly MigrationErrorCode AmbiguousSubscriptionItem = new(
        Values.AmbiguousSubscriptionItem
    );

    public static readonly MigrationErrorCode MultipleSubscriptions = new(
        Values.MultipleSubscriptions
    );

    public static readonly MigrationErrorCode NoPriceForInterval = new(Values.NoPriceForInterval);

    public static readonly MigrationErrorCode NotOnOriginVersion = new(Values.NotOnOriginVersion);

    public static readonly MigrationErrorCode OperationItemNotFound = new(
        Values.OperationItemNotFound
    );

    public static readonly MigrationErrorCode PermanentConfig = new(Values.PermanentConfig);

    public static readonly MigrationErrorCode PermanentDecline = new(Values.PermanentDecline);

    public static readonly MigrationErrorCode TransientDecline = new(Values.TransientDecline);

    public static readonly MigrationErrorCode TransientInfra = new(Values.TransientInfra);

    public static readonly MigrationErrorCode TransientStripe = new(Values.TransientStripe);

    public static readonly MigrationErrorCode Unknown = new(Values.Unknown);

    public static readonly MigrationErrorCode WouldLeaveEmptySubscription = new(
        Values.WouldLeaveEmptySubscription
    );

    public MigrationErrorCode(string value)
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
    public static MigrationErrorCode FromCustom(string value)
    {
        return new MigrationErrorCode(value);
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

    public static bool operator ==(MigrationErrorCode value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(MigrationErrorCode value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(MigrationErrorCode value) => value.Value;

    public static explicit operator MigrationErrorCode(string value) => new(value);

    internal class MigrationErrorCodeSerializer : JsonConverter<MigrationErrorCode>
    {
        public override MigrationErrorCode Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var stringValue =
                reader.GetString()
                ?? throw new global::System.Exception(
                    "The JSON value could not be read as a string."
                );
            return new MigrationErrorCode(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            MigrationErrorCode value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override MigrationErrorCode ReadAsPropertyName(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var stringValue =
                reader.GetString()
                ?? throw new global::System.Exception(
                    "The JSON property name could not be read as a string."
                );
            return new MigrationErrorCode(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            MigrationErrorCode value,
            JsonSerializerOptions options
        )
        {
            writer.WritePropertyName(value.Value);
        }
    }

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string AmbiguousSubscriptionItem = "ambiguous_subscription_item";

        public const string MultipleSubscriptions = "multiple_subscriptions";

        public const string NoPriceForInterval = "no_price_for_interval";

        public const string NotOnOriginVersion = "not_on_origin_version";

        public const string OperationItemNotFound = "operation_item_not_found";

        public const string PermanentConfig = "permanent_config";

        public const string PermanentDecline = "permanent_decline";

        public const string TransientDecline = "transient_decline";

        public const string TransientInfra = "transient_infra";

        public const string TransientStripe = "transient_stripe";

        public const string Unknown = "unknown";

        public const string WouldLeaveEmptySubscription = "would_leave_empty_subscription";
    }
}
