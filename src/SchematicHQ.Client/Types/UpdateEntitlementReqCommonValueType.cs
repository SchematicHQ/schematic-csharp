using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<UpdateEntitlementReqCommonValueType>))]
[Serializable]
public readonly record struct UpdateEntitlementReqCommonValueType : IStringEnum
{
    public static readonly UpdateEntitlementReqCommonValueType Boolean = new(Values.Boolean);

    public static readonly UpdateEntitlementReqCommonValueType Credit = new(Values.Credit);

    public static readonly UpdateEntitlementReqCommonValueType Numeric = new(Values.Numeric);

    public static readonly UpdateEntitlementReqCommonValueType Trait = new(Values.Trait);

    public static readonly UpdateEntitlementReqCommonValueType Unlimited = new(Values.Unlimited);

    public UpdateEntitlementReqCommonValueType(string value)
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
    public static UpdateEntitlementReqCommonValueType FromCustom(string value)
    {
        return new UpdateEntitlementReqCommonValueType(value);
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

    public static bool operator ==(UpdateEntitlementReqCommonValueType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(UpdateEntitlementReqCommonValueType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(UpdateEntitlementReqCommonValueType value) =>
        value.Value;

    public static explicit operator UpdateEntitlementReqCommonValueType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Boolean = "boolean";

        public const string Credit = "credit";

        public const string Numeric = "numeric";

        public const string Trait = "trait";

        public const string Unlimited = "unlimited";
    }
}
