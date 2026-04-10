using System.Text.Json;
using System.Text.Json.Serialization;

namespace SchematicHQ.Client.Datastream
{
    public static class Merge
    {
        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            Converters =
            {
                new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower, false),
                new EntityTypeConverter()
            }
        };

        /// <summary>
        /// Merges a partial JSON update into an existing Company.
        /// Deep-copies the existing company, then applies only the fields
        /// present in partialJson.
        /// </summary>
        public static RulesengineCompany PartialCompany(RulesengineCompany existing, string partialJson)
        {
            using var doc = JsonDocument.Parse(partialJson);
            var root = doc.RootElement;

            var merged = DeepCopyCompany(existing);

            foreach (var prop in root.EnumerateObject())
            {
                var raw = prop.Value.GetRawText();
                switch (prop.Name)
                {
                    case "id":
                        merged.Id = prop.Value.GetString()!;
                        break;
                    case "account_id":
                        merged.AccountId = prop.Value.GetString()!;
                        break;
                    case "environment_id":
                        merged.EnvironmentId = prop.Value.GetString()!;
                        break;
                    case "base_plan_id":
                        merged.BasePlanId = prop.Value.ValueKind == JsonValueKind.Null
                            ? null
                            : prop.Value.GetString();
                        break;
                    case "billing_product_ids":
                        merged.BillingProductIds = JsonSerializer.Deserialize<List<string>>(raw, JsonOptions)!;
                        break;
                    case "credit_balances":
                        var cb = JsonSerializer.Deserialize<Dictionary<string, double>>(raw, JsonOptions)!;
                        merged.CreditBalances ??= new Dictionary<string, double>();
                        foreach (var kvp in cb)
                        {
                            merged.CreditBalances[kvp.Key] = kvp.Value;
                        }
                        break;
                    case "entitlements":
                        merged.Entitlements = JsonSerializer.Deserialize<List<RulesengineFeatureEntitlement>>(raw, JsonOptions)!;
                        break;
                    case "keys":
                        var keys = JsonSerializer.Deserialize<Dictionary<string, string>>(raw, JsonOptions)!;
                        merged.Keys ??= new Dictionary<string, string>();
                        foreach (var kvp in keys)
                        {
                            merged.Keys[kvp.Key] = kvp.Value;
                        }
                        break;
                    case "metrics":
                        var incoming = JsonSerializer.Deserialize<List<RulesengineCompanyMetric>>(raw, JsonOptions)!;
                        merged.Metrics = UpsertMetrics(merged.Metrics, incoming);
                        break;
                    case "plan_ids":
                        merged.PlanIds = JsonSerializer.Deserialize<List<string>>(raw, JsonOptions)!;
                        break;
                    case "plan_version_ids":
                        merged.PlanVersionIds = JsonSerializer.Deserialize<List<string>>(raw, JsonOptions)!;
                        break;
                    case "rules":
                        merged.Rules = JsonSerializer.Deserialize<List<RulesengineRule>>(raw, JsonOptions)!;
                        break;
                    case "subscription":
                        merged.Subscription = JsonSerializer.Deserialize<RulesengineSubscription?>(raw, JsonOptions);
                        break;
                    case "traits":
                        merged.Traits = JsonSerializer.Deserialize<List<RulesengineTrait>>(raw, JsonOptions)!;
                        break;
                }
            }

            return merged;
        }

        /// <summary>
        /// Merges a partial JSON update into an existing User.
        /// Deep-copies the existing user, then applies only the fields
        /// present in partialJson.
        /// </summary>
        public static RulesengineUser PartialUser(RulesengineUser existing, string partialJson)
        {
            using var doc = JsonDocument.Parse(partialJson);
            var root = doc.RootElement;

            var merged = DeepCopyUser(existing);

            foreach (var prop in root.EnumerateObject())
            {
                var raw = prop.Value.GetRawText();
                switch (prop.Name)
                {
                    case "id":
                        merged.Id = prop.Value.GetString()!;
                        break;
                    case "account_id":
                        merged.AccountId = prop.Value.GetString()!;
                        break;
                    case "environment_id":
                        merged.EnvironmentId = prop.Value.GetString()!;
                        break;
                    case "keys":
                        var keys = JsonSerializer.Deserialize<Dictionary<string, string>>(raw, JsonOptions)!;
                        merged.Keys ??= new Dictionary<string, string>();
                        foreach (var kvp in keys)
                        {
                            merged.Keys[kvp.Key] = kvp.Value;
                        }
                        break;
                    case "traits":
                        merged.Traits = JsonSerializer.Deserialize<List<RulesengineTrait>>(raw, JsonOptions)!;
                        break;
                    case "rules":
                        merged.Rules = JsonSerializer.Deserialize<List<RulesengineRule>>(raw, JsonOptions)!;
                        break;
                }
            }

            return merged;
        }

        /// <summary>
        /// Creates a complete deep copy of a Company and all its nested fields.
        /// </summary>
        public static RulesengineCompany DeepCopyCompany(RulesengineCompany c)
        {
            var keysCopy = new Dictionary<string, string>(c.Keys ?? new Dictionary<string, string>());

            Dictionary<string, double>? creditBalancesCopy = null;
            if (c.CreditBalances != null)
            {
                creditBalancesCopy = new Dictionary<string, double>(c.CreditBalances);
            }

            var metricsCopy = new List<RulesengineCompanyMetric>();
            foreach (var metric in c.Metrics ?? Enumerable.Empty<RulesengineCompanyMetric>())
            {
                metricsCopy.Add(new RulesengineCompanyMetric
                {
                    AccountId = metric.AccountId,
                    EnvironmentId = metric.EnvironmentId,
                    CompanyId = metric.CompanyId,
                    EventSubtype = metric.EventSubtype,
                    Period = metric.Period,
                    MonthReset = metric.MonthReset,
                    Value = metric.Value,
                    CreatedAt = metric.CreatedAt,
                    ValidUntil = metric.ValidUntil
                });
            }

            var traitsCopy = new List<RulesengineTrait>();
            foreach (var trait in c.Traits ?? Enumerable.Empty<RulesengineTrait>())
            {
                traitsCopy.Add(new RulesengineTrait
                {
                    Value = trait.Value,
                    TraitDefinition = trait.TraitDefinition
                });
            }

            RulesengineSubscription? subscriptionCopy = null;
            if (c.Subscription != null)
            {
                subscriptionCopy = new RulesengineSubscription
                {
                    Id = c.Subscription.Id,
                    PeriodStart = c.Subscription.PeriodStart,
                    PeriodEnd = c.Subscription.PeriodEnd
                };
            }

            return new RulesengineCompany
            {
                Id = c.Id,
                AccountId = c.AccountId,
                EnvironmentId = c.EnvironmentId,
                BasePlanId = c.BasePlanId,
                BillingProductIds = new List<string>(c.BillingProductIds ?? Enumerable.Empty<string>()),
                CreditBalances = creditBalancesCopy ?? new Dictionary<string, double>(),
                Entitlements = c.Entitlements?.ToList(),
                Keys = keysCopy,
                Metrics = metricsCopy,
                PlanIds = new List<string>(c.PlanIds ?? Enumerable.Empty<string>()),
                PlanVersionIds = new List<string>(c.PlanVersionIds ?? Enumerable.Empty<string>()),
                Rules = c.Rules?.ToList() ?? new List<RulesengineRule>(),
                Subscription = subscriptionCopy,
                Traits = traitsCopy
            };
        }

        /// <summary>
        /// Creates a complete deep copy of a User and all its nested fields.
        /// </summary>
        public static RulesengineUser DeepCopyUser(RulesengineUser u)
        {
            return new RulesengineUser
            {
                Id = u.Id,
                AccountId = u.AccountId,
                EnvironmentId = u.EnvironmentId,
                Keys = u.Keys != null ? new Dictionary<string, string>(u.Keys) : new Dictionary<string, string>(),
                Traits = u.Traits?.ToList() ?? new List<RulesengineTrait>(),
                Rules = u.Rules?.ToList() ?? new List<RulesengineRule>()
            };
        }

        /// <summary>
        /// Merges incoming metrics into existing ones. Metrics are matched by
        /// (EventSubtype, Period, MonthReset); matches are replaced, new metrics are appended.
        /// </summary>
        private static List<RulesengineCompanyMetric> UpsertMetrics(
            IEnumerable<RulesengineCompanyMetric> existing,
            IEnumerable<RulesengineCompanyMetric> incoming)
        {
            var result = existing.ToList();
            var index = new Dictionary<(string, string, string), int>();

            for (int i = 0; i < result.Count; i++)
            {
                var m = result[i];
                index[(m.EventSubtype, m.Period.Value, m.MonthReset.Value)] = i;
            }

            foreach (var m in incoming)
            {
                var key = (m.EventSubtype, m.Period.Value, m.MonthReset.Value);
                if (index.TryGetValue(key, out var idx))
                {
                    result[idx] = m;
                }
                else
                {
                    result.Add(m);
                }
            }

            return result;
        }
    }
}
