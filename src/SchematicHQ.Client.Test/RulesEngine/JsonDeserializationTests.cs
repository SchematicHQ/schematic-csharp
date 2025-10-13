using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using NUnit.Framework;
using SchematicHQ.Client.RulesEngine.Models;
using SchematicHQ.Client.RulesEngine.Utils;
using SchematicHQ.Client.RulesEngine;
using SchematicHQ.Client.Cache;

namespace SchematicHQ.Client.Test.RulesEngine
{
    [TestFixture]
    public class JsonDeserializationTests
    {
        private JsonSerializerOptions _jsonOptions = null!;

        [SetUp]
        public void SetUp()
        {
            _jsonOptions = new JsonSerializerOptions 
            { 
                WriteIndented = false,
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                Converters = { 
                    new ComparableTypeConverter(),
                    new ResilientEnumConverter(),
                    new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower, true)
                }
            };
        }

        [Test]
        public void Flag_WithRuleTypeEnum_ShouldDeserializeCorrectly()
        {
            // Arrange
            var flagJson = @"{
  ""id"": ""flag_6SB8FaJPR8C"",
  ""account_id"": ""acct_cns2asuKAG2"",
  ""environment_id"": ""env_cns2asuKAG2"",
  ""key"": ""analyze-clicks"",
  ""rules"": [
    {
      ""id"": ""rule_QBiTjS4E6db"",
      ""flag_id"": ""flag_6SB8FaJPR8C"",
      ""account_id"": ""acct_cns2asuKAG2"",
      ""environment_id"": ""env_cns2asuKAG2"",
      ""rule_type"": ""plan_entitlement"",
      ""name"": ""Plan Entitlement - true"",
      ""priority"": 0,
      ""conditions"": [
        {
          ""id"": ""cond_5fyBPuAE8wa"",
          ""account_id"": ""acct_cns2asuKAG2"",
          ""environment_id"": ""env_cns2asuKAG2"",
          ""condition_type"": ""plan"",
          ""operator"": ""eq"",
          ""resource_ids"": [
            ""plan_gkUv5TouNCQ""
          ],
          ""event_subtype"": null,
          ""metric_value"": null,
          ""metric_period"": null,
          ""metric_period_month_reset"": null,
          ""credit_id"": null,
          ""consumption_rate"": null,
          ""trait_definition"": null,
          ""trait_value"": """",
          ""comparison_trait_definition"": null
        }
      ],
      ""condition_groups"": null,
      ""value"": true
    }
  ],
  ""default_value"": false
}";

            // Act
            var flag = JsonSerializer.Deserialize<Flag>(flagJson, _jsonOptions);

            // Assert
            Assert.That(flag, Is.Not.Null);
            Assert.That(flag!.Id, Is.EqualTo("flag_6SB8FaJPR8C"));
            Assert.That(flag.Key, Is.EqualTo("analyze-clicks"));
            Assert.That(flag.Rules, Has.Count.EqualTo(1));
            Assert.That(flag.Rules[0].RuleType, Is.EqualTo(RuleType.PlanEntitlement));
        }

        [Test]
        public void Company_WithComparableTypeEnums_ShouldDeserializeCorrectly()
        {
            // Arrange
            var companyJson = @"{
    ""id"": ""comp_DopLPhkmGHU"",
    ""account_id"": """",
    ""environment_id"": ""env_cns2asuKAG2"",
    ""base_plan_id"": ""plan_fuNSdNd3qym"",
    ""billing_product_ids"": [],
    ""crm_product_ids"": null,
    ""keys"": {
        ""clerk_id"": ""org_2qJAWIjMZyEvs01LD74D9RLJ4Jz"",
        ""clerkid"": ""org_2qJAWIjMZyEvs01LD74D9RLJ4Jz"",
        ""id"": ""12345"",
        ""stripe_customer_id"": ""cus_RyiYY4MzKSZjCM""
    },
    ""plan_ids"": [
        ""plan_fuNSdNd3qym""
    ],
    ""metrics"": null,
    ""credit_balances"": {},
    ""subscription"": null,
    ""traits"": [
        {
            ""trait_definition"": {
                ""id"": ""count"",
                ""comparable_type"": """",
                ""entity_type"": ""company""
            },
            ""value"": ""9""
        },
        {
            ""trait_definition"": {
                ""id"": ""plan"",
                ""comparable_type"": """",
                ""entity_type"": ""company""
            },
            ""value"": ""pro""
        },
        {
            ""trait_definition"": {
                ""id"": ""url"",
                ""comparable_type"": """",
                ""entity_type"": ""company""
            },
            ""value"": """"
        }
    ],
    ""rules"": null
}";

            // Act
            var company = JsonSerializer.Deserialize<Company>(companyJson, _jsonOptions);

            // Assert
            Assert.That(company, Is.Not.Null);
            Assert.That(company!.Id, Is.EqualTo("comp_DopLPhkmGHU"));
            Assert.That(company.Traits, Has.Count.EqualTo(3));
            
            // Check that empty string comparable_type is converted to Unknown
            foreach (var trait in company.Traits)
            {
                Assert.That(trait.TraitDefinition?.ComparableType, Is.EqualTo(ComparableType.Unknown));
            }
        }

        [Test]
        [TestCase("", ComparableType.Unknown)]
        [TestCase("string", ComparableType.String)]
        [TestCase("int", ComparableType.Int)]
        [TestCase("bool", ComparableType.Bool)]
        [TestCase("date", ComparableType.Date)]
        public void ComparableType_ShouldDeserializeCorrectly(string jsonValue, ComparableType expectedEnum)
        {
            // Arrange
            var json = $@"{{""comparable_type"": ""{jsonValue}""}}";

            // Act
            var testObj = JsonSerializer.Deserialize<TestComparableTypeObject>(json, _jsonOptions);

            // Assert
            Assert.That(testObj!.ComparableType, Is.EqualTo(expectedEnum));
        }

        [Test]
        [TestCase("plan_entitlement", RuleType.PlanEntitlement)]
        [TestCase("global_override", RuleType.GlobalOverride)]
        [TestCase("company_override", RuleType.CompanyOverride)]
        [TestCase("standard", RuleType.Standard)]
        [TestCase("default", RuleType.Default)]
        public void RuleType_ShouldDeserializeCorrectly(string jsonValue, RuleType expectedEnum)
        {
            // Arrange
            var json = $@"{{""rule_type"": ""{jsonValue}""}}";

            // Act
            var testObj = JsonSerializer.Deserialize<TestRuleTypeObject>(json, _jsonOptions);

            // Assert
            Assert.That(testObj!.RuleType, Is.EqualTo(expectedEnum));
        }

        [Test]
        [TestCase("unknown_value")] 
        [TestCase("")]
        [TestCase("invalid")]
        public void RuleType_WithUnknownValue_ShouldFallbackToFirstEnumValue(string jsonValue)
        {
            // Arrange
            var json = $@"{{""rule_type"": ""{jsonValue}""}}";

            // Act & Assert - Should not throw, should fallback to first enum value
            Assert.DoesNotThrow(() =>
            {
                var testObj = JsonSerializer.Deserialize<TestRuleTypeObject>(json, _jsonOptions);
                // First enum value should be used as fallback
                var firstEnumValue = Enum.GetValues<RuleType>()[0];
                Assert.That(testObj!.RuleType, Is.EqualTo(firstEnumValue));
            });
        }

        // Test helper classes
        private class TestComparableTypeObject
        {
            [JsonPropertyName("comparable_type")]
            public ComparableType ComparableType { get; set; }
        }

        private class TestRuleTypeObject
        {
            [JsonPropertyName("rule_type")]
            public RuleType RuleType { get; set; }
        }
    }
}