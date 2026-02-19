using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using NUnit.Framework;
using SchematicHQ.Client.Cache;
using SchematicHQ.Client.Test.Datastream.Mocks;
using SchematicHQ.Client.Core;
using SchematicHQ.Client.Datastream;
using SchematicHQ.Client.RulesEngine.Models;

namespace SchematicHQ.Client.Test.Datastream
{
    [TestFixture]
    public class CompanyMetricsTests : IDisposable
    {
        private const string CacheKeyPrefixCompany = "company";

        // Test data
        private static readonly Dictionary<string, string> SingleCompanyKey = new Dictionary<string, string>
        {
            ["company_id"] = "12345"
        };

    private static readonly Dictionary<string, string> MultipleCompanyKeys = new Dictionary<string, string>
    {
      ["company_id"] = "12345",
      ["secondary_id"] = "secondary123"
    };

        // Client and dependencies
    private DatastreamClient _client;
    private ICacheProvider<Company> _companyCache;
    private ICacheProvider<string> _companyLookupCache;

        [SetUp]
        public void Setup()
        {
            // Create the test client and cache
            var (client, _, _, _) = DatastreamClientTestFactory.CreateClientWithMocks();
            _client = client;
            // Use reflection to get the private cache fields
            var cacheField = typeof(DatastreamClient).GetField("_companyCache", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            _companyCache = (ICacheProvider<Company>?)cacheField?.GetValue(_client) ?? throw new Exception("Could not get company cache");

            var lookupCacheField = typeof(DatastreamClient).GetField("_companyLookupCache", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            _companyLookupCache = (ICacheProvider<string>?)lookupCacheField?.GetValue(_client) ?? throw new Exception("Could not get company lookup cache");
        }

        [TearDown]
        public void TearDown()
        {
        }

        public void Dispose()
        {
            _client?.Dispose();
        }

        /// <summary>
        /// Helper to build a resource-key-based cache key using reflection
        /// </summary>
        private string ResourceKeyToCacheKey(string keyName, string keyValue)
        {
            var method = typeof(DatastreamClient).GetMethod("ResourceKeyToCacheKey",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var genericMethod = method!.MakeGenericMethod(typeof(Company));
            return (string)genericMethod.Invoke(_client, new object[] { CacheKeyPrefixCompany, keyName, keyValue })!;
        }

        /// <summary>
        /// Helper to build an ID-based cache key using reflection
        /// </summary>
        private string CompanyIdCacheKey(string id)
        {
            var method = typeof(DatastreamClient).GetMethod("CompanyIdCacheKey",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            return (string)method!.Invoke(_client, new object[] { id })!;
        }

        /// <summary>
        /// Helper method to set up a company in cache using both layers
        /// </summary>
        private void SetupCompanyInCache(string companyJson)
        {
            var company = JsonSerializer.Deserialize<Company>(companyJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (company == null) return;

            // Layer 1: Store company object at ID key
            var idKey = CompanyIdCacheKey(company.Id);
            _companyCache.Set(idKey, company);

            // Layer 2: Store company ID at each resource key
            if (company.Keys != null)
            {
                foreach (var key in company.Keys)
                {
                    var resourceKey = ResourceKeyToCacheKey(key.Key, key.Value);
                    _companyLookupCache.Set(resourceKey, company.Id);
                }
            }
        }

        /// <summary>
        /// Helper method to get a company from cache using two-step lookup
        /// </summary>
        private Company? GetCompanyFromCache(Dictionary<string, string> keys)
        {
            if (keys.Count == 0) return null;

            foreach (var key in keys)
            {
                var resourceKey = ResourceKeyToCacheKey(key.Key, key.Value);
                var companyId = _companyLookupCache.Get(resourceKey);
                if (companyId != null)
                {
                    var idKey = CompanyIdCacheKey(companyId);
                    var company = _companyCache.Get(idKey);
                    if (company != null) return company;
                }
            }
            return null;
        }

    /// <summary>
    /// Helper method to update company metrics using the Datastream client
    /// </summary>
    private bool UpdateCompanyMetrics(Dictionary<string, string> companyKeys, string metricName, string period, int quantity = 1)
    {
      // Get the company from cache
      var company = GetCompanyFromCache(companyKeys);
      if (company == null || company.Metrics == null) return false;

      // Find metrics matching the event name (metric name)
      var metricsToUpdate = company.Metrics.Where(m => m.EventSubtype == metricName).ToList();
      if (!metricsToUpdate.Any()) return false;

      // Update the metrics
      foreach (var metric in metricsToUpdate)
      {
        metric.Value += quantity;
      }

      // Save the updated company back to cache using two-layer approach
      var idKey = CompanyIdCacheKey(company.Id);
      _companyCache.Set(idKey, company);
      foreach (var key in companyKeys)
      {
        var resourceKey = ResourceKeyToCacheKey(key.Key, key.Value);
        _companyLookupCache.Set(resourceKey, company.Id);
      }

      return true;
    }

    [Test]
    public void UpdateCompanyMetrics_WithNoMatchingMetric_ReturnsFalse()
    {
        // Arrange
        var companyJson = @"{
                ""id"": ""company123"",
                ""account_id"": ""acc123"",
                ""environment_id"": ""env123"",
                ""keys"": {
                    ""company_id"": ""12345""
                },
                ""traits"": [
                    {
                        ""trait_definition"": {
                            ""id"": ""td_123"",
                            ""name"": ""name"",
                            ""comparable_type"": ""string"",
                            ""entity_type"": ""company""
                        },
                        ""value"": ""Acme Inc""
                    }
                ],
                ""metrics"": [
                    {
                        ""event_subtype"": ""metric1"",
                        ""period"": ""all_time"",
                        ""month_reset"": ""first_of_month"",
                        ""value"": 100,
                        ""created_at"": ""2023-01-01T00:00:00Z""
                    }
                ]
            }";

            SetupCompanyInCache(companyJson);

            // Act
            var result = UpdateCompanyMetrics(SingleCompanyKey, "metric2", "all_time");

            // Assert
            Assert.That(result, Is.False, "Should return false when no matching metric is found");
        }

        [Test]
        public void UpdateCompanyMetrics_WithMatchingMetric_UpdatesValueWithDefaultQuantity()
        {
            // Arrange
            var companyJson = @"{
                ""id"": ""company123"",
                ""account_id"": ""acc123"",
                ""environment_id"": ""env123"",
                ""keys"": {
                    ""company_id"": ""12345""
                },
                ""traits"": [
                    {
                        ""trait_definition"": {
                            ""id"": ""td_123"",
                            ""name"": ""name"",
                            ""comparable_type"": ""string"",
                            ""entity_type"": ""company""
                        },
                        ""value"": ""Acme Inc""
                    }
                ],
                ""metrics"": [
                    {
                        ""event_subtype"": ""metric1"",
                        ""period"": ""all_time"",
                        ""month_reset"": ""first_of_month"",
                        ""value"": 100,
                        ""created_at"": ""2023-01-01T00:00:00Z""
                    }
                ]
            }";

            SetupCompanyInCache(companyJson);

            // Act
            var result = UpdateCompanyMetrics(SingleCompanyKey, "metric1", "all_time");

            // Assert
            Assert.That(result, Is.True, "Should return true when metric is updated");
            var company = GetCompanyFromCache(SingleCompanyKey);
            Assert.That(company, Is.Not.Null);

            var metric = company?.Metrics?.FirstOrDefault(m => m.EventSubtype == "metric1" && m.Period.Value == "all_time");
            Assert.That(metric, Is.Not.Null);
            Assert.That(metric?.Value, Is.EqualTo(101)); // Default quantity is 1
        }

        [Test]
        public void UpdateCompanyMetrics_WithSpecificQuantity_UpdatesMetricValue()
        {
            // Arrange
            var companyJson = @"{
                ""id"": ""company123"",
                ""account_id"": ""acc123"",
                ""environment_id"": ""env123"",
                ""keys"": {
                    ""company_id"": ""12345""
                },
                ""traits"": [
                    {
                        ""trait_definition"": {
                            ""id"": ""td_123"",
                            ""name"": ""name"",
                            ""comparable_type"": ""string"",
                            ""entity_type"": ""company""
                        },
                        ""value"": ""Acme Inc""
                    }
                ],
                ""metrics"": [
                    {
                        ""event_subtype"": ""metric1"",
                        ""period"": ""all_time"",
                        ""month_reset"": ""first_of_month"",
                        ""value"": 100,
                        ""created_at"": ""2023-01-01T00:00:00Z""
                    }
                ]
            }";

            SetupCompanyInCache(companyJson);

            // Act
            var result = UpdateCompanyMetrics(SingleCompanyKey, "metric1", "all_time", 5);

            // Assert
            Assert.That(result, Is.True, "Should return true when metric is updated");
            var company = GetCompanyFromCache(SingleCompanyKey);
            Assert.That(company, Is.Not.Null);

            var metric = company?.Metrics?.FirstOrDefault(m => m.EventSubtype == "metric1" && m.Period.Value == "all_time");
            Assert.That(metric, Is.Not.Null);
            Assert.That(metric?.Value, Is.EqualTo(105)); // 100 + 5
        }

        [Test]
        public void UpdateCompanyMetrics_WithMultipleKeys_UpdatesAllCacheEntries()
        {
            // Arrange
            var companyJson = @"{
                ""id"": ""company123"",
                ""account_id"": ""acc123"",
                ""environment_id"": ""env123"",
                ""keys"": {
                    ""company_id"": ""12345"",
                    ""secondary_id"": ""secondary123""
                },
                ""traits"": [
                    {
                        ""trait_definition"": {
                            ""id"": ""td_123"",
                            ""name"": ""name"",
                            ""comparable_type"": ""string"",
                            ""entity_type"": ""company""
                        },
                        ""value"": ""Acme Inc""
                    }
                ],
                ""metrics"": [
                    {
                        ""event_subtype"": ""metric1"",
                        ""period"": ""all_time"",
                        ""month_reset"": ""first_of_month"",
                        ""value"": 100,
                        ""created_at"": ""2023-01-01T00:00:00Z""
                    }
                ]
            }";

            SetupCompanyInCache(companyJson);

            // Act
            var result = UpdateCompanyMetrics(MultipleCompanyKeys, "metric1", "all_time", 5);

            // Assert
            Assert.That(result, Is.True, "Should return true when metrics are updated");

            // Check company retrieved by primary key
            var companyByPrimary = GetCompanyFromCache(new Dictionary<string, string> { ["company_id"] = "12345" });
            Assert.That(companyByPrimary, Is.Not.Null);

            var metricByPrimary = companyByPrimary?.Metrics?.FirstOrDefault(m => m.EventSubtype == "metric1" && m.Period.Value == "all_time");
            Assert.That(metricByPrimary, Is.Not.Null);
            Assert.That(metricByPrimary?.Value, Is.EqualTo(105));

            // Check company retrieved by secondary key
            var companyBySecondary = GetCompanyFromCache(new Dictionary<string, string> { ["secondary_id"] = "secondary123" });
            Assert.That(companyBySecondary, Is.Not.Null);

            var metricBySecondary = companyBySecondary?.Metrics?.FirstOrDefault(m => m.EventSubtype == "metric1");
            Assert.That(metricBySecondary, Is.Not.Null);
            Assert.That(metricBySecondary?.Value, Is.EqualTo(105));
        }

        [Test]
        public void UpdateCompanyMetrics_WithMultipleMetrics_UpdatesAllMatchingMetrics()
        {
            // Arrange
            var companyJson = @"{
                ""id"": ""company123"",
                ""account_id"": ""acc123"",
                ""environment_id"": ""env123"",
                ""keys"": {
                    ""company_id"": ""12345""
                },
                ""traits"": [
                    {
                        ""trait_definition"": {
                            ""id"": ""td_123"",
                            ""name"": ""name"",
                            ""comparable_type"": ""string"",
                            ""entity_type"": ""company""
                        },
                        ""value"": ""Acme Inc""
                    }
                ],
                ""metrics"": [
                    {
                        ""event_subtype"": ""metric1"",
                        ""period"": ""all_time"",
                        ""month_reset"": ""first_of_month"",
                        ""value"": 100,
                        ""created_at"": ""2023-01-01T00:00:00Z""
                    },
                    {
                        ""event_subtype"": ""metric1"",
                        ""period"": ""current_month"",
                        ""month_reset"": ""first_of_month"",
                        ""value"": 50,
                        ""created_at"": ""2023-01-01T00:00:00Z""
                    },
                    {
                        ""event_subtype"": ""metric2"",
                        ""period"": ""all_time"",
                        ""month_reset"": ""first_of_month"",
                        ""value"": 200,
                        ""created_at"": ""2023-01-01T00:00:00Z""
                    }
                ]
            }";

            SetupCompanyInCache(companyJson);

            // Act
            var result = UpdateCompanyMetrics(SingleCompanyKey, "metric1", "current_month", 10);

            // Assert
            Assert.That(result, Is.True, "Should return true when metric is updated");
            var company = GetCompanyFromCache(SingleCompanyKey);
            Assert.That(company, Is.Not.Null);

            // All metric1 metrics should be updated regardless of period
            var metric1AllTime = company?.Metrics?.FirstOrDefault(m => m.EventSubtype == "metric1" && m.Period.Value == "all_time");
            Assert.That(metric1AllTime, Is.Not.Null);
            Assert.That(metric1AllTime?.Value, Is.EqualTo(110)); // 100 + 10
            var metric1CurrentMonth = company?.Metrics?.FirstOrDefault(m => m.EventSubtype == "metric1" && m.Period.Value == "current_month");
            Assert.That(metric1CurrentMonth, Is.Not.Null);
            Assert.That(metric1CurrentMonth?.Value, Is.EqualTo(60)); // 50 + 10
            // metric2 should not be updated
            var metric2 = company?.Metrics?.FirstOrDefault(m => m.EventSubtype == "metric2");
            Assert.That(metric2, Is.Not.Null);
            Assert.That(metric2?.Value, Is.EqualTo(200)); // Unchanged
        }
    }
}
