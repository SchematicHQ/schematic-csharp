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
                    new ResilientEnumConverter(),
                    new ComparableTypeConverter()
                }
            };
        }

        [Test]
        public void RuleType_UnknownValue_ShouldFallbackToDefault()
        {
            // Arrange
            string unknownRuleTypeJson = "\"unknown_rule_type\"";

            // Act & Assert
            Assert.DoesNotThrow(() =>
            {
                var result = JsonSerializer.Deserialize<RuleType>(unknownRuleTypeJson, _options);
                Assert.That(result, Is.EqualTo(RuleType.GlobalOverride)); // First enum value
            });
        }

        [Test]
        public void ComparableType_UnknownValue_ShouldFallbackToDefault()
        {
            // Arrange
            string unknownComparableTypeJson = "\"unknown_comparable_type\"";

            // Act & Assert
            Assert.DoesNotThrow(() =>
            {
                var result = JsonSerializer.Deserialize<ComparableType>(unknownComparableTypeJson, _options);
                Assert.That(result, Is.EqualTo(ComparableType.Unknown)); // First enum value
            });
        }

        [Test]
        public void ComparableType_EmptyString_ShouldFallbackToDefault()
        {
            // Arrange
            string emptyComparableTypeJson = "\"\"";

            // Act & Assert
            Assert.DoesNotThrow(() =>
            {
                var result = JsonSerializer.Deserialize<ComparableType>(emptyComparableTypeJson, _options);
                Assert.That(result, Is.EqualTo(ComparableType.Unknown));
            });
        }

        [Test]
        public void RuleType_ValidValue_ShouldParseCorrectly()
        {
            // Arrange
            string validRuleTypeJson = "\"standard\"";

            // Act
            var result = JsonSerializer.Deserialize<RuleType>(validRuleTypeJson, _options);

            // Assert
            Assert.That(result, Is.EqualTo(RuleType.Standard));
        }

        [Test]
        public void ComparableType_ValidValue_ShouldParseCorrectly()
        {
            // Arrange
            string validComparableTypeJson = "\"String\"";

            // Act
            var result = JsonSerializer.Deserialize<ComparableType>(validComparableTypeJson, _options);

            // Assert
            Assert.That(result, Is.EqualTo(ComparableType.String));
        }

        [Test]
        public void RuleType_SnakeCaseValue_ShouldParseCorrectly()
        {
            // Arrange
            string snakeCaseRuleTypeJson = "\"global_override\"";

            // Act
            var result = JsonSerializer.Deserialize<RuleType>(snakeCaseRuleTypeJson, _options);

            // Assert
            Assert.That(result, Is.EqualTo(RuleType.GlobalOverride));
        }

        [Test]
        public void ComparableType_NullValue_ShouldFallbackToDefault()
        {
            // Arrange
            string nullComparableTypeJson = "null";

            // Act & Assert
            Assert.DoesNotThrow(() =>
            {
                var result = JsonSerializer.Deserialize<ComparableType?>(nullComparableTypeJson, _options);
                Assert.That(result, Is.Null);
            });
        }

        [TestCase("\"invalid_rule\"", RuleType.GlobalOverride)]
        [TestCase("\"nonexistent\"", RuleType.GlobalOverride)]
        [TestCase("\"\"", RuleType.GlobalOverride)]
        public void RuleType_InvalidValues_ShouldFallbackToDefault(string json, RuleType expected)
        {
            // Act & Assert
            Assert.DoesNotThrow(() =>
            {
                var result = JsonSerializer.Deserialize<RuleType>(json, _options);
                Assert.That(result, Is.EqualTo(expected));
            });
        }

        [TestCase("\"invalid_type\"", ComparableType.Unknown)]
        [TestCase("\"nonexistent\"", ComparableType.Unknown)]
        [TestCase("\"\"", ComparableType.Unknown)]
        public void ComparableType_InvalidValues_ShouldFallbackToDefault(string json, ComparableType expected)
        {
            // Act & Assert
            Assert.DoesNotThrow(() =>
            {
                var result = JsonSerializer.Deserialize<ComparableType>(json, _options);
                Assert.That(result, Is.EqualTo(expected));
            });
        }

        [Test]
        public void RuleType_Serialization_ShouldUseSnakeCase()
        {
            // Arrange
            var ruleType = RuleType.GlobalOverride;

            // Act
            var json = JsonSerializer.Serialize(ruleType, _options);

            // Assert
            Assert.That(json, Is.EqualTo("\"global_override\""));
        }

        [Test]
        public void ComparableType_Serialization_ShouldUseSnakeCase()
        {
            // Arrange
            var comparableType = ComparableType.String;

            // Act
            var json = JsonSerializer.Serialize(comparableType, _options);

            // Assert
            Assert.That(json, Is.EqualTo("\"string\""));
        }
    }
}