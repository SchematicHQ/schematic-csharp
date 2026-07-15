namespace SchematicHQ.Client.RulesEngine
{
    /// <summary>
    /// Response from CheckFlagWithEntitlement containing flag check result with entitlement information.
    /// This is a subset of CheckFlagResponseData with deprecated fields removed.
    ///
    /// <para>This is the public return type of <see cref="Schematic.CheckFlagWithEntitlement"/> and is
    /// intentionally kept distinct from the generated <see cref="RulesengineCheckFlagResult"/> (used
    /// internally on the datastream/WASM path) so that its shape stays stable for existing consumers.</para>
    /// </summary>
    public class CheckFlagWithEntitlementResponse
    {
        /// <summary>
        /// If company keys were provided and matched a company, its ID
        /// </summary>
        public string? CompanyId { get; set; }

        /// <summary>
        /// Entitlement information for the feature, if applicable
        /// </summary>
        public RulesengineFeatureEntitlement? Entitlement { get; set; }

        /// <summary>
        /// If a flag was found, its ID
        /// </summary>
        public string? FlagId { get; set; }

        /// <summary>
        /// The key used to check the flag
        /// </summary>
        public required string FlagKey { get; set; }

        /// <summary>
        /// A human-readable explanation of the result
        /// </summary>
        public required string Reason { get; set; }

        /// <summary>
        /// If a rule was found, its ID
        /// </summary>
        public string? RuleId { get; set; }

        /// <summary>
        /// If a rule was found, its type
        /// </summary>
        public string? RuleType { get; set; }

        /// <summary>
        /// If user keys were provided and matched a user, its ID
        /// </summary>
        public string? UserId { get; set; }

        /// <summary>
        /// A boolean flag check result; for feature entitlements, this represents whether further consumption is permitted
        /// </summary>
        public bool Value { get; set; }

        /// <summary>
        /// Create a CheckFlagWithEntitlementResponse from a rules engine result
        /// </summary>
        public static CheckFlagWithEntitlementResponse FromCheckFlagResult(CheckFlagResult result)
        {
            return new CheckFlagWithEntitlementResponse
            {
                CompanyId = result.CompanyId,
                Entitlement = result.Entitlement,
                FlagId = result.FlagId,
                FlagKey = result.FlagKey,
                Reason = result.Reason,
                RuleId = result.RuleId,
                RuleType = result.RuleType?.Value,
                UserId = result.UserId,
                Value = result.Value
            };
        }

        /// <summary>
        /// Create a CheckFlagWithEntitlementResponse from an API CheckFlagResponseData
        /// </summary>
        public static CheckFlagWithEntitlementResponse FromApiResponse(CheckFlagResponseData data, string flagKey)
        {
            return new CheckFlagWithEntitlementResponse
            {
                CompanyId = data.CompanyId,
                Entitlement = ToRulesEngineEntitlement(data.Entitlement),
                FlagId = data.FlagId,
                FlagKey = flagKey,
                Reason = data.Reason,
                RuleId = data.RuleId,
                RuleType = data.RuleType?.Value,
                UserId = data.UserId,
                Value = data.Value
            };
        }

        /// <summary>
        /// Convert API FeatureEntitlement to RulesengineFeatureEntitlement
        /// </summary>
        private static RulesengineFeatureEntitlement? ToRulesEngineEntitlement(FeatureEntitlement? entitlement)
        {
            if (entitlement == null)
                return null;

            return new RulesengineFeatureEntitlement
            {
                Allocation = entitlement.Allocation,
                CreditId = entitlement.CreditId,
                CreditRemaining = entitlement.CreditRemaining,
                CreditTotal = entitlement.CreditTotal,
                CreditUsed = entitlement.CreditUsed,
                EventName = entitlement.EventName,
                FeatureId = entitlement.FeatureId,
                FeatureKey = entitlement.FeatureKey,
                MetricPeriod = entitlement.MetricPeriod.HasValue
                    ? new RulesengineMetricPeriod(entitlement.MetricPeriod.Value.Value)
                    : null,
                MetricResetAt = entitlement.MetricResetAt,
                MonthReset = entitlement.MonthReset.HasValue
                    ? new RulesengineMetricPeriodMonthReset(entitlement.MonthReset.Value.Value)
                    : null,
                SoftLimit = entitlement.SoftLimit,
                Usage = entitlement.Usage,
                ValueType = new RulesengineEntitlementValueType(entitlement.ValueType.Value)
            };
        }
    }
}
