using Moq;
using Moq.Contrib.HttpClient;
using NUnit.Framework;
using System.Net;

namespace SchematicHQ.Client.Test
{
    [TestFixture]
    public class ClientOptionsTests
    {
        [Test]
        public void WithHttpClient_CopiesAdditionalHeaders()
        {
            var headers = new Dictionary<string, string?> { { "X-Custom", "value" } };
            var options = new ClientOptions { AdditionalHeaders = headers };

            var result = options.WithHttpClient(new HttpClient());

            Assert.That(result.AdditionalHeaders, Is.EquivalentTo(headers));
        }

        [Test]
        public async Task WithHttpClient_SendsAdditionalHeadersOnRequests()
        {
            var handler = new Mock<HttpMessageHandler>();
            handler.SetupAnyRequest()
                   .ReturnsResponse(HttpStatusCode.OK, "{\"data\":{\"flag\":\"f\",\"reason\":\"r\",\"value\":true}}", "application/json");

            var options = new ClientOptions
            {
                AdditionalHeaders = new Dictionary<string, string?> { { "X-Custom", "value" } },
            };
            var schematic = new Schematic("dummy_api_key", options.WithHttpClient(handler.CreateClient()));
            try
            {
                await schematic.CheckFlag("f");
            }
            finally
            {
                await schematic.Shutdown();
            }

            handler.VerifyRequest(
                r => r.Headers.TryGetValues("X-Custom", out var v) && v.Contains("value"),
                Times.Once());
        }
    }
}
