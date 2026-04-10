using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.Test.Unit.MockServer;
using SchematicHQ.Client.Test.Utils;

namespace SchematicHQ.Client.Test.Unit.MockServer.Checkout;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class InternalTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async Task MockServerTest()
    {
        const string requestJson = """
            {
              "add_on_ids": [
                {
                  "add_on_id": "add_on_id",
                  "price_id": "price_id"
                }
              ],
              "company_id": "company_id",
              "credit_bundles": [
                {
                  "bundle_id": "bundle_id",
                  "quantity": 1000000
                }
              ],
              "new_plan_id": "new_plan_id",
              "new_price_id": "new_price_id",
              "pay_in_advance": [
                {
                  "price_id": "price_id",
                  "quantity": 1000000
                }
              ],
              "skip_trial": true
            }
            """;

        const string mockResponse = """
            {
              "data": {
                "application_id": "application_id",
                "cancel_at": 1000000,
                "cancel_at_period_end": true,
                "company_id": "company_id",
                "confirm_payment_intent_client_secret": "confirm_payment_intent_client_secret",
                "confirm_payment_intent_id": "confirm_payment_intent_id",
                "created_at": "2024-01-15T09:30:00.000Z",
                "currency": "currency",
                "customer_external_id": "customer_external_id",
                "default_payment_method_id": "default_payment_method_id",
                "expired_at": "2024-01-15T09:30:00.000Z",
                "id": "id",
                "interval": "interval",
                "metadata": {
                  "key": "value"
                },
                "period_end": 1000000,
                "period_start": 1000000,
                "provider_type": "orb",
                "status": "status",
                "subscription_external_id": "subscription_external_id",
                "total_price": 1000000,
                "trial_end": 1000000,
                "trial_end_setting": "cancel"
              },
              "params": {
                "key": "value"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/checkout-internal")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPost()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.Checkout.InternalAsync(
            new ChangeSubscriptionInternalRequestBody
            {
                AddOnIds = new List<UpdateAddOnRequestBody>()
                {
                    new UpdateAddOnRequestBody { AddOnId = "add_on_id", PriceId = "price_id" },
                },
                CompanyId = "company_id",
                CreditBundles = new List<UpdateCreditBundleRequestBody>()
                {
                    new UpdateCreditBundleRequestBody
                    {
                        BundleId = "bundle_id",
                        Quantity = 1000000,
                    },
                },
                NewPlanId = "new_plan_id",
                NewPriceId = "new_price_id",
                PayInAdvance = new List<UpdatePayInAdvanceRequestBody>()
                {
                    new UpdatePayInAdvanceRequestBody { PriceId = "price_id", Quantity = 1000000 },
                },
                SkipTrial = true,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
