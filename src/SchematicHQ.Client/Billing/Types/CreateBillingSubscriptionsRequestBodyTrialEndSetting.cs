using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<CreateBillingSubscriptionsRequestBodyTrialEndSetting>))]
[Serializable]
public readonly record struct CreateBillingSubscriptionsRequestBodyTrialEndSetting : IStringEnum
{
    public static readonly CreateBillingSubscriptionsRequestBodyTrialEndSetting Subscribe = new(
        Values.Subscribe
    );

    public static readonly CreateBillingSubscriptionsRequestBodyTrialEndSetting Cancel = new(
        Values.Cancel
    );

    public CreateBillingSubscriptionsRequestBodyTrialEndSetting(string value)
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
    public static CreateBillingSubscriptionsRequestBodyTrialEndSetting FromCustom(string value)
    {
        return new CreateBillingSubscriptionsRequestBodyTrialEndSetting(value);
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

    public static bool operator ==(
        CreateBillingSubscriptionsRequestBodyTrialEndSetting value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        CreateBillingSubscriptionsRequestBodyTrialEndSetting value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(
        CreateBillingSubscriptionsRequestBodyTrialEndSetting value
    ) => value.Value;

    public static explicit operator CreateBillingSubscriptionsRequestBodyTrialEndSetting(
        string value
    ) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Subscribe = "subscribe";

        public const string Cancel = "cancel";
    }
}
