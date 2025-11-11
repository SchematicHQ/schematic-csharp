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
            Assert.That(flag.Rules[0].RuleType, Is.EqualTo(RuleRuleType.PlanEntitlement));
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
            
            // Check that empty string comparable_type is handled correctly
            foreach (var trait in company.Traits)
            {
                // Empty string should create a TraitDefinitionComparableType with empty string value
                Assert.That(trait.TraitDefinition?.ComparableType.Value, Is.EqualTo(""));
            }
        }

        [Test]
        public void TraitDefinitionComparableType_EmptyString_ShouldDeserializeToEmptyValue()
        {
            // Arrange
            var json = @"{""comparable_type"": """"}";

            // Act
            var testObj = JsonSerializer.Deserialize<TestTraitDefinitionComparableTypeObject>(json, _jsonOptions);

            // Assert
            Assert.That(testObj!.ComparableType.Value, Is.EqualTo(""));
        }

        [Test]
        public void TraitDefinitionComparableType_String_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = @"{""comparable_type"": ""string""}";

            // Act
            var testObj = JsonSerializer.Deserialize<TestTraitDefinitionComparableTypeObject>(json, _jsonOptions);

            // Assert
            Assert.That(testObj!.ComparableType, Is.EqualTo(TraitDefinitionComparableType.String));
        }

        [Test]
        public void TraitDefinitionComparableType_Int_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = @"{""comparable_type"": ""int""}";

            // Act
            var testObj = JsonSerializer.Deserialize<TestTraitDefinitionComparableTypeObject>(json, _jsonOptions);

            // Assert
            Assert.That(testObj!.ComparableType, Is.EqualTo(TraitDefinitionComparableType.Int));
        }

        [Test]
        public void TraitDefinitionComparableType_Bool_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = @"{""comparable_type"": ""bool""}";

            // Act
            var testObj = JsonSerializer.Deserialize<TestTraitDefinitionComparableTypeObject>(json, _jsonOptions);

            // Assert
            Assert.That(testObj!.ComparableType, Is.EqualTo(TraitDefinitionComparableType.Bool));
        }

        [Test]
        public void TraitDefinitionComparableType_Date_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = @"{""comparable_type"": ""date""}";

            // Act
            var testObj = JsonSerializer.Deserialize<TestTraitDefinitionComparableTypeObject>(json, _jsonOptions);

            // Assert
            Assert.That(testObj!.ComparableType, Is.EqualTo(TraitDefinitionComparableType.Date));
        }

        [Test]
        public void RuleType_PlanEntitlement_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = @"{""rule_type"": ""plan_entitlement""}";

            // Act
            var testObj = JsonSerializer.Deserialize<TestRuleTypeObject>(json, _jsonOptions);

            // Assert
            Assert.That(testObj!.RuleType, Is.EqualTo(RuleRuleType.PlanEntitlement));
        }

        [Test]
        public void RuleType_GlobalOverride_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = @"{""rule_type"": ""global_override""}";

            // Act
            var testObj = JsonSerializer.Deserialize<TestRuleTypeObject>(json, _jsonOptions);

            // Assert
            Assert.That(testObj!.RuleType, Is.EqualTo(RuleRuleType.GlobalOverride));
        }

        [Test]
        public void RuleType_CompanyOverride_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = @"{""rule_type"": ""company_override""}";

            // Act
            var testObj = JsonSerializer.Deserialize<TestRuleTypeObject>(json, _jsonOptions);

            // Assert
            Assert.That(testObj!.RuleType, Is.EqualTo(RuleRuleType.CompanyOverride));
        }

        [Test]
        public void RuleType_Standard_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = @"{""rule_type"": ""standard""}";

            // Act
            var testObj = JsonSerializer.Deserialize<TestRuleTypeObject>(json, _jsonOptions);

            // Assert
            Assert.That(testObj!.RuleType, Is.EqualTo(RuleRuleType.Standard));
        }

        [Test]
        public void RuleType_Default_ShouldDeserializeCorrectly()
        {
            // Arrange
            var json = @"{""rule_type"": ""default""}";

            // Act
            var testObj = JsonSerializer.Deserialize<TestRuleTypeObject>(json, _jsonOptions);

            // Assert
            Assert.That(testObj!.RuleType, Is.EqualTo(RuleRuleType.Default));
        }

        [Test]
        public void RuleType_WithUnknownValue_ShouldCreateCustomEnumValue()
        {
            // Arrange
            var json = @"{""rule_type"": ""unknown_value""}";

            // Act & Assert - Should not throw, should create custom enum value
            Assert.DoesNotThrow(() =>
            {
                var testObj = JsonSerializer.Deserialize<TestRuleTypeObject>(json, _jsonOptions);
                // Should create custom enum value for unrecognized values
                Assert.That(testObj!.RuleType.Value, Is.EqualTo("unknown_value"));
            });
        }

        [Test]
        public void RuleType_WithEmptyValue_ShouldCreateCustomEnumValue()
        {
            // Arrange
            var json = @"{""rule_type"": """"}";

            // Act & Assert - Should not throw, should create custom enum value
            Assert.DoesNotThrow(() =>
            {
                var testObj = JsonSerializer.Deserialize<TestRuleTypeObject>(json, _jsonOptions);
                // Should create custom enum value for empty values
                Assert.That(testObj!.RuleType.Value, Is.EqualTo(""));
            });
        }

        [Test]
        public void RuleType_WithInvalidValue_ShouldCreateCustomEnumValue()
        {
            // Arrange
            var json = @"{""rule_type"": ""invalid""}";

            // Act & Assert - Should not throw, should create custom enum value
            Assert.DoesNotThrow(() =>
            {
                var testObj = JsonSerializer.Deserialize<TestRuleTypeObject>(json, _jsonOptions);
                // Should create custom enum value for invalid values
                Assert.That(testObj!.RuleType.Value, Is.EqualTo("invalid"));
            });
        }

        // Test helper classes
        private class TestComparableTypeObject
        {
            [JsonPropertyName("comparable_type")]
            public ComparableType ComparableType { get; set; }
        }

        private class TestTraitDefinitionComparableTypeObject
        {
            [JsonPropertyName("comparable_type")]
            public TraitDefinitionComparableType ComparableType { get; set; }
        }

        private class TestRuleTypeObject
        {
            [JsonPropertyName("rule_type")]
            public RuleRuleType RuleType { get; set; }
        }
    }
}