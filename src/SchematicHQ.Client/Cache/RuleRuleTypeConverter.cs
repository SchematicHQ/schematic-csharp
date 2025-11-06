using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SchematicHQ.Client.Cache
{
    public class RuleRuleTypeConverter : JsonConverter<RuleRuleType>
    {
        public override RuleRuleType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            
            return value switch
            {
                "" or null => RuleRuleType.Unknown,
                "default" => RuleRuleType.Default,
                "global_override" => RuleRuleType.GlobalOverride,
                "company_override" => RuleRuleType.CompanyOverride,
                "company_override_usage_exceeded" => RuleRuleType.CompanyOverrideUsageExceeded,
                "plan_entitlement" => RuleRuleType.PlanEntitlement,
                "plan_entitlement_usage_exceeded" => RuleRuleType.PlanEntitlementUsageExceeded,
                "standard" => RuleRuleType.Standard,
                _ => RuleRuleType.Unknown
            };
        }

        public override void Write(Utf8JsonWriter writer, RuleRuleType value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Value);
        }
    }
}