using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(AccountMemberPermission.AccountMemberPermissionSerializer))]
[Serializable]
public readonly record struct AccountMemberPermission : IStringEnum
{
    public static readonly AccountMemberPermission BillingCreditsEdit = new(
        Values.BillingCreditsEdit
    );

    public static readonly AccountMemberPermission CompaniesEdit = new(Values.CompaniesEdit);

    public static readonly AccountMemberPermission CompanyUsersEdit = new(Values.CompanyUsersEdit);

    public static readonly AccountMemberPermission ComponentsEdit = new(Values.ComponentsEdit);

    public static readonly AccountMemberPermission DataExportsEdit = new(Values.DataExportsEdit);

    public static readonly AccountMemberPermission FeaturesEdit = new(Values.FeaturesEdit);

    public static readonly AccountMemberPermission FlagRulesEdit = new(Values.FlagRulesEdit);

    public static readonly AccountMemberPermission FlagsEdit = new(Values.FlagsEdit);

    public static readonly AccountMemberPermission OverridesEdit = new(Values.OverridesEdit);

    public static readonly AccountMemberPermission PlanBillingEdit = new(Values.PlanBillingEdit);

    public static readonly AccountMemberPermission PlanEntitlementsEdit = new(
        Values.PlanEntitlementsEdit
    );

    public static readonly AccountMemberPermission PlanVersionsEdit = new(Values.PlanVersionsEdit);

    public static readonly AccountMemberPermission PlansEdit = new(Values.PlansEdit);

    public static readonly AccountMemberPermission WebhooksEdit = new(Values.WebhooksEdit);

    public static readonly AccountMemberPermission WebhooksRevealSecret = new(
        Values.WebhooksRevealSecret
    );

    public AccountMemberPermission(string value)
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
    public static AccountMemberPermission FromCustom(string value)
    {
        return new AccountMemberPermission(value);
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

    public static bool operator ==(AccountMemberPermission value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(AccountMemberPermission value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(AccountMemberPermission value) => value.Value;

    public static explicit operator AccountMemberPermission(string value) => new(value);

    internal class AccountMemberPermissionSerializer : JsonConverter<AccountMemberPermission>
    {
        public override AccountMemberPermission Read(
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
            return new AccountMemberPermission(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            AccountMemberPermission value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override AccountMemberPermission ReadAsPropertyName(
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
            return new AccountMemberPermission(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            AccountMemberPermission value,
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
        public const string BillingCreditsEdit = "billing_credits_edit";

        public const string CompaniesEdit = "companies_edit";

        public const string CompanyUsersEdit = "company_users_edit";

        public const string ComponentsEdit = "components_edit";

        public const string DataExportsEdit = "data_exports_edit";

        public const string FeaturesEdit = "features_edit";

        public const string FlagRulesEdit = "flag_rules_edit";

        public const string FlagsEdit = "flags_edit";

        public const string OverridesEdit = "overrides_edit";

        public const string PlanBillingEdit = "plan_billing_edit";

        public const string PlanEntitlementsEdit = "plan_entitlements_edit";

        public const string PlanVersionsEdit = "plan_versions_edit";

        public const string PlansEdit = "plans_edit";

        public const string WebhooksEdit = "webhooks_edit";

        public const string WebhooksRevealSecret = "webhooks_reveal_secret";
    }
}
