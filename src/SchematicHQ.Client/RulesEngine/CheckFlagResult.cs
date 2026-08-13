namespace SchematicHQ.Client.RulesEngine
{
    /// <summary>
    /// Result of a local flag evaluation, returned by the datastream client.
    ///
    /// <para>Retained for backward compatibility. Internally the WASM rules engine produces the
    /// generated <see cref="RulesengineCheckFlagResult"/>, which is mapped onto this type at the
    /// engine boundary. (The former native-only <c>SetRuleFields</c> helper was removed with the
    /// native port; the WASM engine populates the entitlement fields directly.)</para>
    /// </summary>
    public class CheckFlagResult
    {
        public string? CompanyId { get; set; }
        public RulesengineFeatureEntitlement? Entitlement { get; set; }
        public Exception? Error { get; set; }
        public long? FeatureAllocation { get; set; }
        public long? FeatureUsage { get; set; }
        public string? FeatureUsageEvent { get; set; }
        public RulesengineMetricPeriod? FeatureUsagePeriod { get; set; }
        public DateTime? FeatureUsageResetAt { get; set; }
        public string? FlagId { get; set; }
        public required string FlagKey { get; set; }
        public required string Reason { get; set; }
        public string? RuleId { get; set; }
        public RulesengineRuleType? RuleType { get; set; }
        public string? UserId { get; set; }
        public bool Value { get; set; }
    }
}
