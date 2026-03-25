using NUnit.Framework;
using SchematicHQ.Client.Datastream;

namespace SchematicHQ.Client.Test.Datastream
{
    [TestFixture]
    public class MergeTests
    {
        private static RulesengineCompany BaseCompany()
        {
            return new RulesengineCompany
            {
                Id = "co-1",
                AccountId = "acc-1",
                EnvironmentId = "env-1",
                BasePlanId = "plan-1",
                BillingProductIds = new List<string> { "bp-1" },
                CreditBalances = new Dictionary<string, double> { ["credit-1"] = 100.0 },
                Keys = new Dictionary<string, string> { ["domain"] = "example.com" },
                PlanIds = new List<string> { "plan-1" },
                PlanVersionIds = new List<string> { "pv-1" },
                Traits = new List<RulesengineTrait>
                {
                    new RulesengineTrait
                    {
                        Value = "Enterprise",
                        TraitDefinition = new RulesengineTraitDefinition
                        {
                            Id = "plan",
                            ComparableType = RulesengineTraitDefinitionComparableType.String,
                            EntityType = RulesengineEntityType.Company
                        }
                    }
                },
                Entitlements = new List<RulesengineFeatureEntitlement>
                {
                    new RulesengineFeatureEntitlement
                    {
                        FeatureId = "feat-1",
                        FeatureKey = "feature-one",
                        ValueType = RulesengineEntitlementValueType.Boolean
                    }
                }
            };
        }

        private static RulesengineUser BaseUser()
        {
            return new RulesengineUser
            {
                Id = "user-1",
                AccountId = "acc-1",
                EnvironmentId = "env-1",
                Keys = new Dictionary<string, string> { ["email"] = "user@example.com" },
                Traits = new List<RulesengineTrait>
                {
                    new RulesengineTrait
                    {
                        Value = "Premium",
                        TraitDefinition = new RulesengineTraitDefinition
                        {
                            Id = "tier",
                            ComparableType = RulesengineTraitDefinitionComparableType.String,
                            EntityType = RulesengineEntityType.User
                        }
                    }
                }
            };
        }

        private static RulesengineRule MakeRule(string id)
        {
            return new RulesengineRule
            {
                Id = id,
                AccountId = "acc-1",
                EnvironmentId = "env-1",
                Name = id,
                Priority = 1,
                RuleType = RulesengineRuleRuleType.Default,
                Value = true
            };
        }

        // --- PartialCompany tests ---

        [Test]
        public void PartialCompany_OnlyTraits()
        {
            var existing = BaseCompany();
            var partial = @"{""id"":""co-1"",""traits"":[{""value"":""Startup"",""trait_definition"":{""id"":""plan"",""comparable_type"":""string"",""entity_type"":""company""}}]}";

            var merged = Merge.PartialCompany(existing, partial);

            Assert.That(merged.Traits.Count(), Is.EqualTo(1));
            Assert.That(merged.Traits.First().Value, Is.EqualTo("Startup"));

            // Other fields preserved
            Assert.That(merged.AccountId, Is.EqualTo("acc-1"));
            Assert.That(merged.EnvironmentId, Is.EqualTo("env-1"));
            Assert.That(merged.Keys, Is.EqualTo(new Dictionary<string, string> { ["domain"] = "example.com" }));
            Assert.That(merged.BillingProductIds.ToList(), Is.EqualTo(new List<string> { "bp-1" }));
            Assert.That(merged.BasePlanId, Is.Not.Null);
            Assert.That(merged.BasePlanId, Is.EqualTo("plan-1"));
        }

        [Test]
        public void PartialCompany_MergesKeys()
        {
            var existing = BaseCompany();
            var partial = @"{""id"":""co-1"",""keys"":{""slug"":""new-slug""}}";

            var merged = Merge.PartialCompany(existing, partial);

            // New key added, existing key preserved
            Assert.That(merged.Keys, Is.EqualTo(new Dictionary<string, string>
            {
                ["domain"] = "example.com",
                ["slug"] = "new-slug"
            }));
            Assert.That(merged.Traits.Count(), Is.EqualTo(1));
        }

