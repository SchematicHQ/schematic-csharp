using System.Net;
using System.Text.Json;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client.Tests
{
    [TestFixture]
    public class EventCaptureClientTests
    {
        private Mock<ISchematicLogger> _mockLogger;
        private Mock<HttpMessageHandler> _mockHandler;
        private HttpClient _httpClient;

        [SetUp]
        public void SetUp()
        {
            _mockLogger = new Mock<ISchematicLogger>();
            _mockHandler = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_mockHandler.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _httpClient.Dispose();
        }

        [Test]
        public async Task SendBatchAsync_SendsToCorrectEndpoint()
        {
            HttpRequestMessage? capturedRequest = null;

            _mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .Callback<HttpRequestMessage, CancellationToken>((req, _) => capturedRequest = req)
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));

            var client = new EventCaptureClient(_httpClient, "test_api_key", _mockLogger.Object);
            var events = new List<CreateEventRequestBody>
            {
                new CreateEventRequestBody
                {
                    EventType = EventType.Track,
                    Body = new EventBodyTrack { Event = "test_event" }
                }
            };

            await client.SendBatchAsync(events);

            Assert.That(capturedRequest, Is.Not.Null);
            Assert.That(capturedRequest!.RequestUri!.ToString(), Is.EqualTo("https://c.schematichq.com/batch"));
            Assert.That(capturedRequest.Method, Is.EqualTo(HttpMethod.Post));
        }

        [Test]
        public async Task SendBatchAsync_UsesCustomBaseUrl()
        {
            HttpRequestMessage? capturedRequest = null;

            _mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .Callback<HttpRequestMessage, CancellationToken>((req, _) => capturedRequest = req)
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));

            var client = new EventCaptureClient(_httpClient, "test_api_key", _mockLogger.Object, "https://custom.capture.com");
            var events = new List<CreateEventRequestBody>
            {
                new CreateEventRequestBody
                {
                    EventType = EventType.Track,
                    Body = new EventBodyTrack { Event = "test_event" }
                }
            };

            await client.SendBatchAsync(events);

            Assert.That(capturedRequest!.RequestUri!.ToString(), Is.EqualTo("https://custom.capture.com/batch"));
        }

        [Test]
        public async Task SendBatchAsync_IncludesApiKeyInPayload()
        {
            string? capturedBody = null;

            _mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .Callback<HttpRequestMessage, CancellationToken>(async (req, _) =>
                {
                    capturedBody = await req.Content!.ReadAsStringAsync();
                })
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));

            var client = new EventCaptureClient(_httpClient, "test_api_key_123", _mockLogger.Object);
            var events = new List<CreateEventRequestBody>
            {
                new CreateEventRequestBody
                {
                    EventType = EventType.Track,
                    Body = new EventBodyTrack { Event = "test_event" }
                }
            };

            await client.SendBatchAsync(events);

            Assert.That(capturedBody, Is.Not.Null);
            var doc = JsonDocument.Parse(capturedBody!);
            var eventsArray = doc.RootElement.GetProperty("events");
            Assert.That(eventsArray.GetArrayLength(), Is.EqualTo(1));

            var firstEvent = eventsArray[0];
            Assert.That(firstEvent.GetProperty("api_key").GetString(), Is.EqualTo("test_api_key_123"));
            Assert.That(firstEvent.GetProperty("type").GetString(), Is.EqualTo("track"));
        }

        [Test]
        public async Task SendBatchAsync_TransformsMultipleEvents()
        {
            string? capturedBody = null;

            _mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .Callback<HttpRequestMessage, CancellationToken>(async (req, _) =>
                {
                    capturedBody = await req.Content!.ReadAsStringAsync();
                })
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));

            var client = new EventCaptureClient(_httpClient, "test_key", _mockLogger.Object);
            var events = new List<CreateEventRequestBody>
            {
                new CreateEventRequestBody
                {
                    EventType = EventType.Track,
                    Body = new EventBodyTrack { Event = "event1" }
                },
                new CreateEventRequestBody
                {
                    EventType = EventType.Identify,
                    Body = new EventBodyIdentify { Keys = new Dictionary<string, string> { { "user_id", "123" } } }
                },
                new CreateEventRequestBody
                {
                    EventType = EventType.FlagCheck,
                    Body = new EventBodyFlagCheck { FlagKey = "test_flag", Value = true, Reason = "test" }
                }
            };

            await client.SendBatchAsync(events);

            var doc = JsonDocument.Parse(capturedBody!);
            var eventsArray = doc.RootElement.GetProperty("events");
            Assert.That(eventsArray.GetArrayLength(), Is.EqualTo(3));

            Assert.That(eventsArray[0].GetProperty("type").GetString(), Is.EqualTo("track"));
            Assert.That(eventsArray[1].GetProperty("type").GetString(), Is.EqualTo("identify"));
            Assert.That(eventsArray[2].GetProperty("type").GetString(), Is.EqualTo("flag_check"));

            // All events should have the api_key
            for (int i = 0; i < 3; i++)
            {
                Assert.That(eventsArray[i].GetProperty("api_key").GetString(), Is.EqualTo("test_key"));
            }
        }

        [Test]
        public async Task SendBatchAsync_SkipsEmptyList()
        {
            _mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));

            var client = new EventCaptureClient(_httpClient, "test_key", _mockLogger.Object);
            await client.SendBatchAsync(new List<CreateEventRequestBody>());

            _mockHandler
                .Protected()
                .Verify(
                    "SendAsync",
                    Times.Never(),
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>());
        }

        [Test]
        public void SendBatchAsync_ThrowsOnHttpError()
        {
            _mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("Internal Server Error")
                });

            var client = new EventCaptureClient(_httpClient, "test_key", _mockLogger.Object);
            var events = new List<CreateEventRequestBody>
            {
                new CreateEventRequestBody
                {
                    EventType = EventType.Track,
                    Body = new EventBodyTrack { Event = "test_event" }
                }
            };

            Assert.ThrowsAsync<HttpRequestException>(async () => await client.SendBatchAsync(events));
        }

        [Test]
        public async Task SendBatchAsync_IncludesSentAtWhenPresent()
        {
            string? capturedBody = null;
            var sentAt = new DateTime(2026, 4, 13, 12, 0, 0, DateTimeKind.Utc);

            _mockHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .Callback<HttpRequestMessage, CancellationToken>(async (req, _) =>
                {
                    capturedBody = await req.Content!.ReadAsStringAsync();
                })
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));

            var client = new EventCaptureClient(_httpClient, "test_key", _mockLogger.Object);
            var events = new List<CreateEventRequestBody>
            {
                new CreateEventRequestBody
                {
                    EventType = EventType.Track,
                    Body = new EventBodyTrack { Event = "test_event" },
                    SentAt = sentAt
                }
            };

            await client.SendBatchAsync(events);

            var doc = JsonDocument.Parse(capturedBody!);
            var firstEvent = doc.RootElement.GetProperty("events")[0];
            Assert.That(firstEvent.TryGetProperty("sent_at", out _), Is.True);
        }
    }
}
