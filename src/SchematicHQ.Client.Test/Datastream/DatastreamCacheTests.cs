using System.Text.Json;
using System.Text.Json.Serialization;
using NUnit.Framework;
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

        private void PopulateTwoLayerCompanyCache(RulesengineCompany company)
        {
            var method = typeof(DatastreamClient).GetMethod("CacheCompanyForKeys",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            method!.Invoke(_client, new object[] { company });
        }

        private void PopulateTwoLayerUserCache(RulesengineUser user)
        {
            var method = typeof(DatastreamClient).GetMethod("CacheUserForKeys",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            method!.Invoke(_client, new object[] { user });
        }

        [Test]
        public async Task ExpiredCache_RequestsResourcesAgain()
        {
            var companyKey = "company-123";

            var company = new RulesengineCompany
            {
                AccountId = "acc_123",
                EnvironmentId = "env_123",
                Id = "comp_123",
                Keys = new Dictionary<string, string> { { "id", companyKey } }
            };

            PopulateTwoLayerCompanyCache(company);

            var keys = new Dictionary<string, string> { { "id", companyKey } };
            var cachedCompany = await _client.GetCompanyFromCache(keys);
            Assert.That(cachedCompany, Is.Not.Null, "Company should be in cache after Set");
            Assert.That(cachedCompany.Id, Is.EqualTo(company.Id), "Cached company should match what we stored");

            // Wait for cache to expire
            Thread.Sleep(200); // Cache TTL is 100ms in Setup

            var expiredCompany = await _client.GetCompanyFromCache(keys);
            Assert.That(expiredCompany, Is.Null, "Company should not be in cache after TTL expiration");
        }

        [Test]
        public async Task TwoStepCompanyLookup_CacheAndRetrieve()
        {
            var company = new RulesengineCompany
            {
                AccountId = "acc_123",
                EnvironmentId = "env_123",
                Id = "comp_456",
                Keys = new Dictionary<string, string> { { "org_id", "org-789" } }
            };

            PopulateTwoLayerCompanyCache(company);

            var keys = new Dictionary<string, string> { { "org_id", "org-789" } };
            var cached = await _client.GetCompanyFromCache(keys);

            Assert.That(cached, Is.Not.Null, "Company should be retrievable via two-step lookup");
            Assert.That(cached!.Id, Is.EqualTo("comp_456"));
            Assert.That(cached.AccountId, Is.EqualTo("acc_123"));
        }

        [Test]
        public async Task TwoStepUserLookup_CacheAndRetrieve()
        {
            var user = new RulesengineUser
            {
                AccountId = "acc_123",
                EnvironmentId = "env_123",
                Id = "user_456",
                Keys = new Dictionary<string, string> { { "email", "test@example.com" } }
            };

            PopulateTwoLayerUserCache(user);

            var keys = new Dictionary<string, string> { { "email", "test@example.com" } };
            var cached = await _client.GetUserFromCache(keys);

            Assert.That(cached, Is.Not.Null, "User should be retrievable via two-step lookup");
            Assert.That(cached!.Id, Is.EqualTo("user_456"));
            Assert.That(cached.AccountId, Is.EqualTo("acc_123"));
        }

        [Test]
        public async Task MultipleResourceKeys_ResolveToSameObject()
        {
            var company = new RulesengineCompany
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

            var byOrg = await _client.GetCompanyFromCache(new Dictionary<string, string> { { "org_id", "org-111" } });
            var bySlug = await _client.GetCompanyFromCache(new Dictionary<string, string> { { "slug", "acme-corp" } });
            var byExternal = await _client.GetCompanyFromCache(new Dictionary<string, string> { { "external_id", "ext-222" } });

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

        [Test]
        public async Task DeleteMessage_RemovesCompanyFromCache()
        {
            // Arrange - First populate the cache with a company
            var company = new RulesengineCompany
            {
                AccountId = "acc_123",
                EnvironmentId = "env_123",
                Id = "comp_del_1",
                Keys = new Dictionary<string, string> { { "org_id", "org-to-delete" } }
            };

            PopulateTwoLayerCompanyCache(company);

            // Verify company is in cache before deletion
            var keys = new Dictionary<string, string> { { "org_id", "org-to-delete" } };
            var cachedBefore = await _client.GetCompanyFromCache(keys);
            Assert.That(cachedBefore, Is.Not.Null, "Company should be in cache before delete message");
            Assert.That(cachedBefore!.Id, Is.EqualTo("comp_del_1"));

            // Arrange - Construct a delete message for this company
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower, false) }
            };

            var deleteResponse = new DataStreamResponse
            {
                MessageType = MessageType.Delete,
                EntityType = SchematicHQ.Client.Datastream.EntityType.Company,
                Data = JsonDocument.Parse(JsonSerializer.Serialize(company, jsonOptions)).RootElement
            };

            _mockWebSocket.SetupToReceive(JsonSerializer.Serialize(deleteResponse));

            // Act - Start the client to process the delete message
            _client.Start();

            // Give time for the async message processing to complete
            await Task.Delay(200);

            // Assert - Company should be removed from cache
            var cachedAfter = await _client.GetCompanyFromCache(keys);
            Assert.That(cachedAfter, Is.Null, "Company should be removed from cache after delete message");
        }

        [Test]
        public async Task DeleteMessage_RemovesUserFromCache()
        {
            // Arrange - First populate the cache with a user
            var user = new RulesengineUser
            {
                AccountId = "acc_123",
                EnvironmentId = "env_123",
                Id = "user_del_1",
                Keys = new Dictionary<string, string> { { "email", "delete-me@example.com" } }
            };

            PopulateTwoLayerUserCache(user);

            // Verify user is in cache before deletion
            var keys = new Dictionary<string, string> { { "email", "delete-me@example.com" } };
            var cachedBefore = await _client.GetUserFromCache(keys);
            Assert.That(cachedBefore, Is.Not.Null, "User should be in cache before delete message");
            Assert.That(cachedBefore!.Id, Is.EqualTo("user_del_1"));

            // Arrange - Construct a delete message for this user
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower, false) }
            };

            var deleteResponse = new DataStreamResponse
            {
                MessageType = MessageType.Delete,
                EntityType = SchematicHQ.Client.Datastream.EntityType.User,
                Data = JsonDocument.Parse(JsonSerializer.Serialize(user, jsonOptions)).RootElement
            };

            _mockWebSocket.SetupToReceive(JsonSerializer.Serialize(deleteResponse));

            // Act - Start the client to process the delete message
            _client.Start();

            // Give time for the async message processing to complete
            await Task.Delay(200);

            // Assert - User should be removed from cache
            var cachedAfter = await _client.GetUserFromCache(keys);
            Assert.That(cachedAfter, Is.Null, "User should be removed from cache after delete message");
        }

        // NOTE: Partial entity message merging test is intentionally omitted.
        // The source code in HandleCompanyMessage and HandleUserMessage does NOT perform
        // any merging for MessageType.Partial messages. Both Full and Partial messages
        // are handled identically: the entity is deserialized and cached, replacing any
        // existing cache entry entirely. There is no field-level merge logic in the
        // current implementation, so there is no distinct behavior to test for Partial
        // messages beyond what the Full message tests already cover.

        [Test]
        public async Task CompanyUpdate_RemovedKey_OrphanedLookupEntryIsDeleted()
        {
            var original = new RulesengineCompany
            {
                AccountId = "acc_123",
                EnvironmentId = "env_123",
                Id = "comp_orphan_1",
                Keys = new Dictionary<string, string>
                {
                    { "org_id", "org-100" },
                    { "email", "old@example.com" }
                }
            };

            PopulateTwoLayerCompanyCache(original);

            // Verify both keys resolve
            Assert.That(await _client.GetCompanyFromCache(new Dictionary<string, string> { { "org_id", "org-100" } }), Is.Not.Null);
            Assert.That(await _client.GetCompanyFromCache(new Dictionary<string, string> { { "email", "old@example.com" } }), Is.Not.Null);

            // Update with "email" key removed
            var updated = new RulesengineCompany
            {
                AccountId = "acc_123",
                EnvironmentId = "env_123",
                Id = "comp_orphan_1",
                Keys = new Dictionary<string, string>
                {
                    { "org_id", "org-100" }
                }
            };

            PopulateTwoLayerCompanyCache(updated);

            // org_id should still resolve
            var cached = await _client.GetCompanyFromCache(new Dictionary<string, string> { { "org_id", "org-100" } });
            Assert.That(cached, Is.Not.Null);
            Assert.That(cached!.Id, Is.EqualTo("comp_orphan_1"));

            // email key should no longer resolve
            var orphaned = await _client.GetCompanyFromCache(new Dictionary<string, string> { { "email", "old@example.com" } });
            Assert.That(orphaned, Is.Null, "Removed company key should not remain in lookup cache");
        }

        [Test]
        public async Task UserUpdate_RemovedKey_OrphanedLookupEntryIsDeleted()
        {
            var original = new RulesengineUser
            {
                AccountId = "acc_123",
                EnvironmentId = "env_123",
                Id = "user_orphan_1",
                Keys = new Dictionary<string, string>
                {
                    { "user_id", "u-100" },
                    { "email", "old@example.com" }
                }
            };

            PopulateTwoLayerUserCache(original);

            // Verify both keys resolve
            Assert.That(await _client.GetUserFromCache(new Dictionary<string, string> { { "user_id", "u-100" } }), Is.Not.Null);
            Assert.That(await _client.GetUserFromCache(new Dictionary<string, string> { { "email", "old@example.com" } }), Is.Not.Null);

            // Update with "email" key removed
            var updated = new RulesengineUser
            {
                AccountId = "acc_123",
                EnvironmentId = "env_123",
                Id = "user_orphan_1",
                Keys = new Dictionary<string, string>
                {
                    { "user_id", "u-100" }
                }
            };

            PopulateTwoLayerUserCache(updated);

            // user_id should still resolve
            var cached = await _client.GetUserFromCache(new Dictionary<string, string> { { "user_id", "u-100" } });
            Assert.That(cached, Is.Not.Null);
            Assert.That(cached!.Id, Is.EqualTo("user_orphan_1"));

            // email key should no longer resolve
            var orphaned = await _client.GetUserFromCache(new Dictionary<string, string> { { "email", "old@example.com" } });
            Assert.That(orphaned, Is.Null, "Removed user key should not remain in lookup cache");
        }

        [Test]
        public async Task CompanyUpdate_ChangedKeyValue_OldValueIsRemoved()
        {
            var original = new RulesengineCompany
            {
                AccountId = "acc_123",
                EnvironmentId = "env_123",
                Id = "comp_changed_1",
                Keys = new Dictionary<string, string>
                {
                    { "org_id", "org-100" },
                    { "email", "old@example.com" }
                }
            };

            PopulateTwoLayerCompanyCache(original);

            // Update with changed email value
            var updated = new RulesengineCompany
            {
                AccountId = "acc_123",
                EnvironmentId = "env_123",
                Id = "comp_changed_1",
                Keys = new Dictionary<string, string>
                {
                    { "org_id", "org-100" },
                    { "email", "new@example.com" }
                }
            };

            PopulateTwoLayerCompanyCache(updated);

            // New email should resolve
            var cached = await _client.GetCompanyFromCache(new Dictionary<string, string> { { "email", "new@example.com" } });
            Assert.That(cached, Is.Not.Null);
            Assert.That(cached!.Id, Is.EqualTo("comp_changed_1"));

            // Old email should no longer resolve
            var stale = await _client.GetCompanyFromCache(new Dictionary<string, string> { { "email", "old@example.com" } });
            Assert.That(stale, Is.Null, "Old key value should not remain in lookup cache after value change");
        }

        [Test]
        public async Task UserUpdate_ChangedKeyValue_OldValueIsRemoved()
        {
            var original = new RulesengineUser
            {
                AccountId = "acc_123",
                EnvironmentId = "env_123",
                Id = "user_changed_1",
                Keys = new Dictionary<string, string>
                {
                    { "user_id", "u-100" },
                    { "email", "old@example.com" }
                }
            };

            PopulateTwoLayerUserCache(original);

            // Update with changed email value
            var updated = new RulesengineUser
            {
                AccountId = "acc_123",
                EnvironmentId = "env_123",
                Id = "user_changed_1",
                Keys = new Dictionary<string, string>
                {
                    { "user_id", "u-100" },
                    { "email", "new@example.com" }
                }
            };

            PopulateTwoLayerUserCache(updated);

            // New email should resolve
            var cached = await _client.GetUserFromCache(new Dictionary<string, string> { { "email", "new@example.com" } });
            Assert.That(cached, Is.Not.Null);
            Assert.That(cached!.Id, Is.EqualTo("user_changed_1"));

            // Old email should no longer resolve
            var stale = await _client.GetUserFromCache(new Dictionary<string, string> { { "email", "old@example.com" } });
            Assert.That(stale, Is.Null, "Old key value should not remain in lookup cache after value change");
        }

        [Test]
        public async Task CompanyUpdate_UnchangedKeys_StillResolvable()
        {
            var original = new RulesengineCompany
            {
                AccountId = "acc_123",
                EnvironmentId = "env_123",
                Id = "comp_stable_1",
                Keys = new Dictionary<string, string>
                {
                    { "org_id", "org-100" },
                    { "slug", "acme" }
                }
            };

            PopulateTwoLayerCompanyCache(original);

            // Update with same keys but different non-key data
            var updated = new RulesengineCompany
            {
                AccountId = "acc_456",
                EnvironmentId = "env_123",
                Id = "comp_stable_1",
                Keys = new Dictionary<string, string>
                {
                    { "org_id", "org-100" },
                    { "slug", "acme" }
                }
            };

            PopulateTwoLayerCompanyCache(updated);

            // Both keys should still resolve to the updated entity
            var byOrg = await _client.GetCompanyFromCache(new Dictionary<string, string> { { "org_id", "org-100" } });
            var bySlug = await _client.GetCompanyFromCache(new Dictionary<string, string> { { "slug", "acme" } });

            Assert.That(byOrg, Is.Not.Null);
            Assert.That(bySlug, Is.Not.Null);
            Assert.That(byOrg!.AccountId, Is.EqualTo("acc_456"), "Should reflect updated entity data");
            Assert.That(bySlug!.AccountId, Is.EqualTo("acc_456"), "Should reflect updated entity data");
        }
    }
}