        [Test]
        public void PartialCompany_MergesCreditBalances()
        {
            var existing = BaseCompany();
            var partial = @"{""id"":""co-1"",""credit_balances"":{""credit-2"":200.0}}";

            var merged = Merge.PartialCompany(existing, partial);

            // New balance added, existing balance preserved
            Assert.That(merged.CreditBalances, Is.EqualTo(new Dictionary<string, double>
            {
                ["credit-1"] = 100.0,
                ["credit-2"] = 200.0
            }));
        }

        [Test]
        public void PartialCompany_OverwritesCreditBalance()
        {
            var existing = BaseCompany();
            var partial = @"{""id"":""co-1"",""credit_balances"":{""credit-1"":50.0}}";

            var merged = Merge.PartialCompany(existing, partial);

            Assert.That(merged.CreditBalances, Is.EqualTo(new Dictionary<string, double>
            {
                ["credit-1"] = 50.0
            }));
        }

        [Test]
        public void PartialCompany_UpsertsMetrics()
        {
            var existing = BaseCompany();
            existing.Metrics = new List<RulesengineCompanyMetric>
            {
                new RulesengineCompanyMetric
                {
                    AccountId = "acc-1", EnvironmentId = "env-1", CompanyId = "co-1",
                    EventSubtype = "event-a", Period = RulesengineCompanyMetricPeriod.AllTime,
                    MonthReset = RulesengineCompanyMetricMonthReset.FirstOfMonth,
                    Value = 10, CreatedAt = DateTime.UtcNow
                },
                new RulesengineCompanyMetric
                {
                    AccountId = "acc-1", EnvironmentId = "env-1", CompanyId = "co-1",
                    EventSubtype = "event-b", Period = RulesengineCompanyMetricPeriod.CurrentMonth,
                    MonthReset = RulesengineCompanyMetricMonthReset.FirstOfMonth,
                    Value = 5, CreatedAt = DateTime.UtcNow
                }
            };

            // Update event-a, add event-c
            var partial = @"{""id"":""co-1"",""metrics"":[
                {""account_id"":""acc-1"",""environment_id"":""env-1"",""company_id"":""co-1"",""event_subtype"":""event-a"",""period"":""all_time"",""month_reset"":""first_of_month"",""value"":42,""created_at"":""2026-01-01T00:00:00Z""},
                {""account_id"":""acc-1"",""environment_id"":""env-1"",""company_id"":""co-1"",""event_subtype"":""event-c"",""period"":""current_day"",""month_reset"":""billing_cycle"",""value"":1,""created_at"":""2026-01-01T00:00:00Z""}
            ]}";

            var merged = Merge.PartialCompany(existing, partial);

            var metrics = merged.Metrics.ToList();
            Assert.That(metrics.Count, Is.EqualTo(3));
            // event-a updated in place
            Assert.That(metrics[0].EventSubtype, Is.EqualTo("event-a"));
            Assert.That(metrics[0].Value, Is.EqualTo(42));
            // event-b unchanged
            Assert.That(metrics[1].EventSubtype, Is.EqualTo("event-b"));
            Assert.That(metrics[1].Value, Is.EqualTo(5));
            // event-c appended
            Assert.That(metrics[2].EventSubtype, Is.EqualTo("event-c"));
            Assert.That(metrics[2].Value, Is.EqualTo(1));

