using NUnit.Framework;
using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SchematicHQ.Client.Datastream;
using SchematicHQ.Client.Test.Datastream.Mocks;

namespace SchematicHQ.Client.Test.Datastream
{
    [TestFixture]
    public class ReplicatorHealthServiceTests
    {
        [Test]
        public async Task PerformHealthCheck_WhenReplicatorReady_SetsHealthyToTrue()
        {
            // Arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            var logger = new MockSchematicLogger();
            
            var healthResponse = """
                {
                    "status": "healthy",
                    "ready": true,
                    "connected": true,
                    "components": {
                        "datastream": "ready",
                        "redis": "connected"
                    },
                    "cache_version": "test123",
                    "timestamp": "2025-10-06T12:50:52.2943-06:00"
                }
                """;

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(healthResponse)
                });

            var service = new ReplicatorHealthService(httpClient, "http://test/ready", logger);

            // Act
            service.Start();
            
            // Wait for health check to complete
            await Task.Delay(100);

            // Assert
            Assert.That(service.IsHealthy, Is.True);
            Assert.That(service.CacheVersion, Is.EqualTo("test123"));
        }

        [Test]
        public async Task PerformHealthCheck_WhenReplicatorNotReady_SetsHealthyToFalse()
        {
            // Arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            var logger = new MockSchematicLogger();
            
            var healthResponse = """
                {
                    "status": "healthy",
                    "ready": false,
                    "connected": true,
                    "components": {
                        "datastream": "connected_loading",
                        "redis": "connected"
                    },
                    "cache_version": "test123",
                    "timestamp": "2025-10-06T12:50:52.2943-06:00"
                }
                """;

            // Return 503 Service Unavailable when not ready (matching Go replicator behavior)
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.ServiceUnavailable,
                    Content = new StringContent(healthResponse)
                });

            var service = new ReplicatorHealthService(httpClient, "http://test/ready", logger);

            // Act
            service.Start();
            
            // Wait for health check to complete
            await Task.Delay(100);

            // Assert
            Assert.That(service.IsHealthy, Is.False);
            Assert.That(service.CacheVersion, Is.EqualTo("test123")); // Should still parse cache version
        }

        [Test]
        public async Task CacheVersionChanged_WhenVersionChanges_FiresEvent()
        {
            // Arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            var httpClient = new HttpClient(mockHttpMessageHandler.Object);
            var logger = new MockSchematicLogger();
            
            var eventCallCount = 0;
            string? firstEventOldVersion = null;
            string? firstEventNewVersion = null;
            string? secondEventOldVersion = null;
            string? secondEventNewVersion = null;

            var service = new ReplicatorHealthService(httpClient, "http://test/ready", logger);
            service.CacheVersionChanged += (old, newVer) =>
            {
                if (eventCallCount == 0)
                {
                    firstEventOldVersion = old;
                    firstEventNewVersion = newVer;
                }
                else if (eventCallCount == 1)
                {
                    secondEventOldVersion = old;
                    secondEventNewVersion = newVer;
                }
                eventCallCount++;
            };

            // First response with version "v1"
            var firstResponse = """
                {
                    "status": "healthy",
                    "ready": true,
                    "connected": true,
                    "cache_version": "v1",
                    "timestamp": "2025-10-06T12:50:52.2943-06:00"
                }
                """;

            // Second response with version "v2"
            var secondResponse = """
                {
                    "status": "healthy",
                    "ready": true,
                    "connected": true,
                    "cache_version": "v2",
                    "timestamp": "2025-10-06T12:50:52.2943-06:00"
                }
                """;

            // Set up separate calls instead of sequence to avoid timing issues
            var callCount = 0;
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .Returns(() =>
                {
                    callCount++;
                    if (callCount == 1)
                    {
                        return Task.FromResult(new HttpResponseMessage
                        {
                            StatusCode = HttpStatusCode.OK,
                            Content = new StringContent(firstResponse)
                        });
                    }
                    else
                    {
                        return Task.FromResult(new HttpResponseMessage
                        {
                            StatusCode = HttpStatusCode.OK,
                            Content = new StringContent(secondResponse)
                        });
                    }
                });

            try
            {
                // Act - Call GetCacheVersionAsync twice to trigger two health checks
                // First call should trigger the first HTTP request
                var firstVersion = await service.GetCacheVersionAsync();
                
                // Assert first version is set and first event fired (null -> "v1")
                Assert.That(firstVersion, Is.EqualTo("v1"));
                Assert.That(eventCallCount, Is.EqualTo(1), "Exactly one event should have fired for null -> v1");
                Assert.That(firstEventOldVersion, Is.Null, "First event old version should be null");
                Assert.That(firstEventNewVersion, Is.EqualTo("v1"), "First event new version should be v1");
                
                // Reset cache version to force second health check
                // We can't directly access private fields, so we'll use reflection as a test-only approach
                var cacheVersionField = typeof(ReplicatorHealthService).GetField("_cacheVersion", 
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                cacheVersionField?.SetValue(service, null);
                
                // Second call should trigger the second HTTP request
                var secondVersion = await service.GetCacheVersionAsync();
                
                // Assert that the second event was fired when version changed from v1 to v2
                Assert.That(secondVersion, Is.EqualTo("v2"));
                Assert.That(eventCallCount, Is.EqualTo(2), "Exactly two events should have fired");
                Assert.That(secondEventOldVersion, Is.Null, "Second event old version should be null (since we reset it)");
                Assert.That(secondEventNewVersion, Is.EqualTo("v2"), "Second event new version should be v2");
            }
            finally
            {
                service.Dispose();
            }
        }

        [Test]
        public void Dispose_CleansUpResources()
        {
            // Arrange
            var httpClient = new HttpClient();
            var logger = new MockSchematicLogger();
            var service = new ReplicatorHealthService(httpClient, "http://test/ready", logger);

            // Act & Assert - should not throw
            service.Dispose();
            
            // Multiple dispose calls should be safe
            service.Dispose();
        }
    }
}