using Moq;
using NUnit.Framework;
using System.Net;
using Moq.Contrib.HttpClient;
using System.Text.Json;
using System.Text;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client.Test
{
    [TestFixture]
    public class CompaniesClientTests
    {
        private Schematic _schematic;
        private ClientOptions _options;
        private Mock<ISchematicLogger> _logger;

        private HttpResponseMessage CreateUpsertCompanyResponse(HttpStatusCode code)
        {
            var response = new UpsertCompanyResponse
            {
                Data = new CompanyDetailResponseData
                {
                    Id = "test-company-id",
                    Name = "Acme Widgets, Inc.",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    EnvironmentId = "test-env-id",
                    UserCount = 1
                }
            };
            var serializedResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true });
            return new HttpResponseMessage(code)
            {
                Content = new StringContent(serializedResponse, Encoding.UTF8, "application/json")
            };
        }

        private HttpResponseMessage CreateUpsertUserResponse(HttpStatusCode code)
        {
            var response = new UpsertUserResponse
            {
                Data = new UserDetailResponseData
                {
                    Id = "test-user-id",
                    Name = "Wile E. Coyote",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    EnvironmentId = "test-env-id"
                }
            };
            var serializedResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true });
            return new HttpResponseMessage(code)
            {
                Content = new StringContent(serializedResponse, Encoding.UTF8, "application/json")
            };
        }

        private void SetupSchematicTestClient(HttpResponseMessage response)
        {
            _logger = new Mock<ISchematicLogger>();
            _options = new ClientOptions
            {
                Logger = _logger.Object
            };

            var handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            var testClient = handler.CreateClient();

            handler.SetupAnyRequest()
                   .ReturnsAsync(response);

            _schematic = new Schematic("dummy_api_key", _options.WithHttpClient(testClient));
        }

        [TearDown]
        public async Task TearDown()
        {
            if (_schematic != null) await _schematic.Shutdown();
        }

        [Test]
        public async Task UpsertCompany_CanAccessThroughHoistedClient()
        {
            // Arrange
            SetupSchematicTestClient(CreateUpsertCompanyResponse(HttpStatusCode.OK));
            var request = new UpsertCompanyRequestBody
            {
                Keys = new Dictionary<string, string> { { "id", "your-company-id" } },
                Name = "Acme Widgets, Inc.",
                Traits = new Dictionary<string, object>
                {
                    { "city", "Atlanta" },
                    { "high_score", 25 },
                    { "is_active", true }
                }
            };

            // Act
            // This test verifies we can access Companies directly on Schematic instance
            // rather than going through API.Companies
            var response = await _schematic.Companies.UpsertCompanyAsync(request);

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Data, Is.Not.Null);
            Assert.That(response.Data.Id, Is.EqualTo("test-company-id"));
            Assert.That(response.Data.Name, Is.EqualTo("Acme Widgets, Inc."));
        }

        [Test]
        public async Task UpsertUser_CanAccessThroughHoistedClient()
        {
            // Arrange
            SetupSchematicTestClient(CreateUpsertUserResponse(HttpStatusCode.OK));
            var request = new UpsertUserRequestBody
            {
                Keys = new Dictionary<string, string>
                {
                    { "email", "wcoyote@acme.net" },
                    { "user_id", "your-user-id" }
                },
                Name = "Wile E. Coyote",
                Traits = new Dictionary<string, object>
                {
                    { "city", "Atlanta" },
                    { "high_score", 25 },
                    { "is_active", true }
                },
                Company = new Dictionary<string, string> { { "id", "your-company-id" } }
            };

            // Act
            // This test verifies we can access Companies directly on Schematic instance
            // rather than going through API.Companies
            var response = await _schematic.Companies.UpsertUserAsync(request);

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Data, Is.Not.Null);
            Assert.That(response.Data.Id, Is.EqualTo("test-user-id"));
            Assert.That(response.Data.Name, Is.EqualTo("Wile E. Coyote"));
        }

        [Test]
        public async Task UpsertCompany_ExampleFromReadme()
        {
            // Arrange
            SetupSchematicTestClient(CreateUpsertCompanyResponse(HttpStatusCode.OK));
            
            // Act
            // This mimics the example code from the README.md
            var response = await _schematic.Companies.UpsertCompanyAsync(new UpsertCompanyRequestBody
            {
                Keys = new Dictionary<string, string> { { "id", "your-company-id" } },
                Name = "Acme Widgets, Inc.",
                Traits = new Dictionary<string, object>
                {
                    { "city", "Atlanta" },
                    { "high_score", 25 },
                    { "is_active", true }
                }
            });

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Data, Is.Not.Null);
            Assert.That(response.Data.Name, Is.EqualTo("Acme Widgets, Inc."));
        }

        [Test]
        public async Task UpsertUser_ExampleFromReadme()
        {
            // Arrange
            SetupSchematicTestClient(CreateUpsertUserResponse(HttpStatusCode.OK));
            
            // Act
            // This mimics the example code from the README.md
            var response = await _schematic.Companies.UpsertUserAsync(new UpsertUserRequestBody
            {
                Keys = new Dictionary<string, string>
                {
                    { "email", "wcoyote@acme.net" },
                    { "user_id", "your-user-id" }
                },
                Name = "Wile E. Coyote",
                Traits = new Dictionary<string, object>
                {
                    { "city", "Atlanta" },
                    { "high_score", 25 },
                    { "is_active", true }
                },
                Company = new Dictionary<string, string> { { "id", "your-company-id" } }
            });

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Data, Is.Not.Null);
            Assert.That(response.Data.Name, Is.EqualTo("Wile E. Coyote"));
        }
    }
}