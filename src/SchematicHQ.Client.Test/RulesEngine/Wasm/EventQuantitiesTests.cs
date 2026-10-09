using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Core;
using SchematicHQ.Client.RulesEngine;

namespace SchematicHQ.Client.Test.RulesEngine.Wasm
{
    /// <summary>
    /// The event_quantities preflight and the quantity_rates it prices from, run through the
    /// WASM engine. Flags, companies and preflights are read from snake_case JSON, as DataStream
    /// and the API send them, so a field the generated models would drop fails here.
    ///
    /// <para>The generated <see cref="PreflightRequestBody"/> has no event_quantities property
    /// until the API's preflight body carries it and Fern regenerates; until then it rides in the
    /// body's extension data, which is enough to pin that the engine path forwards it.</para>
    /// </summary>
    [TestFixture]
    public class EventQuantitiesTests
    {
        private const string Subtype = "chat";
        private const string CreditId = "credit-abc";
        private static readonly JsonObject Rates = new() { ["input_tokens"] = 0.001, ["output_tokens"] = 0.01 };

        // 1 request x 0.5 + (1000 - 400 cached) x 0.001 + 100 x 0.01 = 2.1. The cached tokens are
        // unrated, so they cost nothing but still come out of input.
        private static readonly JsonObject Quantities = new()
        {
            ["input_tokens"] = 1000,
            ["cached_input_tokens"] = 400,
            ["output_tokens"] = 100,
        };

        private WasmRulesEngine _engine = null!;

        [OneTimeSetUp]
        public void SetUp()
        {
            _engine = new WasmRulesEngine(NullLogger.Instance);
            _engine.Initialize();
        }

        [OneTimeTearDown]
        public void TearDown() => _engine.Dispose();

        /// <summary>
        /// A single credit-balance rule priced like an inference entitlement: requests at the
        /// consumption rate, tokens at their own rates.
        /// </summary>
        private static RulesengineFlag InferenceFlag(double consumptionRate, JsonNode? quantityRates)
        {
            var condition = new JsonObject
            {
                ["id"] = "cond-1",
                ["account_id"] = "acct",
                ["environment_id"] = "env",
                ["condition_type"] = "credit",
                ["operator"] = "lt",
                ["resource_ids"] = new JsonArray(),
                ["trait_value"] = "",
                ["credit_id"] = CreditId,
                ["consumption_rate"] = consumptionRate,
                ["event_subtype"] = Subtype,
            };
            if (quantityRates != null)
            {
                condition["quantity_rates"] = quantityRates.DeepClone();
            }
            var flag = new JsonObject
            {
                ["id"] = "flag-1",
                ["account_id"] = "acct",
                ["environment_id"] = "env",
                ["key"] = "chat",
                ["default_value"] = false,
                ["rules"] = new JsonArray
                {
                    new JsonObject
                    {
                        ["id"] = "rule-1",
                        ["account_id"] = "acct",
                        ["environment_id"] = "env",
                        ["name"] = "Credits",
                        ["rule_type"] = "plan_entitlement",
                        ["priority"] = 0,
                        ["value"] = true,
                        ["conditions"] = new JsonArray { condition },
                        ["condition_groups"] = new JsonArray(),
                    },
                },
            };
            return JsonUtils.Deserialize<RulesengineFlag>(flag.ToJsonString());
        }

        private static JsonObject CompanyNode(double balance) =>
            new()
            {
                ["id"] = "co",
                ["account_id"] = "acct",
                ["environment_id"] = "env",
                ["keys"] = new JsonObject { ["id"] = "co" },
                ["credit_balances"] = new JsonObject { [CreditId] = balance },
                ["billing_product_ids"] = new JsonArray(),
                ["crm_product_ids"] = new JsonArray(),
                ["plan_ids"] = new JsonArray(),
                ["plan_version_ids"] = new JsonArray(),
                ["metrics"] = new JsonArray(),
                ["traits"] = new JsonArray(),
                ["rules"] = new JsonArray(),
            };

        private static RulesengineCompany Company(double balance) =>
            JsonUtils.Deserialize<RulesengineCompany>(CompanyNode(balance).ToJsonString());

        private static PreflightRequestBody Preflight(JsonObject options) =>
            JsonUtils.Deserialize<PreflightRequestBody>(options.ToJsonString());

        private static PreflightRequestBody EventQuantities(string subtype, double? quantity, JsonNode? quantities)
        {
            var eventQuantities = new JsonObject { ["event_subtype"] = subtype };
            if (quantity.HasValue)
            {
                eventQuantities["quantity"] = quantity.Value;
            }
            if (quantities != null)
            {
                eventQuantities["quantities"] = quantities.DeepClone();
            }
            return Preflight(new JsonObject { ["event_quantities"] = eventQuantities });
        }

        private CheckFlagResult Check(double balance, PreflightRequestBody preflight) =>
            _engine.CheckFlag(Company(balance), null, InferenceFlag(0.5, Rates), preflight);

        [Test]
        public void PassesWhenTheBalanceCoversTheCall()
        {
            var result = Check(2.1, EventQuantities(Subtype, null, Quantities));

            Assert.That(result.RuleId, Is.EqualTo("rule-1"));
            Assert.That(result.Value, Is.True);
        }

