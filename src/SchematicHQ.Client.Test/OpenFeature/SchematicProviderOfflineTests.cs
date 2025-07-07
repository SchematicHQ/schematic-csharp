using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenFeature;
using OpenFeature.Constant;
using OpenFeature.Model;
using SchematicHQ.Client;
using SchematicHQ.Client.OpenFeature;

namespace SchematicHQ.Client.Test.OpenFeature
{
    [TestFixture]
    public class SchematicProviderOfflineTests
    {
        [Test]
        public void GetMetadata_ReturnsCorrectName()
        {
            var provider = new SchematicProvider("test-key");
            var metadata = provider.GetMetadata();
            Assert.That(metadata.Name, Is.EqualTo("schematic-provider"));
        }

        [Test]
        public async Task ResolveBooleanValue_InOfflineMode_ReturnsDefaultValue()
        {
            // Arrange
            var options = new ClientOptions
            {
                Offline = true,
                FlagDefaults = new Dictionary<string, bool>
                {
                    ["test-flag"] = true,
                    ["other-flag"] = false
                }
            };
            var provider = new SchematicProvider("test-key", options);
            var context = EvaluationContext.Empty;

            // Act
            var result = await provider.ResolveBooleanValue("test-flag", false, context);

            // Assert
            Assert.That(result.FlagKey, Is.EqualTo("test-flag"));
            Assert.That(result.Value, Is.True);
            Assert.That(result.Variant, Is.EqualTo("on"));
            Assert.That(result.Reason, Is.EqualTo(Reason.Cached));
        }

        [Test]
        public async Task ResolveBooleanValue_FlagNotInDefaults_ReturnsFalse()
        {
            // Arrange
            var options = new ClientOptions
            {
                Offline = true,
                FlagDefaults = new Dictionary<string, bool>
                {
                    ["existing-flag"] = true
                }
            };
            var provider = new SchematicProvider("test-key", options);

            // Act
            // In offline mode, Schematic returns false for flags not in defaults, regardless of the provided default
            var result = await provider.ResolveBooleanValue("non-existent-flag", true, EvaluationContext.Empty);

            // Assert
            Assert.That(result.Value, Is.False); // Schematic always returns false for unknown flags in offline mode
            Assert.That(result.Reason, Is.EqualTo(Reason.Cached));
        }

        [Test]
        public async Task ResolveBooleanValue_WithContext_ExtractsCorrectly()
        {
            // Arrange
            var options = new ClientOptions
            {
                Offline = true,
                FlagDefaults = new Dictionary<string, bool>
                {
                    ["feature-x"] = true
                }
            };
            var provider = new SchematicProvider("test-key", options);
            
            var context = EvaluationContext.Builder()
                .Set("company", new Structure(new Dictionary<string, Value>
                {
                    ["id"] = new Value("company-123"),
                    ["name"] = new Value("Test Company")
                }))
                .Set("user", new Structure(new Dictionary<string, Value>
                {
                    ["id"] = new Value("user-456"),
                    ["email"] = new Value("test@example.com")
                }))
                .Build();

            // Act - In offline mode, context is ignored but should not cause errors
            var result = await provider.ResolveBooleanValue("feature-x", false, context);

            // Assert
            Assert.That(result.Value, Is.True);
        }

        [Test]
        public async Task ResolveBooleanValue_WithTargetingKey_HandlesCorrectly()
        {
            // Arrange
            var options = new ClientOptions
            {
                Offline = true,
                FlagDefaults = new Dictionary<string, bool>
                {
                    ["test-flag"] = true
                }
            };
            var provider = new SchematicProvider("test-key", options);
            
            var context = EvaluationContext.Builder()
                .SetTargetingKey("user-123")
                .Build();

            // Act
            var result = await provider.ResolveBooleanValue("test-flag", false, context);

            // Assert
            Assert.That(result.Value, Is.True);
        }

