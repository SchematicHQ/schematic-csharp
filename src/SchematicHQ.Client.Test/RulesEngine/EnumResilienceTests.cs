using System;
using System.Text.Json;
using NUnit.Framework;
using SchematicHQ.Client.Cache;
using SchematicHQ.Client.RulesEngine;
using SchematicHQ.Client.RulesEngine.Utils;

namespace SchematicHQ.Client.Test.RulesEngine
{
    [TestFixture]
    public class EnumResilienceTests
    {
        private JsonSerializerOptions _options;

        [SetUp]
        public void Setup()
        {
            _options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
                Converters = {
                    new ResilientEnumConverter()
                }
            };
        }

        [Test]
        public void RuleType_UnknownValue_ShouldFallbackToUnknown()
        {
            // Arrange
            string unknownRuleTypeJson = "\"unknown_rule_type\"";

            // Act & Assert
            Assert.DoesNotThrow(() =>
            {
                var result = JsonSerializer.Deserialize<RulesengineRuleType>(unknownRuleTypeJson, _options);
                Assert.That(result.Value, Is.EqualTo("unknown_rule_type")); // Should preserve unknown value
            });
        }

        [Test]
        public void RuleType_ValidValue_ShouldParseCorrectly()
        {
            // Arrange
            string validRuleTypeJson = "\"standard\"";

            // Act
            var result = JsonSerializer.Deserialize<RulesengineRuleType>(validRuleTypeJson, _options);

            // Assert
            Assert.That(result, Is.EqualTo(RulesengineRuleType.Standard));
        }

        [Test]
        public void RuleType_SnakeCaseValue_ShouldParseCorrectly()
        {
            // Arrange
            string snakeCaseRuleTypeJson = "\"global_override\"";

            // Act
            var result = JsonSerializer.Deserialize<RulesengineRuleType>(snakeCaseRuleTypeJson, _options);

            // Assert
            Assert.That(result, Is.EqualTo(RulesengineRuleType.GlobalOverride));
        }

        [Test]
        public void RuleType_InvalidValue_ShouldFallbackToUnknown()
        {
            // Act & Assert
            Assert.DoesNotThrow(() =>
            {
                var result = JsonSerializer.Deserialize<RulesengineRuleType>("\"invalid_rule\"", _options);
                Assert.That(result.Value, Is.EqualTo("invalid_rule")); // Should preserve unknown value
            });
        }

        [Test]
        public void RuleType_NonexistentValue_ShouldFallbackToUnknown()
        {
            // Act & Assert
            Assert.DoesNotThrow(() =>
            {
                var result = JsonSerializer.Deserialize<RulesengineRuleType>("\"nonexistent\"", _options);
                Assert.That(result.Value, Is.EqualTo("nonexistent")); // Should preserve unknown value
            });
        }

        [Test]
        public void RuleType_EmptyValue_ShouldFallbackToUnknown()
        {
            // Act & Assert
            Assert.DoesNotThrow(() =>
            {
                var result = JsonSerializer.Deserialize<RulesengineRuleType>("\"\"", _options);
                Assert.That(result.Value, Is.EqualTo("")); // Empty string is preserved
            });
        }

        [Test]
        public void RuleType_Serialization_ShouldUseSnakeCase()
        {
            // Arrange
            var ruleType = RulesengineRuleType.GlobalOverride;

            // Act
            var json = JsonSerializer.Serialize(ruleType, _options);

            // Assert
            Assert.That(json, Is.EqualTo("\"global_override\""));
        }

        [Test]
        public void RuleType_UnknownSerialization_ShouldSerializeAsEmptyString()
        {
            // Arrange
            var ruleType = RulesengineRuleType.FromCustom("");

            // Act
            var json = JsonSerializer.Serialize(ruleType, _options);

            // Assert
            Assert.That(json, Is.EqualTo("\"\""));
        }
    }
}