        [Test]
        public void RefusesWhenTheBalanceFallsShort()
        {
            // Covers the request and the legacy single unit, not the tokens.
            var result = Check(2.0, EventQuantities(Subtype, null, Quantities));

            Assert.That(result.RuleId, Is.Null);
            Assert.That(result.Value, Is.False);
        }

        [Test]
        public void IgnoredForAnotherSubtype()
        {
            var other = EventQuantities("other", null, new JsonObject { ["input_tokens"] = 1e6 });

            Assert.That(Check(1.0, other).RuleId, Is.EqualTo("rule-1"));
        }

        [Test]
        public void QuantityMultipliesTheBaseNotTheQuantities()
        {
            // 3 x 0.5 + 600 x 0.001 + 100 x 0.01 = 3.1.
            var preflight = EventQuantities(Subtype, 3, Quantities);

            Assert.That(Check(3.1, preflight).RuleId, Is.EqualTo("rule-1"));
            Assert.That(Check(3.0, preflight).RuleId, Is.Null);
        }

        [Test]
        public void EventQuantitiesBeatsEventUsage()
        {
            // event_usage alone would ask 1 x 0.5, which 2.0 covers.
            var preflight = Preflight(
                new JsonObject
                {
                    ["event_usage"] = new JsonObject { ["event_subtype"] = Subtype, ["quantity"] = 1 },
                    ["event_quantities"] = new JsonObject
                    {
                        ["event_subtype"] = Subtype,
                        ["quantities"] = Quantities.DeepClone(),
                    },
                });

            Assert.That(Check(2.0, preflight).RuleId, Is.Null);
        }

        [Test]
        public void AnEmptyCreditCostStillDeclaresNothing()
        {
            var result = _engine.CheckFlag(
                Company(1.0),
                null,
                InferenceFlag(0.5, Rates),
                new PreflightRequestBody { CreditCost = new Dictionary<string, double>() });

            // 1.0 covers the legacy single-unit check at 0.5.
            Assert.That(result.RuleId, Is.EqualTo("rule-1"));
        }

        [Test]
        public void ACompanyEntitlementsQuantityRatesReachTheResult()
        {
            var company = CompanyNode(0);
            company["entitlements"] = new JsonArray
            {
                new JsonObject
                {
                    ["feature_id"] = "feat-1",
                    ["feature_key"] = "chat",
                    ["value_type"] = "credit",
                    ["quantity_rates"] = Rates.DeepClone(),
                },
            };

            var result = _engine.CheckFlag(
                JsonUtils.Deserialize<RulesengineCompany>(company.ToJsonString()),
                null,
                InferenceFlag(0.5, null));

            Assert.That(result.Entitlement, Is.Not.Null);
            var rates = result.Entitlement!.AdditionalProperties["quantity_rates"];
            Assert.That(rates.GetProperty("input_tokens").GetDouble(), Is.EqualTo(0.001));
            Assert.That(rates.GetProperty("output_tokens").GetDouble(), Is.EqualTo(0.01));
        }

        private static IEnumerable<TestCaseData> FixtureCases()
        {
            // quantity_cost.json is copied verbatim from schematic-api's
            // api/lib/rulesengine/testdata/quantity_cost.json. The API's burn and the engine both
            // price every case there; running them through the WASM engine here pins that this
            // SDK's wire shape for quantity_rates and event_quantities reaches that pricing intact.
            var path = Path.Combine(
                TestContext.CurrentContext.TestDirectory,
                "RulesEngine",
                "Wasm",
                "testdata",
                "quantity_cost.json");
            var fixture = JsonNode.Parse(File.ReadAllText(path))!;
            foreach (var tc in fixture["cases"]!.AsArray())
            {
                var quantity = tc!["quantity"]?.GetValue<double>();
                var quantities = tc["quantities"]?.AsObject();
                // The preflight rejects negative quantities before pricing.
                if (quantity < 0 || (quantities?.Any(q => q.Value!.GetValue<double>() < 0) ?? false))
                {
                    continue;
                }
                yield return new TestCaseData(tc.ToJsonString()).SetName(tc["name"]!.GetValue<string>());
            }
        }

        [TestCaseSource(nameof(FixtureCases))]
        public void SharedQuantityCostFixture(string caseJson)
        {
            var tc = JsonNode.Parse(caseJson)!;
            var flag = InferenceFlag(tc["consumption_rate"]!.GetValue<double>(), tc["quantity_rates"]);
            var preflight = EventQuantities(Subtype, tc["quantity"]?.GetValue<double>(), tc["quantities"]);
            var cost = tc["expected_cost"]!.GetValue<double>();

            // A cost priced to zero gates on balance > 0, so the smallest positive balance passes
            // and zero does not.
            var covers = cost * (1 + 1e-9) + 1e-9;
            var shortOf = cost == 0 ? 0 : cost * (1 - 1e-6);

            Assert.That(
                _engine.CheckFlag(Company(covers), null, flag, preflight).RuleId,
                Is.EqualTo("rule-1"),
                $"balance {covers} should cover cost {cost}");
            Assert.That(
                _engine.CheckFlag(Company(shortOf), null, flag, preflight).RuleId,
                Is.Null,
                $"balance {shortOf} should not cover cost {cost}");
        }
    }
}
