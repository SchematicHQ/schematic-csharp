using Moq;
using NUnit.Framework;
using System.Net;
using Moq.Contrib.HttpClient;
using System.Text.Json;
using System.Text;
using OneOf;
using SchematicHQ.Client.Core;
using SchematicHQ.Client.Cache;

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
                CacheProviders = new List<ICacheProvider<bool?>> { new LocalCache<bool?>() }
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
                Assert.That(cacheProvider.Get(flagKey), Is.EqualTo(true));
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
                cacheProvider.Set(cacheKey, true);
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
                cacheProvider.Set(flagKey, true);
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
            _logger.Verify(logger => logger.Error("Error checking flag: {0}", It.IsAny<string>()), Times.Once);
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
    }
}