            // Original not mutated
            Assert.That(existing.Metrics.First().Value, Is.EqualTo(10));
        }

        [Test]
        public void PartialCompany_EmptyEntitlements()
        {
            var existing = BaseCompany();
            var partial = @"{""id"":""co-1"",""entitlements"":[]}";

            var merged = Merge.PartialCompany(existing, partial);

            Assert.That(merged.Entitlements!.Count(), Is.EqualTo(0));
            Assert.That(merged.AccountId, Is.EqualTo("acc-1"));
        }

        [Test]
        public void PartialCompany_NullBasePlanId()
        {
            var existing = BaseCompany();
            var partial = @"{""id"":""co-1"",""base_plan_id"":null}";

            var merged = Merge.PartialCompany(existing, partial);

            Assert.That(merged.BasePlanId, Is.Null);
            Assert.That(merged.BillingProductIds.ToList(), Is.EqualTo(new List<string> { "bp-1" }));
        }

        [Test]
        public void PartialCompany_MissingId()
        {
            var existing = BaseCompany();
            var partial = @"{""traits"":[]}";

            var ex = Assert.Throws<ArgumentException>(() => Merge.PartialCompany(existing, partial));
            Assert.That(ex!.Message, Does.Contain("missing required field: id"));
        }

        [Test]
        public void PartialCompany_DoesNotMutateOriginal()
        {
            var existing = BaseCompany();
            var origKeys = new Dictionary<string, string>(existing.Keys);

            var partial = @"{""id"":""co-1"",""keys"":{""slug"":""new-slug""},""traits"":[]}";

            var merged = Merge.PartialCompany(existing, partial);

            Assert.That(existing.Keys, Is.EqualTo(origKeys));
            Assert.That(existing.Traits.Count(), Is.EqualTo(1));

            Assert.That(merged.Keys, Is.EqualTo(new Dictionary<string, string>
            {
                ["domain"] = "example.com",
                ["slug"] = "new-slug"
            }));
            Assert.That(merged.Traits.Count(), Is.EqualTo(0));
        }

        [Test]
        public void PartialCompany_Rules()
        {
            var existing = BaseCompany();
            existing.Rules = new List<RulesengineRule> { MakeRule("rule-old") };

            var partial = @"{""id"":""co-1"",""rules"":[{""id"":""rule-new"",""account_id"":""acc-1"",""environment_id"":""env-1"",""name"":""rule-new"",""priority"":1,""rule_type"":""default"",""value"":true}]}";

            var merged = Merge.PartialCompany(existing, partial);

            Assert.That(merged.Rules.Count(), Is.EqualTo(1));
            Assert.That(merged.Rules.First().Id, Is.EqualTo("rule-new"));
            Assert.That(existing.Rules.First().Id, Is.EqualTo("rule-old"));
        }

        [Test]
        public void PartialCompany_FullEntityPartialMessage()
        {
            var existing = BaseCompany();
            existing.Metrics = new List<RulesengineCompanyMetric>
            {
                new RulesengineCompanyMetric
                {
                    AccountId = "acc-1", EnvironmentId = "env-1", CompanyId = "co-1",
                    EventSubtype = "event-a", Period = RulesengineCompanyMetricPeriod.AllTime,
                    MonthReset = RulesengineCompanyMetricMonthReset.FirstOfMonth,
                    Value = 10, CreatedAt = DateTime.UtcNow
                }
            };
            existing.Rules = new List<RulesengineRule> { MakeRule("rule-1") };

            var partial = @"{
                ""id"": ""co-1"",
                ""account_id"": ""acc-2"",
                ""environment_id"": ""env-2"",
                ""base_plan_id"": ""plan-99"",
                ""billing_product_ids"": [""bp-10"", ""bp-20""],
                ""credit_balances"": {""credit-1"": 999.0, ""credit-new"": 50.0},
                ""entitlements"": [
                    {""feature_id"": ""feat-new"", ""feature_key"": ""feature-new"", ""value_type"": ""boolean""},
                    {""feature_id"": ""feat-2"", ""feature_key"": ""feature-two"", ""value_type"": ""boolean""}
                ],
                ""keys"": {""domain"": ""new.com"", ""slug"": ""new-slug""},
                ""metrics"": [
                    {""account_id"":""acc-1"",""environment_id"":""env-1"",""company_id"":""co-1"",""event_subtype"":""event-a"",""period"":""all_time"",""month_reset"":""first_of_month"",""value"":42,""created_at"":""2026-01-01T00:00:00Z""},
                    {""account_id"":""acc-1"",""environment_id"":""env-1"",""company_id"":""co-1"",""event_subtype"":""event-new"",""period"":""current_day"",""month_reset"":""billing_cycle"",""value"":7,""created_at"":""2026-01-01T00:00:00Z""}
                ],
                ""plan_ids"": [""plan-99"", ""plan-100""],
                ""plan_version_ids"": [""pv-99""],
                ""rules"": [
                    {""id"":""rule-new-1"",""account_id"":""acc-1"",""environment_id"":""env-1"",""name"":""rule-new-1"",""priority"":1,""rule_type"":""default"",""value"":true},
                    {""id"":""rule-new-2"",""account_id"":""acc-1"",""environment_id"":""env-1"",""name"":""rule-new-2"",""priority"":2,""rule_type"":""default"",""value"":false}
                ],
                ""subscription"": {""id"": ""sub-new"", ""period_start"": ""2026-01-01T00:00:00Z"", ""period_end"": ""2027-01-01T00:00:00Z""},
                ""traits"": [
                    {""value"": ""Startup"", ""trait_definition"": {""id"": ""tier"", ""comparable_type"": ""string"", ""entity_type"": ""company""}},
                    {""value"": ""Annual"", ""trait_definition"": {""id"": ""billing"", ""comparable_type"": ""string"", ""entity_type"": ""company""}}
                ]
            }";

            var merged = Merge.PartialCompany(existing, partial);

            Assert.That(merged.Id, Is.EqualTo("co-1"));
            Assert.That(merged.AccountId, Is.EqualTo("acc-2"));
            Assert.That(merged.EnvironmentId, Is.EqualTo("env-2"));

            Assert.That(merged.BasePlanId, Is.Not.Null);
            Assert.That(merged.BasePlanId, Is.EqualTo("plan-99"));

            Assert.That(merged.BillingProductIds.ToList(), Is.EqualTo(new List<string> { "bp-10", "bp-20" }));

            // Credit balances merge: existing credit-1 overwritten, credit-new added
            Assert.That(merged.CreditBalances, Is.EqualTo(new Dictionary<string, double>
            {
                ["credit-1"] = 999.0,
                ["credit-new"] = 50.0
            }));

            var entitlements = merged.Entitlements!.ToList();
            Assert.That(entitlements.Count, Is.EqualTo(2));
            Assert.That(entitlements[0].FeatureId, Is.EqualTo("feat-new"));
            Assert.That(entitlements[0].FeatureKey, Is.EqualTo("feature-new"));
            Assert.That(entitlements[1].FeatureId, Is.EqualTo("feat-2"));
            Assert.That(entitlements[1].FeatureKey, Is.EqualTo("feature-two"));

            // Keys merge: domain overwritten, slug added
            Assert.That(merged.Keys, Is.EqualTo(new Dictionary<string, string>
            {
                ["domain"] = "new.com",
                ["slug"] = "new-slug"
            }));

            // Metrics upsert: event-a updated, event-new appended
            var metrics = merged.Metrics.ToList();
            Assert.That(metrics.Count, Is.EqualTo(2));
            Assert.That(metrics[0].EventSubtype, Is.EqualTo("event-a"));
            Assert.That(metrics[0].Value, Is.EqualTo(42));
            Assert.That(metrics[1].EventSubtype, Is.EqualTo("event-new"));
            Assert.That(metrics[1].Value, Is.EqualTo(7));

            Assert.That(merged.PlanIds.ToList(), Is.EqualTo(new List<string> { "plan-99", "plan-100" }));
            Assert.That(merged.PlanVersionIds.ToList(), Is.EqualTo(new List<string> { "pv-99" }));

            var rules = merged.Rules.ToList();
            Assert.That(rules.Count, Is.EqualTo(2));
            Assert.That(rules[0].Id, Is.EqualTo("rule-new-1"));
            Assert.That(rules[1].Id, Is.EqualTo("rule-new-2"));

            Assert.That(merged.Subscription, Is.Not.Null);
            Assert.That(merged.Subscription!.Id, Is.EqualTo("sub-new"));

            var traits = merged.Traits.ToList();
            Assert.That(traits.Count, Is.EqualTo(2));
            Assert.That(traits[0].Value, Is.EqualTo("Startup"));
            Assert.That(traits[1].Value, Is.EqualTo("Annual"));

            // Original not mutated
            Assert.That(existing.AccountId, Is.EqualTo("acc-1"));
            Assert.That(existing.BasePlanId, Is.EqualTo("plan-1"));
            Assert.That(existing.Keys, Is.EqualTo(new Dictionary<string, string> { ["domain"] = "example.com" }));
            Assert.That(existing.Metrics.First().Value, Is.EqualTo(10));
        }

        // --- PartialUser tests ---

        [Test]
        public void PartialUser_OnlyTraits()
        {
            var existing = BaseUser();
            var partial = @"{""id"":""user-1"",""traits"":[{""value"":""Free"",""trait_definition"":{""id"":""tier"",""comparable_type"":""string"",""entity_type"":""user""}}]}";

            var merged = Merge.PartialUser(existing, partial);

            Assert.That(merged.Traits.Count(), Is.EqualTo(1));
            Assert.That(merged.Traits.First().Value, Is.EqualTo("Free"));
            Assert.That(merged.Keys, Is.EqualTo(new Dictionary<string, string> { ["email"] = "user@example.com" }));
        }

        [Test]
        public void PartialUser_MergesKeys()
        {
            var existing = BaseUser();
            var partial = @"{""id"":""user-1"",""keys"":{""slack_id"":""U123""}}";

            var merged = Merge.PartialUser(existing, partial);

            Assert.That(merged.Keys, Is.EqualTo(new Dictionary<string, string>
            {
                ["email"] = "user@example.com",
                ["slack_id"] = "U123"
            }));
            Assert.That(merged.Traits.Count(), Is.EqualTo(1));
        }

        [Test]
        public void PartialUser_MissingId()
        {
            var existing = BaseUser();
            var partial = @"{""keys"":{""email"":""new@example.com""}}";

            var ex = Assert.Throws<ArgumentException>(() => Merge.PartialUser(existing, partial));
            Assert.That(ex!.Message, Does.Contain("missing required field: id"));
        }

        [Test]
        public void PartialUser_DoesNotMutateOriginal()
        {
            var existing = BaseUser();
            var origKeys = new Dictionary<string, string>(existing.Keys);

            var partial = @"{""id"":""user-1"",""keys"":{""slug"":""new""},""traits"":[]}";

            var merged = Merge.PartialUser(existing, partial);

            Assert.That(existing.Keys, Is.EqualTo(origKeys));
            Assert.That(existing.Traits.Count(), Is.EqualTo(1));

            Assert.That(merged.Keys, Is.EqualTo(new Dictionary<string, string>
            {
                ["email"] = "user@example.com",
                ["slug"] = "new"
            }));
            Assert.That(merged.Traits.Count(), Is.EqualTo(0));
        }

        [Test]
        public void PartialUser_FullEntityPartialMessage()
        {
            var existing = BaseUser();
            existing.Rules = new List<RulesengineRule> { MakeRule("rule-1") };

            var partial = @"{
                ""id"": ""user-1"",
                ""account_id"": ""acc-2"",
                ""environment_id"": ""env-2"",
                ""keys"": {""email"": ""new@example.com"", ""slack_id"": ""U999""},
                ""traits"": [
                    {""value"": ""Free"", ""trait_definition"": {""id"": ""tier"", ""comparable_type"": ""string"", ""entity_type"": ""user""}},
                    {""value"": ""Monthly"", ""trait_definition"": {""id"": ""billing"", ""comparable_type"": ""string"", ""entity_type"": ""user""}}
                ],
                ""rules"": [
                    {""id"":""rule-new-1"",""account_id"":""acc-1"",""environment_id"":""env-1"",""name"":""rule-new-1"",""priority"":1,""rule_type"":""default"",""value"":true},
                    {""id"":""rule-new-2"",""account_id"":""acc-1"",""environment_id"":""env-1"",""name"":""rule-new-2"",""priority"":2,""rule_type"":""default"",""value"":false}
                ]
            }";

            var merged = Merge.PartialUser(existing, partial);

            Assert.That(merged.Id, Is.EqualTo("user-1"));
            Assert.That(merged.AccountId, Is.EqualTo("acc-2"));
            Assert.That(merged.EnvironmentId, Is.EqualTo("env-2"));

            // Keys merge: email overwritten, slack_id added
            Assert.That(merged.Keys, Is.EqualTo(new Dictionary<string, string>
            {
                ["email"] = "new@example.com",
                ["slack_id"] = "U999"
            }));

            var traits = merged.Traits.ToList();
            Assert.That(traits.Count, Is.EqualTo(2));
            Assert.That(traits[0].Value, Is.EqualTo("Free"));
            Assert.That(traits[1].Value, Is.EqualTo("Monthly"));

            var rules = merged.Rules.ToList();
            Assert.That(rules.Count, Is.EqualTo(2));
            Assert.That(rules[0].Id, Is.EqualTo("rule-new-1"));
            Assert.That(rules[1].Id, Is.EqualTo("rule-new-2"));

            // Original not mutated
            Assert.That(existing.AccountId, Is.EqualTo("acc-1"));
            Assert.That(existing.Keys, Is.EqualTo(new Dictionary<string, string> { ["email"] = "user@example.com" }));
            Assert.That(existing.Traits.Count(), Is.EqualTo(1));
            Assert.That(existing.Rules.First().Id, Is.EqualTo("rule-1"));
        }

        // --- ExtractIdFromJson tests ---

        [Test]
        public void ExtractIdFromJson_ValidJson()
        {
            var id = Merge.ExtractIdFromJson(@"{""id"":""co-1"",""traits"":[]}");
            Assert.That(id, Is.EqualTo("co-1"));
        }

        [Test]
        public void ExtractIdFromJson_MissingId()
        {
            var ex = Assert.Throws<ArgumentException>(() => Merge.ExtractIdFromJson(@"{""traits"":[]}"));
            Assert.That(ex!.Message, Does.Contain("missing required field: id"));
        }

        // --- DeepCopyCompany tests ---

        [Test]
        public void DeepCopyCompany_FullCopy()
        {
            var validUntil = new DateTime(2026, 4, 1, 0, 0, 0, DateTimeKind.Utc);
            var orig = new RulesengineCompany
            {
                Id = "co-1",
                AccountId = "acc-1",
                EnvironmentId = "env-1",
                BasePlanId = "plan-1",
                BillingProductIds = new List<string> { "bp-1", "bp-2" },
                CreditBalances = new Dictionary<string, double> { ["credit-1"] = 50.0 },
                Keys = new Dictionary<string, string> { ["domain"] = "example.com" },
                PlanIds = new List<string> { "plan-1" },
                PlanVersionIds = new List<string> { "pv-1" },
                Subscription = new RulesengineSubscription
                {
                    Id = "sub-1",
                    PeriodStart = DateTime.UtcNow,
                    PeriodEnd = DateTime.UtcNow.AddMonths(1)
                },
                Entitlements = new List<RulesengineFeatureEntitlement>
                {
                    new RulesengineFeatureEntitlement
                    {
                        FeatureId = "f1",
                        FeatureKey = "feature-one",
                        ValueType = RulesengineEntitlementValueType.Boolean
                    }
                },
                Rules = new List<RulesengineRule> { MakeRule("r1") },
                Metrics = new List<RulesengineCompanyMetric>
                {
                    new RulesengineCompanyMetric
                    {
                        AccountId = "acc-1", EnvironmentId = "env-1", CompanyId = "co-1",
                        EventSubtype = "event-1", Period = RulesengineCompanyMetricPeriod.AllTime,
                        MonthReset = RulesengineCompanyMetricMonthReset.FirstOfMonth,
                        Value = 42, CreatedAt = DateTime.UtcNow, ValidUntil = validUntil
                    }
                },
                Traits = new List<RulesengineTrait>
                {
                    new RulesengineTrait
                    {
                        Value = "Enterprise",
                        TraitDefinition = new RulesengineTraitDefinition
                        {
                            Id = "plan",
                            ComparableType = RulesengineTraitDefinitionComparableType.String,
                            EntityType = RulesengineEntityType.Company
                        }
                    }
                }
            };

            var cp = Merge.DeepCopyCompany(orig);

            Assert.That(cp.Id, Is.EqualTo(orig.Id));
            Assert.That(cp.AccountId, Is.EqualTo(orig.AccountId));
            Assert.That(cp.EnvironmentId, Is.EqualTo(orig.EnvironmentId));
            Assert.That(cp.BasePlanId, Is.EqualTo(orig.BasePlanId));

            // Slices are independent
            ((List<string>)cp.BillingProductIds)[0] = "changed";
            Assert.That(orig.BillingProductIds.First(), Is.EqualTo("bp-1"));

            ((List<string>)cp.PlanIds)[0] = "changed";
            Assert.That(orig.PlanIds.First(), Is.EqualTo("plan-1"));

            ((List<string>)cp.PlanVersionIds)[0] = "changed";
            Assert.That(orig.PlanVersionIds.First(), Is.EqualTo("pv-1"));

            // Maps are independent
            cp.Keys["domain"] = "changed.com";
            Assert.That(orig.Keys["domain"], Is.EqualTo("example.com"));

            cp.CreditBalances["credit-1"] = 999;
            Assert.That(orig.CreditBalances["credit-1"], Is.EqualTo(50.0));

            // Subscription deep copied
            Assert.That(cp.Subscription, Is.Not.Null);
            Assert.That(cp.Subscription!.Id, Is.EqualTo("sub-1"));
            cp.Subscription.Id = "changed";
            Assert.That(orig.Subscription!.Id, Is.EqualTo("sub-1"));

            // Metrics deep copied
            var cpMetrics = cp.Metrics.ToList();
            Assert.That(cpMetrics.Count, Is.EqualTo(1));
            Assert.That(cpMetrics[0].Value, Is.EqualTo(42));
            Assert.That(cpMetrics[0].ValidUntil, Is.EqualTo(validUntil));
            cpMetrics[0].Value = 999;
            Assert.That(orig.Metrics.First().Value, Is.EqualTo(42));

            // Traits deep copied
            var cpTraits = cp.Traits.ToList();
            Assert.That(cpTraits.Count, Is.EqualTo(1));
            Assert.That(cpTraits[0].Value, Is.EqualTo("Enterprise"));
            cpTraits[0].Value = "changed";
            Assert.That(orig.Traits.First().Value, Is.EqualTo("Enterprise"));
        }

        [Test]
        public void DeepCopyUser_EmptyFields()
        {
            var cp = Merge.DeepCopyUser(new RulesengineUser
            {
                Id = "u1",
                AccountId = "acc-1",
                EnvironmentId = "env-1"
            });
            Assert.That(cp.Id, Is.EqualTo("u1"));
            Assert.That(cp.Keys.Count, Is.EqualTo(0));
            Assert.That(cp.Traits.Count(), Is.EqualTo(0));
            Assert.That(cp.Rules.Count(), Is.EqualTo(0));
        }

        [Test]
        public void DeepCopyUser_FullCopy()
        {
            var orig = new RulesengineUser
            {
                Id = "user-1",
                AccountId = "acc-1",
                EnvironmentId = "env-1",
                Keys = new Dictionary<string, string> { ["email"] = "a@b.com" },
                Traits = new List<RulesengineTrait>
                {
                    new RulesengineTrait { Value = "Premium" }
                },
                Rules = new List<RulesengineRule> { MakeRule("r1") }
            };

            var cp = Merge.DeepCopyUser(orig);

            Assert.That(cp.Id, Is.EqualTo(orig.Id));
            Assert.That(cp.AccountId, Is.EqualTo(orig.AccountId));

            cp.Keys["email"] = "changed";
            Assert.That(orig.Keys["email"], Is.EqualTo("a@b.com"));

            // Traits and Rules are shallow copies of the list (new list, same references)
            // Modifying the list itself shouldn't affect original
            var cpTraits = cp.Traits.ToList();
            cpTraits[0] = new RulesengineTrait { Value = "Free" };
            // Original list is untouched since we copied the list
            Assert.That(orig.Traits.First().Value, Is.EqualTo("Premium"));

            var cpRules = cp.Rules.ToList();
            cpRules[0] = MakeRule("r2");
            Assert.That(orig.Rules.First().Id, Is.EqualTo("r1"));
        }
    }
}
