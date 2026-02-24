using System.Text.Json;
using NUnit.Framework;
using SchematicHQ.Client.RulesEngine.Models;
using SchematicHQ.Client.Datastream;
using SchematicHQ.Client.Test.Datastream.Mocks;

namespace SchematicHQ.Client.Test.Datastream
{
    [TestFixture]
    public class DatastreamCacheTests
    {
        private MockWebSocket _mockWebSocket;
        private MockSchematicLogger _mockLogger;
        private DatastreamClient _client;

        [SetUp]
        public void Setup()
        {
            var testSetup = DatastreamClientTestFactory.CreateClientWithMocks(cacheTtl: TimeSpan.FromMilliseconds(100));
            _client = testSetup.Client;
            _mockWebSocket = testSetup.WebSocket;
            _mockLogger = testSetup.Logger;
        }

        private void PopulateTwoLayerCompanyCache(Company company)
        {
            var method = typeof(DatastreamClient).GetMethod("CacheCompanyForKeys",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            method!.Invoke(_client, new object[] { company });
        }

        private void PopulateTwoLayerUserCache(User user)
        {
            var method = typeof(DatastreamClient).GetMethod("CacheUserForKeys",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            method!.Invoke(_client, new object[] { user });
        }

        [Test]
        public void ExpiredCache_RequestsResourcesAgain()
        {
            var companyKey = "company-123";

            var company = new Company
            {
                AccountId = "acc_123",
                EnvironmentId = "env_123",
                Id = "comp_123",
                Keys = new Dictionary<string, string> { { "id", companyKey } }
            };

            PopulateTwoLayerCompanyCache(company);

            var keys = new Dictionary<string, string> { { "id", companyKey } };
            var cachedCompany = _client.GetCompanyFromCache(keys);
            Assert.That(cachedCompany, Is.Not.Null, "Company should be in cache after Set");
            Assert.That(cachedCompany.Id, Is.EqualTo(company.Id), "Cached company should match what we stored");

            // Wait for cache to expire
            Thread.Sleep(200); // Cache TTL is 100ms in Setup

            var expiredCompany = _client.GetCompanyFromCache(keys);
            Assert.That(expiredCompany, Is.Null, "Company should not be in cache after TTL expiration");
        }

        [Test]
        public void TwoStepCompanyLookup_CacheAndRetrieve()
        {
            var company = new Company
            {
                AccountId = "acc_123",
                EnvironmentId = "env_123",
                Id = "comp_456",
                Keys = new Dictionary<string, string> { { "org_id", "org-789" } }
            };

            PopulateTwoLayerCompanyCache(company);

            var keys = new Dictionary<string, string> { { "org_id", "org-789" } };
            var cached = _client.GetCompanyFromCache(keys);

            Assert.That(cached, Is.Not.Null, "Company should be retrievable via two-step lookup");
            Assert.That(cached!.Id, Is.EqualTo("comp_456"));
            Assert.That(cached.AccountId, Is.EqualTo("acc_123"));
        }

        [Test]
        public void TwoStepUserLookup_CacheAndRetrieve()
        {
            var user = new User
            {
                AccountId = "acc_123",
                EnvironmentId = "env_123",
                Id = "user_456",
                Keys = new Dictionary<string, string> { { "email", "test@example.com" } }
            };

            PopulateTwoLayerUserCache(user);

            var keys = new Dictionary<string, string> { { "email", "test@example.com" } };
            var cached = _client.GetUserFromCache(keys);

            Assert.That(cached, Is.Not.Null, "User should be retrievable via two-step lookup");
            Assert.That(cached!.Id, Is.EqualTo("user_456"));
            Assert.That(cached.AccountId, Is.EqualTo("acc_123"));
        }

        [Test]
        public void MultipleResourceKeys_ResolveToSameObject()
        {
            var company = new Company
            {
                AccountId = "acc_123",
                EnvironmentId = "env_123",
                Id = "comp_multi",
                Keys = new Dictionary<string, string>
                {
                    { "org_id", "org-111" },
                    { "slug", "acme-corp" },
                    { "external_id", "ext-222" }
                }
            };

            PopulateTwoLayerCompanyCache(company);

            var byOrg = _client.GetCompanyFromCache(new Dictionary<string, string> { { "org_id", "org-111" } });
            var bySlug = _client.GetCompanyFromCache(new Dictionary<string, string> { { "slug", "acme-corp" } });
            var byExternal = _client.GetCompanyFromCache(new Dictionary<string, string> { { "external_id", "ext-222" } });

            Assert.That(byOrg, Is.Not.Null);
            Assert.That(bySlug, Is.Not.Null);
            Assert.That(byExternal, Is.Not.Null);

            Assert.That(byOrg!.Id, Is.EqualTo("comp_multi"));
            Assert.That(bySlug!.Id, Is.EqualTo("comp_multi"));
            Assert.That(byExternal!.Id, Is.EqualTo("comp_multi"));

            // All resource keys resolve to the same cached object via the ID layer
            Assert.That(byOrg, Is.SameAs(bySlug));
            Assert.That(bySlug, Is.SameAs(byExternal));
        }
    }
}
