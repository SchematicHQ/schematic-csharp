using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using NUnit.Framework;
using SchematicHQ.Client.Cache;
using SchematicHQ.Client.Core;
using SchematicHQ.Client.Datastream;
using SchematicHQ.Client.Test.Datastream.Mocks;

namespace SchematicHQ.Client.Test.Datastream
{
    /// <summary>
    /// credit_postpaid must survive every path a company takes between the datastream
    /// and the WASM engine: the datastream parse, the Redis cache serializer, and the
    /// copy the metrics update makes. A null entry means off and must never reach the
    /// engine as {}, which reads as postpaid on with no limit.
    /// </summary>
    [TestFixture]
    public class CreditPostpaidTests
    {
        private const string CompanyJson = @"{
            ""id"": ""comp_pp"",
            ""account_id"": ""acc_1"",
            ""environment_id"": ""env_1"",
            ""keys"": {""id"": ""company-pp""},
            ""credit_balances"": {""credit-limited"": 0, ""credit-unlimited"": 0, ""credit-off"": 0},
            ""credit_postpaid"": {
                ""credit-limited"": {""overdraft_limit"": 100},
                ""credit-unlimited"": {},
                ""credit-off"": null
            },
            ""metrics"": [
                {
                    ""account_id"": ""acc_1"",
                    ""company_id"": ""comp_pp"",
                    ""environment_id"": ""env_1"",
                    ""event_subtype"": ""metric1"",
                    ""period"": ""all_time"",
                    ""month_reset"": ""first_of_month"",
                    ""value"": 100,
                    ""created_at"": ""2026-01-01T00:00:00Z""
                }
            ]
        }";

        private static readonly Dictionary<string, string> CompanyKeys = new() { ["id"] = "company-pp" };

        private DatastreamClient _client = null!;

        [SetUp]
        public void Setup()
        {
            _client = DatastreamClientTestFactory.CreateClientWithMocks().Client;
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
        }

        private async Task HandleFullCompanyMessage(string json)
        {
            var response = new DataStreamResponse
            {
                MessageType = MessageType.Full,
                EntityType = SchematicHQ.Client.Datastream.EntityType.Company,
                Data = JsonDocument.Parse(json).RootElement
            };
            var method = typeof(DatastreamClient).GetMethod("HandleCompanyMessage",
                BindingFlags.NonPublic | BindingFlags.Instance);
            await (Task)method!.Invoke(_client, new object[] { response })!;
        }

        private static void AssertPostpaid(RulesengineCompany? company)
        {
            Assert.That(company, Is.Not.Null);
            var postpaid = company!.CreditPostpaid;
            Assert.That(postpaid, Is.Not.Null);
            Assert.That(postpaid!.Keys, Is.EquivalentTo(new[] { "credit-limited", "credit-unlimited" }));
            Assert.That(postpaid["credit-limited"].OverdraftLimit, Is.EqualTo(100.0));
            Assert.That(postpaid["credit-unlimited"].OverdraftLimit, Is.Null);
        }

        [Test]
        public async Task FullCompanyMessage_CarriesCreditPostpaidAndDropsNullEntries()
        {
            await HandleFullCompanyMessage(CompanyJson);

            AssertPostpaid(await _client.GetCompanyFromCache(CompanyKeys));
        }

        [Test]
        public async Task RedisCacheSerializer_RoundTripsCreditPostpaid()
        {
            await HandleFullCompanyMessage(CompanyJson);
            var company = await _client.GetCompanyFromCache(CompanyKeys);

            // RedisCache.Set/Get serialize with exactly these options
            var json = JsonSerializer.Serialize(company, SchematicCacheSerializerDefaults.Options);
            var roundTripped = JsonSerializer.Deserialize<RulesengineCompany>(json, SchematicCacheSerializerDefaults.Options);

            AssertPostpaid(roundTripped);
        }

        [Test]
        public async Task MetricsUpdate_KeepsCreditPostpaid()
        {
            await HandleFullCompanyMessage(CompanyJson);
            var before = await _client.GetCompanyFromCache(CompanyKeys);

            var updated = await _client.UpdateCompanyMetricsAsync(new EventBodyTrack
            {
                Company = CompanyKeys,
                Event = "metric1",
                Quantity = 1
            });

            Assert.That(updated, Is.True);
            var after = await _client.GetCompanyFromCache(CompanyKeys);
            Assert.That(after!.Metrics.Single().Value, Is.EqualTo(101));
            AssertPostpaid(after);
            Assert.That(after.CreditPostpaid, Is.Not.SameAs(before!.CreditPostpaid));
        }

        [Test]
        public async Task WasmPayload_OmitsNullEntryAndKeepsEmptyConfig()
        {
            await HandleFullCompanyMessage(CompanyJson);
            var company = await _client.GetCompanyFromCache(CompanyKeys);

            // WasmRulesEngine.CheckFlag builds its company payload with this call
            var node = JsonUtils.SerializeToNode(company)!;
            var postpaid = node["credit_postpaid"]!.AsObject();

            Assert.That(postpaid.ContainsKey("credit-off"), Is.False);
            Assert.That(postpaid["credit-unlimited"]!.AsObject().Count, Is.EqualTo(0));
            Assert.That(postpaid["credit-limited"]!["overdraft_limit"]!.GetValue<double>(), Is.EqualTo(100.0));
        }
    }
}
