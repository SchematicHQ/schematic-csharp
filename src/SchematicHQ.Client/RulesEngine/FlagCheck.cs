using SchematicHQ.Client.RulesEngine.Utils;
using SchematicHQ.Client.RulesEngine.Extensions;
using SchematicHQ.Client.RulesEngine.Wasm;

namespace SchematicHQ.Client.RulesEngine
{

    public static class Errors
    {
        public static readonly Exception ErrorUnexpected = new Exception("Unexpected error");
        public static readonly Exception ErrorFlagNotFound = new Exception("Flag not found");
    }

    public static class FlagCheckService
    {
        public const string ReasonNoCompanyOrUser = "No company or user context; default value for flag";
        public const string ReasonCompanyNotFound = "Company not found";
        public const string ReasonCompanyNotSpecified = "Must specify a company";
        public const string ReasonFlagNotFound = "Flag not found";
        public const string ReasonNoRulesMatched = "No rules matched; default value for flag";
        public const string ReasonServerError = "Server error; Schematic has been notified";
        public const string ReasonUserNotFound = "User not found";

        /// <summary>
        /// Checks a flag using the WASM-powered rules engine by default.
        /// Falls back to traditional logic if WASM is not available.
        /// </summary>
        public static async Task<RulesengineCheckFlagResult> CheckFlag(
            RulesengineCompany? company,
            RulesengineUser? user,
            RulesengineFlag? flag,
            CancellationToken cancellationToken = default)
        {
            try
            {
                // Try using the WASM engine first for better performance
                using var wasmEngine = new WasmRulesEngine();
                return await wasmEngine.CheckFlagAsync(company, user, flag, cancellationToken);
            }
            catch
            {
                // Fall back to traditional logic if WASM fails
                return await CheckFlagTraditional(company, user, flag, cancellationToken);
            }
        }

        /// <summary>
        /// Traditional flag check implementation used as fallback when WASM is not available.
        /// </summary>
        private static async Task<RulesengineCheckFlagResult> CheckFlagTraditional(
            RulesengineCompany? company,
            RulesengineUser? user,
            RulesengineFlag? flag,
            CancellationToken cancellationToken = default)
        {
            var resp = new RulesengineCheckFlagResult
            {
              Reason = ReasonNoRulesMatched,
              FlagKey = "",
              Value = false, // Required property
            };

            if (flag == null)
            {
                resp.Reason = ReasonFlagNotFound;
                resp.Err = Errors.ErrorFlagNotFound.Message;
                return resp;
            }

            resp.FlagId = flag.Id;
            resp.FlagKey = flag.Key;
            resp.Value = flag.DefaultValue;

            if (company != null)
            {
                resp.CompanyId = company.Id;
            }

            if (user != null)
            {
                resp.UserId = user.Id;
            }

            // Filter company and user rules to only include those for this flag
            var companyRules = company?.Rules?
                .Where(r => r?.FlagId != null && r.FlagId == flag.Id)
                .ToList();
            var userRules = user?.Rules?
                .Where(r => r?.FlagId != null && r.FlagId == flag.Id)
                .ToList();

            var ruleChecker = RuleCheckService.NewRuleCheckService();
            foreach (var group in GroupRulesByPriority(flag.Rules?.ToList(), companyRules, userRules))
            {
                foreach (var rule in group)
                {
                    if (rule == null)
                    {
                        continue;
                    }

                    try
                    {
                        var checkRuleResp = await ruleChecker.Check(new CheckScope
                        {
                            Company = company,
                            Rule = rule,
                            User = user
                        }, cancellationToken);

                        if (checkRuleResp == null)
                        {
                            resp.Err = Errors.ErrorUnexpected.Message;
                            return resp;
                        }

                        if (checkRuleResp.Match)
                        {
                            resp.Value = rule.Value;
                            resp.Reason = $"Matched {rule.RuleType.DisplayName()} rule \"{rule.Name}\" ({rule.Id})";
                            resp.SetRuleFields(company, rule);
                            return resp;
                        }
                    }
                    catch (Exception ex)
                    {
                        resp.Err = ex.Message;
                        return resp;
                    }
                }
            }

            return resp;
        }

        public static List<List<RulesengineRule>> GroupRulesByPriority(params List<RulesengineRule>?[] ruleSlices)
        {
            var allRules = new List<RulesengineRule>();
            foreach (var rules in ruleSlices)
            {
                if (rules != null && rules.Count > 0)
                {
                    allRules.AddRange(rules);
                }
            }

            if (allRules.Count == 0)
            {
                return new List<List<RulesengineRule>>();
            }

            // Group rules by their type (convert to internal enum for grouping)
            var grouped = allRules.GroupBy(rule => rule.RuleType)
                .ToDictionary(g => g.Key, g => g.ToList());

            // Prioritize rules within each type group
            foreach (var ruleType in grouped.Keys.ToList())
            {
                var rulesForType = grouped[ruleType];
                switch (ruleType.PrioritizationMethod())
                {
                    case RulePrioritizationMethod.Priority:
                        grouped[ruleType] = rulesForType.OrderBy(r => r.Priority).ToList();
                        break;
                    case RulePrioritizationMethod.Optimistic:
                        grouped[ruleType] = rulesForType.OrderByDescending(r => r.Value).ToList();
                        break;
                }
            }

            // Prioritize type groups relative to one another
            var prioritizedGroups = new List<List<RulesengineRule>>();
            foreach (var ruleType in RulesengineRuleTypeExtensions.RuleTypePriority)
            {
                if (grouped.TryGetValue(ruleType, out var rules2))
                {
                    prioritizedGroups.Add(rules2);
                }
            }

            return prioritizedGroups;
        }
    }
}