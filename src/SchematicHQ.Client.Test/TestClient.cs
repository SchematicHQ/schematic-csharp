using Moq;
using NUnit.Framework;
using System.Net;
using Moq.Contrib.HttpClient;
using System.Text.Json;
using System.Text;
using OneOf;
using SchematicHQ.Client.Core;
using SchematicHQ.Client.Cache;
using SchematicHQ.Client.RulesEngine;

namespace SchematicHQ.Client.Test
{
    [TestFixture]
    public class SchematicTests
    {
        private Schematic _schematic;
        private ClientOptions _options;
        private Mock<ISchematicLogger> _logger;
        private int _defaultEventBufferPeriod = 3; // seconds

        private HttpResponseMessage CreateCheckFlagResponse(HttpStatusCode code, bool flagValue)
        {
            var response = new CheckFlagResponse
            {
                Data = new CheckFlagResponseData
                {
                    Flag = "test_flag",
                    Reason = "test_reason",
                    Value = flagValue
                }
            };
            var serializedResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true });
            return new HttpResponseMessage(code)
            {
                Content = new StringContent(serializedResponse, Encoding.UTF8, "application/json")
            };
        }

        private HttpResponseMessage CreateCheckFlagResponseWithEntitlement(HttpStatusCode code, bool flagValue, FeatureEntitlement? entitlement = null, string? companyId = null, string? userId = null, string? flagId = null, string? ruleId = null, string? ruleType = null, string? reason = null)
        {
            var response = new CheckFlagResponse
            {
                Data = new CheckFlagResponseData
                {
                    Flag = "test_flag",
                    Reason = reason ?? "matched entitlement rule",
                    Value = flagValue,
                    Entitlement = entitlement,
                    CompanyId = companyId,
                    UserId = userId,
                    FlagId = flagId,
                    RuleId = ruleId,
                    RuleType = ruleType != null ? RuleType.FromCustom(ruleType) : null
                }
            };
            var serializedResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true });
            return new HttpResponseMessage(code)
            {
                Content = new StringContent(serializedResponse, Encoding.UTF8, "application/json")
            };
        }

        private HttpResponseMessage CreateEventBatchResponse(HttpStatusCode code)
        {
            var response = new CreateEventBatchResponse{
              Data = new RawEventBatchResponseData{
                Events = new List<RawEventResponseData>()
              }
            };
            var serializedResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true });
            return new HttpResponseMessage(code)
            {
                Content = new StringContent(serializedResponse, Encoding.UTF8, "application/json")
            };
        }

        private void SetupSchematicTestClient(bool isOffline, HttpResponseMessage response, Dictionary<string, bool>? flagDefaults = null)
        {
            HttpClient testClient = new HttpClient(new OfflineHttpMessageHandler());
            _options = new ClientOptions
            {
                Logger = _logger.Object,
                Offline = isOffline,
                FlagDefaults = flagDefaults ?? new Dictionary<string, bool>(),
                DefaultEventBufferPeriod = TimeSpan.FromSeconds(_defaultEventBufferPeriod),
                CacheProviders = new List<ICacheProvider<CheckFlagWithEntitlementResponse?>> { new LocalCache<CheckFlagWithEntitlementResponse?>() }
            };

            if (!_options.Offline)
            {
                var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
                testClient = handler.CreateClient();

                handler.SetupAnyRequest()
                       .ReturnsAsync(response);
            }
            _schematic = new Schematic("dummy_api_key", _options.WithHttpClient(testClient));
        }

        [SetUp]
        public void Setup()
        {
            _logger = new Mock<ISchematicLogger>();
        }

        [TearDown]
        public async Task TearDown()
        {
            if (_schematic != null) await _schematic.Shutdown();
        }

        [Test]
        public async Task CheckFlag_CachesResultIfNotCached()
        {
            SetupSchematicTestClient(isOffline: false, response: CreateCheckFlagResponse(HttpStatusCode.OK, true));
            string flagKey = "test_flag";

            // Act
            var result = await _schematic.CheckFlag(flagKey);

            // Assert
            Assert.That(result, Is.True);
            foreach (var cacheProvider in _options.CacheProviders)
            {
                var cached = cacheProvider.Get(flagKey);
                Assert.That(cached, Is.Not.Null);
                Assert.That(cached!.Value, Is.True);
            }
        }

        [Test]
        public async Task CheckFlag_StoreCorrectCacheKey()
        {
            // Arrange
            SetupSchematicTestClient(isOffline: false, response: CreateCheckFlagResponse(HttpStatusCode.OK, false));
            string flagKey = "test_flag";
            string cacheKey = "test_flag:c-name=test_company:u-id=unique_id";
            foreach (var cacheProvider in _options.CacheProviders)
            {
                cacheProvider.Set(cacheKey, new CheckFlagWithEntitlementResponse { FlagKey = flagKey, Value = true, Reason = "cache" });
            }

            // Act
            var result = await _schematic.CheckFlag(
                flagKey: flagKey,
                company: new Dictionary<string, string> { { "name", "test_company" } },
                user: new Dictionary<string, string> { { "id", "unique_id" } }
            );

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task CheckFlag_ReturnsCachedValueIfExists()
        {
            // Arrange
            SetupSchematicTestClient(isOffline: false, response: CreateCheckFlagResponse(HttpStatusCode.OK, false));
            string flagKey = "test_flag";
            foreach (var cacheProvider in _options.CacheProviders)
            {
                cacheProvider.Set(flagKey, new CheckFlagWithEntitlementResponse { FlagKey = flagKey, Value = true, Reason = "cache" });
            }

            // Act
            var result = await _schematic.CheckFlag(flagKey);

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task CheckFlag_LogsErrorAndReturnsDefaultOnException()
        {
            // Arrange
            string flagKey = "error_flag";
            SetupSchematicTestClient(
                isOffline: false,
                response: CreateCheckFlagResponse(HttpStatusCode.InternalServerError, false),
                flagDefaults: new Dictionary<string, bool> { { flagKey, true } }
            );

            // Act
            var result = await _schematic.CheckFlag(flagKey);

            // Assert
            Assert.That(result, Is.True); // default is true for the test flag
            _logger.Verify(logger => logger.Error("Error checking flag via API: {0}", It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task Identify_EnqueuesEventNonBlocking()
        {
            // Arrange
            SetupSchematicTestClient(isOffline: false, response: CreateEventBatchResponse(HttpStatusCode.OK));
            var keys = new Dictionary<string, string> { { "user_id", "12345" } };
            var company = new EventBodyIdentifyCompany { Name = "test_company" };

            // Act
            var identifyTask = Task.Run(() => _schematic.Identify(keys, company, "John Doe", new Dictionary<string, object>()));

            // Assert
            await identifyTask; // Ensure the task completes
            await Task.Delay(100); // Allow some time for the event to be processed asynchronously
            Assert.That(_schematic.GetBufferWaitingEventCount(), Is.EqualTo(1));
        }

        [Test]
        public async Task Track_EnqueuesEventNonBlocking()
        {
            // Arrange
            SetupSchematicTestClient(isOffline: false, response: CreateEventBatchResponse(HttpStatusCode.OK));
            var company = new Dictionary<string, string> { { "company_id", "67890" } };
            var user = new Dictionary<string, string> { { "user_id", "12345" } };

            // Act
            var trackTask = Task.Run(() => _schematic.Track("event_name", company, user, new Dictionary<string, object>()));

            // Assert
            await trackTask; // Ensure the task completes
            await Task.Delay(100); // Allow some time for the event to be processed asynchronously
            Assert.That(_schematic.GetBufferWaitingEventCount(), Is.EqualTo(1));
        }

        [Test]
        public async Task EventBuffer_FlushesPeriodically()
        {
            // Arrange
            SetupSchematicTestClient(isOffline: false, response: CreateCheckFlagResponse(HttpStatusCode.OK, false));

            // Act
            for (int i = 0; i < 10; i++)
            {
                _schematic.Track($"event_{i}");
            }

            // Assert
            Assert.That(_schematic.GetBufferWaitingEventCount(), Is.EqualTo(10)); // Not Flushed yet
            await Task.Delay((_defaultEventBufferPeriod * 1000) + 100); // Wait for the periodic flush to occur
            Assert.That(_schematic.GetBufferWaitingEventCount(), Is.EqualTo(0)); // Assuming the buffer has been flushed
        }

        [Test]
        public void Track_OfflineMode()
        {
            // Arrange
            SetupSchematicTestClient(isOffline: true, response: CreateCheckFlagResponse(HttpStatusCode.OK, false));

            // Act
            for (int i = 0; i < 10; i++)
            {
                _schematic.Track($"event_{i}");
            }

            // Assert
            Assert.That(_schematic.GetBufferWaitingEventCount(), Is.EqualTo(0)); // nothing should be added to buffer in offline mode
        }

        [Test]
        public void Identify_OfflineMode()
        {
            // Arrange
            SetupSchematicTestClient(isOffline: true, response: CreateCheckFlagResponse(HttpStatusCode.OK, false));
            var keys = new Dictionary<string, string> { { "user_id", "12345" } };
            var company = new EventBodyIdentifyCompany { Name = "test_company" };

            // Act
            for (int i = 0; i < 10; i++)
            {
                _schematic.Identify(keys, company, "John Doe", new Dictionary<string, object>());
            }

            // Assert
            Assert.That(_schematic.GetBufferWaitingEventCount(), Is.EqualTo(0)); // nothing should be added to buffer in offline mode
        }

        [Test]
        public async Task CheckFlag_OfflineModeReturnsDefault()
        {
            // Arrange
            SetupSchematicTestClient(
                isOffline: true,
                response: CreateCheckFlagResponse(HttpStatusCode.OK, false),
                flagDefaults: new Dictionary<string, bool> { { "test_flag_key", true } }
            );

            // Act
            var result = await _schematic.CheckFlag("test_flag_key");

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public async Task CheckFlagWithEntitlement_ReturnsResponseWithEntitlement()
        {
            // Arrange
            var entitlement = new FeatureEntitlement
            {
                FeatureId = "feat_123",
                FeatureKey = "test_feature",
                ValueType = EntitlementValueType.Numeric,
                Allocation = 100,
                Usage = 50
            };
            SetupSchematicTestClient(
                isOffline: false,
                response: CreateCheckFlagResponseWithEntitlement(
                    HttpStatusCode.OK,
                    true,
                    entitlement: entitlement,
                    companyId: "comp_123",
                    flagId: "flag_123",
                    ruleId: "rule_123",
                    ruleType: "entitlement"
                )
            );

            // Act
            var result = await _schematic.CheckFlagWithEntitlement("test_flag");

            // Assert
            Assert.That(result.Value, Is.True);
            Assert.That(result.FlagKey, Is.EqualTo("test_flag"));
            Assert.That(result.Reason, Is.EqualTo("matched entitlement rule"));
            Assert.That(result.CompanyId, Is.EqualTo("comp_123"));
            Assert.That(result.FlagId, Is.EqualTo("flag_123"));
            Assert.That(result.RuleId, Is.EqualTo("rule_123"));
            Assert.That(result.RuleType, Is.EqualTo("entitlement"));
            Assert.That(result.Entitlement, Is.Not.Null);
            Assert.That(result.Entitlement!.FeatureId, Is.EqualTo("feat_123"));
            Assert.That(result.Entitlement.FeatureKey, Is.EqualTo("test_feature"));
            Assert.That(result.Entitlement.Allocation, Is.EqualTo(100));
            Assert.That(result.Entitlement.Usage, Is.EqualTo(50));
        }

        [Test]
        public async Task CheckFlagWithEntitlement_OfflineModeReturnsDefault()
        {
            // Arrange
            SetupSchematicTestClient(
                isOffline: true,
                response: CreateCheckFlagResponse(HttpStatusCode.OK, false),
                flagDefaults: new Dictionary<string, bool> { { "test_flag_key", true } }
            );

            // Act
            var result = await _schematic.CheckFlagWithEntitlement("test_flag_key");

            // Assert
            Assert.That(result.Value, Is.True);
            Assert.That(result.FlagKey, Is.EqualTo("test_flag_key"));
            Assert.That(result.Reason, Is.EqualTo("offline mode"));
            Assert.That(result.Entitlement, Is.Null);
        }

        [Test]
        public async Task CheckFlagWithEntitlement_ReturnsResponseWithNoEntitlement()
        {
            // Arrange
            SetupSchematicTestClient(
                isOffline: false,
                response: CreateCheckFlagResponseWithEntitlement(HttpStatusCode.OK, false)
            );

            // Act
            var result = await _schematic.CheckFlagWithEntitlement("test_flag");

            // Assert
            Assert.That(result.Value, Is.False);
            Assert.That(result.FlagKey, Is.EqualTo("test_flag"));
            Assert.That(result.Entitlement, Is.Null);
        }

        [Test]
        public async Task CheckFlagWithEntitlement_ReturnsDefaultOnError()
        {
            // Arrange
            string flagKey = "error_flag";
            SetupSchematicTestClient(
                isOffline: false,
                response: CreateCheckFlagResponse(HttpStatusCode.InternalServerError, false),
                flagDefaults: new Dictionary<string, bool> { { flagKey, true } }
            );

            // Act
            var result = await _schematic.CheckFlagWithEntitlement(flagKey);

            // Assert
            Assert.That(result.Value, Is.True);
            Assert.That(result.FlagKey, Is.EqualTo(flagKey));
            Assert.That(result.Entitlement, Is.Null);
        }

        [Test]
        public async Task CheckFlagWithEntitlement_CachesAndReturnsCachedResponse()
        {
            // Arrange
            SetupSchematicTestClient(
                isOffline: false,
                response: CreateCheckFlagResponseWithEntitlement(HttpStatusCode.OK, true)
            );
            string flagKey = "test_flag";

            // First call should hit the API and cache
            var result1 = await _schematic.CheckFlagWithEntitlement(flagKey);
            Assert.That(result1.Value, Is.True);

            // Verify cache was populated with full response
            foreach (var cacheProvider in _options.CacheProviders)
            {
                var cached = cacheProvider.Get(flagKey);
                Assert.That(cached, Is.Not.Null);
                Assert.That(cached!.Value, Is.True);
                Assert.That(cached.FlagKey, Is.EqualTo(flagKey));
                Assert.That(cached.Reason, Is.EqualTo("matched entitlement rule"));
            }
        }

        [Test]
        public async Task CheckFlag_WithCacheDisabled_AlwaysCallsAPI()
        {
            // Arrange
            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handler.SetupAnyRequest()
                   .ReturnsAsync(CreateCheckFlagResponse(HttpStatusCode.OK, true));

            var testClient = handler.CreateClient();
            _options = new ClientOptions
            {
                Logger = _logger.Object,
                Offline = false,
                FlagDefaults = new Dictionary<string, bool>(),
                DefaultEventBufferPeriod = TimeSpan.FromSeconds(_defaultEventBufferPeriod),
                CacheProviders = new List<ICacheProvider<CheckFlagWithEntitlementResponse?>> { new LocalCache<CheckFlagWithEntitlementResponse?>(maxItems: 0) }
            };
            _schematic = new Schematic("dummy_api_key", _options.WithHttpClient(testClient));

            string flagKey = "test_flag";

            // Act
            var result1 = await _schematic.CheckFlag(flagKey);
            var result2 = await _schematic.CheckFlag(flagKey);

            // Assert
            Assert.That(result1, Is.True);
            Assert.That(result2, Is.True);
            handler.VerifyAnyRequest(Times.Exactly(2));
        }

        [Test]
        public async Task CheckFlag_ReturnsFalseOnErrorWithNoDefault()
        {
            // Arrange
            string flagKey = "error_flag";
            SetupSchematicTestClient(
                isOffline: false,
                response: CreateCheckFlagResponse(HttpStatusCode.InternalServerError, false)
                // No flag defaults set
            );

            // Act
            var result = await _schematic.CheckFlag(flagKey);

            // Assert
            Assert.That(result, Is.False);
            _logger.Verify(logger => logger.Error("Error checking flag via API: {0}", It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task CheckFlag_DifferentContextsProduceDifferentCacheKeys()
        {
            // Arrange
            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handler.SetupAnyRequest()
                   .ReturnsAsync(CreateCheckFlagResponse(HttpStatusCode.OK, true));

            var testClient = handler.CreateClient();
            _options = new ClientOptions
            {
                Logger = _logger.Object,
                Offline = false,
                FlagDefaults = new Dictionary<string, bool>(),
                DefaultEventBufferPeriod = TimeSpan.FromSeconds(_defaultEventBufferPeriod),
                CacheProviders = new List<ICacheProvider<CheckFlagWithEntitlementResponse?>> { new LocalCache<CheckFlagWithEntitlementResponse?>() }
            };
            _schematic = new Schematic("dummy_api_key", _options.WithHttpClient(testClient));

            string flagKey = "test_flag";

            // Act
            var result1 = await _schematic.CheckFlag(
                flagKey: flagKey,
                company: new Dictionary<string, string> { { "id", "company_a" } }
            );
            var result2 = await _schematic.CheckFlag(
                flagKey: flagKey,
                company: new Dictionary<string, string> { { "id", "company_b" } }
            );

            // Assert
            Assert.That(result1, Is.True);
            Assert.That(result2, Is.True);
            handler.VerifyAnyRequest(Times.Exactly(2));
        }

        [Test]
        public async Task SetFlagDefault_UpdatesDefaultUsedByCheckFlag()
        {
            SetupSchematicTestClient(
                isOffline: true,
                response: CreateCheckFlagResponse(HttpStatusCode.OK, false)
            );

            var beforeSet = await _schematic.CheckFlag("late_flag");
            Assert.That(beforeSet, Is.False);

            _schematic.SetFlagDefault("late_flag", true);

            var afterSet = await _schematic.CheckFlag("late_flag");
            Assert.That(afterSet, Is.True);

            _schematic.SetFlagDefault("late_flag", false);
            var afterOverride = await _schematic.CheckFlag("late_flag");
            Assert.That(afterOverride, Is.False);
        }

        [Test]
        public async Task SetFlagDefaults_UpdatesMultipleDefaults()
        {
            SetupSchematicTestClient(
                isOffline: true,
                response: CreateCheckFlagResponse(HttpStatusCode.OK, false),
                flagDefaults: new Dictionary<string, bool> { { "existing_flag", false } }
            );

            _schematic.SetFlagDefaults(new Dictionary<string, bool>
            {
                { "existing_flag", true },
                { "new_flag_a", true },
                { "new_flag_b", false }
            });

            Assert.That(await _schematic.CheckFlag("existing_flag"), Is.True);
            Assert.That(await _schematic.CheckFlag("new_flag_a"), Is.True);
            Assert.That(await _schematic.CheckFlag("new_flag_b"), Is.False);
            Assert.That(await _schematic.CheckFlag("unset_flag"), Is.False);
        }

        [Test]
        public async Task Track_IncludesQuantityWhenProvided()
        {
            // Arrange
            SetupSchematicTestClient(isOffline: false, response: CreateEventBatchResponse(HttpStatusCode.OK));
            var company = new Dictionary<string, string> { { "company_id", "67890" } };
            var user = new Dictionary<string, string> { { "user_id", "12345" } };

            // Act
            var trackTask = Task.Run(() => _schematic.Track("event_name", company, user, new Dictionary<string, object>(), quantity: 5));

            // Assert
            await trackTask; // Ensure the task completes
            await Task.Delay(100); // Allow some time for the event to be processed asynchronously
            Assert.That(_schematic.GetBufferWaitingEventCount(), Is.EqualTo(1));
        }
    }
}
