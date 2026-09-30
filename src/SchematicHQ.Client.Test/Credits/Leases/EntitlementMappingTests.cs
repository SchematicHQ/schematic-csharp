using System.Reflection;
using NUnit.Framework;
using SchematicHQ.Client.Leases;
using SchematicHQ.Client.RulesEngine;

namespace SchematicHQ.Client.Test.Credits.Leases;

/// <summary>
/// Two mappers turn the API's entitlement into the rules engine's: the REST flag
/// check's, and server mode's. A check has to answer with the same entitlement
/// whichever one it went through, because CheckResult tells callers to read a
/// credits counter off it and nothing in the result says which path produced it.
///
/// <para>Both are hand-written against generated types, so a field added
/// upstream is silently dropped rather than failing to compile. These walk the
/// properties by reflection instead of listing them, so the next added field
/// fails here.</para>
/// </summary>
[TestFixture]
public class EntitlementMappingTests
{
    [Test]
    public void The_Rest_Check_Carries_Every_Entitlement_Field()
    {
        AssertEverythingCrossed(CheckFlagWithEntitlementResponseMapper(Api()));
    }

    [Test]
    public void Server_Mode_Carries_Every_Entitlement_Field()
    {
        AssertEverythingCrossed(LeaseEntitlement.FromApi(Api()));
    }

    /// <summary>
    /// Every readable property on the mapped entitlement has to be populated,
    /// because the source was built with every field set to something non-default.
    /// </summary>
    private static void AssertEverythingCrossed(RulesengineFeatureEntitlement? mapped)
    {
        Assert.That(mapped, Is.Not.Null);

        var dropped = new List<string>();
        foreach (var property in typeof(RulesengineFeatureEntitlement).GetProperties(
            BindingFlags.Public | BindingFlags.Instance
        ))
        {
            // Not part of the mapping: the generated types carry it for
            // round-tripping unknown JSON.
            if (property.Name == nameof(RulesengineFeatureEntitlement.AdditionalProperties))
            {
                continue;
            }
            if (property.GetValue(mapped) == null)
            {
                dropped.Add(property.Name);
            }
        }

        Assert.That(dropped, Is.Empty, "fields the mapper dropped");
    }

    /// <summary>
    /// The REST path's mapper is private, so it is reached the way a caller
    /// does, through the response the flag check builds.
    /// </summary>
    private static RulesengineFeatureEntitlement? CheckFlagWithEntitlementResponseMapper(
        FeatureEntitlement entitlement
    ) =>
        CheckFlagWithEntitlementResponse
            .FromApiResponse(
                new CheckFlagResponseData
                {
                    Entitlement = entitlement,
                    Flag = "inference",
                    Reason = "matched",
                    Value = true,
                },
                "inference"
            )
            .Entitlement;

    /// <summary>
    /// Every field set, and none to its type's default, so a dropped one reads
    /// as null on the far side.
    /// </summary>
    private static FeatureEntitlement Api() =>
        new()
        {
            Allocation = 100,
            ConsumptionRate = 10,
            CreditId = "cred_1",
            CreditRemaining = 40,
            CreditReserved = 20,
            CreditSettled = 60,
            CreditTotal = 100,
            CreditUsed = 40,
            EventName = "inference",
            EventSubtype = "inference_tokens",
            FeatureId = "feat_1",
            FeatureKey = "inference",
            MetricPeriod = new MetricPeriod(MetricPeriod.Values.CurrentMonth),
            MetricResetAt = new DateTime(2025, 2, 1, 0, 0, 0, DateTimeKind.Utc),
            MonthReset = new MetricPeriodMonthReset(MetricPeriodMonthReset.Values.FirstOfMonth),
            SoftLimit = 200,
            Usage = 60,
            ValueType = new EntitlementValueType(EntitlementValueType.Values.Credit),
            WarningTiers = new List<WarningTier>
            {
                new() { Key = "low", Value = 10 },
            },
        };
}
