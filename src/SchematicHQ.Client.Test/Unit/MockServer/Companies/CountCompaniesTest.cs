using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Companies;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class CountCompaniesTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string mockResponse = """
            {
              "data": {
                "count": 1
              },
              "params": {
                "credit_type_ids": [
                  "credit_type_ids"
                ],
                "has_scheduled_downgrade": true,
                "ids": [
                  "ids"
                ],
                "limit": 1000000,
                "monetized_subscriptions": true,
                "offset": 1000000,
                "plan_id": "plan_id",
                "plan_ids": [
                  "plan_ids"
                ],
                "plan_version_id": "plan_version_id",
                "q": "q",
                "sort_order_column": "sort_order_column",
                "sort_order_direction": "asc",
                "subscription_statuses": [
                  "active"
                ],
                "subscription_types": [
                  "free"
                ],
                "with_entitlement_for": "with_entitlement_for",
                "with_subscription": true,
                "without_feature_override_for": "without_feature_override_for",
                "without_plan": true,
                "without_subscription": true
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/companies/count")
                    .WithParam("plan_id", "plan_id")
                    .WithParam("plan_version_id", "plan_version_id")
                    .WithParam("q", "q")
                    .WithParam("sort_order_column", "sort_order_column")
                    .WithParam("sort_order_direction", "asc")
                    .WithParam("with_entitlement_for", "with_entitlement_for")
                    .WithParam("without_feature_override_for", "without_feature_override_for")
                    .WithParam("limit", "1000000")
                    .WithParam("offset", "1000000")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Companies.CountCompaniesAsync(
            new CountCompaniesRequest
            {
                CreditTypeIds = [new List<string>() { "credit_type_ids" }],
                HasScheduledDowngrade = true,
                Ids = [new List<string>() { "ids" }],
                MonetizedSubscriptions = true,
                PlanId = "plan_id",
                PlanIds = [new List<string>() { "plan_ids" }],
                PlanVersionId = "plan_version_id",
                Q = "q",
                SortOrderColumn = "sort_order_column",
                SortOrderDirection = SortDirection.Asc,
                SubscriptionStatuses =
                [
                    new List<SubscriptionStatus>() { SubscriptionStatus.Active },
                ],
                SubscriptionTypes = [new List<SubscriptionType>() { SubscriptionType.Free }],
                WithEntitlementFor = "with_entitlement_for",
                WithoutFeatureOverrideFor = "without_feature_override_for",
                WithoutPlan = true,
                WithoutSubscription = true,
                WithSubscription = true,
                Limit = 1000000,
                Offset = 1000000,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