        [Test]
        public async Task ResolveStringValue_ReturnsUnsupportedType()
        {
            // Arrange
            var provider = new SchematicProvider("test-key");
            var context = EvaluationContext.Empty;

            // Act
            var result = await provider.ResolveStringValue("test-flag", "default", context);

            // Assert
            Assert.That(result.FlagKey, Is.EqualTo("test-flag"));
            Assert.That(result.Value, Is.EqualTo("default"));
            Assert.That(result.Reason, Is.EqualTo(Reason.Error));
            Assert.That(result.ErrorType, Is.EqualTo(ErrorType.TypeMismatch));
            Assert.That(result.ErrorMessage, Does.Contain("not supported"));
        }

        [Test]
        public async Task ResolveIntegerValue_ReturnsUnsupportedType()
        {
            // Arrange
            var provider = new SchematicProvider("test-key");

            // Act
            var result = await provider.ResolveIntegerValue("test-flag", 42, EvaluationContext.Empty);

            // Assert
            Assert.That(result.Value, Is.EqualTo(42));
            Assert.That(result.Reason, Is.EqualTo(Reason.Error));
            Assert.That(result.ErrorType, Is.EqualTo(ErrorType.TypeMismatch));
        }

        [Test]
        public async Task ResolveDoubleValue_ReturnsUnsupportedType()
        {
            // Arrange
            var provider = new SchematicProvider("test-key");

            // Act
            var result = await provider.ResolveDoubleValue("test-flag", 3.14, EvaluationContext.Empty);

            // Assert
            Assert.That(result.Value, Is.EqualTo(3.14));
            Assert.That(result.Reason, Is.EqualTo(Reason.Error));
            Assert.That(result.ErrorType, Is.EqualTo(ErrorType.TypeMismatch));
        }

        [Test]
        public async Task ResolveStructureValue_ReturnsUnsupportedType()
        {
            // Arrange
            var provider = new SchematicProvider("test-key");
            var defaultValue = new Value(new Structure(new Dictionary<string, Value>()));

            // Act
            var result = await provider.ResolveStructureValue("test-flag", defaultValue, EvaluationContext.Empty);

            // Assert
            Assert.That(result.Value, Is.EqualTo(defaultValue));
            Assert.That(result.Reason, Is.EqualTo(Reason.Error));
            Assert.That(result.ErrorType, Is.EqualTo(ErrorType.TypeMismatch));
        }

        [Test]
        public async Task Initialize_CompletesSuccessfully()
        {
            // Arrange
            var provider = new SchematicProvider("test-key");

            // Act & Assert (should not throw)
            await provider.Initialize(EvaluationContext.Empty);
        }

        [Test]
        public async Task Shutdown_InOfflineMode_CompletesSuccessfully()
        {
            // Arrange
            var options = new ClientOptions { Offline = true };
            var provider = new SchematicProvider("test-key", options);

            // Act & Assert (should not throw)
            await provider.Shutdown();
        }

        [Test]
        public async Task TrackEventAsync_WithEmptyEventName_ThrowsArgumentException()
        {
            // Arrange
            var provider = new SchematicProvider("test-key");

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => 
                await provider.TrackEventAsync("", EvaluationContext.Empty));
        }

        [Test]
        public async Task TrackEventAsync_InOfflineMode_CompletesSuccessfully()
        {
            // Arrange
            var options = new ClientOptions { Offline = true };
            var provider = new SchematicProvider("test-key", options);
            
            var context = EvaluationContext.Builder()
                .Set("company", new Structure(new Dictionary<string, Value>
                {
                    ["id"] = new Value("company-123")
                }))
                .Build();

            var traits = new Dictionary<string, object>
            {
                ["action"] = "clicked"
            };

            // Act & Assert (should not throw)
            await provider.TrackEventAsync("button_click", context, traits);
        }

        [Test]
        public void Constructor_WithEmptyApiKey_ThrowsArgumentException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new SchematicProvider(""));
        }

        [Test]
        public void Constructor_WithNullSchematic_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new SchematicProvider((Schematic)null!));
        }
    }
}