using NUnit.Framework;
using SchematicHQ.Client.Leases;

namespace SchematicHQ.Client.Test.Credits.Leases;

/// <summary>
/// Configuration that cannot work is refused at startup. Each of these values
/// fails quietly at run time otherwise, as a sweep that never fires or a lease
/// that expires the moment it is drawn, so the client says so while there is
/// still a stack trace pointing at the setting.
/// </summary>
[TestFixture]
public class CreditLeaseConfigValidationTests
{
    [Test]
    public void A_Usable_Configuration_Validates()
    {
        Assert.DoesNotThrow(
            () =>
                new CreditLeaseConfig
                {
                    DefaultLeaseDuration = TimeSpan.FromMinutes(5),
                    DefaultReservationTTL = TimeSpan.FromMinutes(1),
                    SweepInterval = TimeSpan.FromSeconds(30),
                    DefaultLeaseSize = 1000,
                    // Both ends of the range are usable: zero tops up only on
                    // demand, one tops up after every check.
                    LowWaterMark = 0,
                }.Validate()
        );
        Assert.DoesNotThrow(() => new CreditLeaseConfig { LowWaterMark = 1 }.Validate());
        // Everything unset is the all-defaults case, which has to stay valid.
        Assert.DoesNotThrow(() => new CreditLeaseConfig().Validate());
    }

    [Test]
    public void A_Non_Positive_Sweep_Interval_Is_Refused()
    {
        Rejects(new CreditLeaseConfig { SweepInterval = TimeSpan.Zero });
        Rejects(new CreditLeaseConfig { SweepInterval = TimeSpan.FromSeconds(-1) });
    }

    [Test]
    public void A_Non_Positive_Lease_Duration_Or_Reservation_TTL_Is_Refused()
    {
        Rejects(new CreditLeaseConfig { DefaultLeaseDuration = TimeSpan.Zero });
        Rejects(new CreditLeaseConfig { DefaultLeaseDuration = TimeSpan.FromMinutes(-1) });
        Rejects(new CreditLeaseConfig { DefaultReservationTTL = TimeSpan.Zero });
        Rejects(new CreditLeaseConfig { DefaultReservationTTL = TimeSpan.FromMinutes(-1) });
    }

    [Test]
    public void A_Lease_Size_That_Is_Not_A_Finite_Positive_Number_Is_Refused()
    {
        Rejects(new CreditLeaseConfig { DefaultLeaseSize = 0 });
        Rejects(new CreditLeaseConfig { DefaultLeaseSize = -1 });
        Rejects(new CreditLeaseConfig { DefaultLeaseSize = double.NaN });
        // An infinite size asks the server for an infinite grant and makes
        // every water-mark comparison meaningless.
        Rejects(new CreditLeaseConfig { DefaultLeaseSize = double.PositiveInfinity });
        Rejects(new CreditLeaseConfig { DefaultLeaseSize = double.NegativeInfinity });
        Rejects(Override(new LeaseOverride { DefaultLeaseSize = double.PositiveInfinity }));
    }

    [Test]
    public void A_Mode_Outside_The_Enum_Is_Refused()
    {
        // It compares equal to none of the arms, so it would fall through the
        // mode switch and behave as Auto rather than as what the caller meant.
        var error = Assert.Throws<ArgumentException>(
            () => new CreditLeaseConfig { Mode = (CreditLeaseMode)99 }.Validate()
        );
        Assert.That(error!.Message, Does.Contain("99"));

        foreach (CreditLeaseMode mode in Enum.GetValues(typeof(CreditLeaseMode)))
        {
            Assert.DoesNotThrow(() => new CreditLeaseConfig { Mode = mode }.Validate());
        }
    }

    [Test]
    public void A_Water_Mark_Outside_Its_Range_Is_Refused()
    {
        Rejects(new CreditLeaseConfig { LowWaterMark = -0.1 });
        Rejects(new CreditLeaseConfig { LowWaterMark = 1.5 });
        Rejects(new CreditLeaseConfig { LowWaterMark = double.NaN });
    }

    [Test]
    public void An_Override_Is_Held_To_The_Same_Rules_And_Is_Named_In_The_Message()
    {
        var config = new CreditLeaseConfig
        {
            Overrides = new Dictionary<string, LeaseOverride>
            {
                ["ct_1"] = new() { DefaultLeaseSize = -5 },
            },
        };

        var error = Assert.Throws<ArgumentException>(() => config.Validate());
        Assert.That(error!.Message, Does.Contain("ct_1"));

        Rejects(Override(new LeaseOverride { DefaultLeaseDuration = TimeSpan.Zero }));
        Rejects(Override(new LeaseOverride { DefaultReservationTTL = TimeSpan.Zero }));
        Rejects(Override(new LeaseOverride { LowWaterMark = 2 }));
        // A null override is the same as none at all.
        Assert.DoesNotThrow(() => Override(null!).Validate());
    }

    [Test]
    public void The_Client_Refuses_To_Start_On_A_Bad_Value()
    {
        Assert.Throws<ArgumentException>(
            () =>
                new Schematic(
                    "sch_test",
                    new ClientOptions
                    {
                        EventBuffer = new RecordingEventBuffer(),
                        CreditLeases = new CreditLeaseConfig { SweepInterval = TimeSpan.Zero },
                    }
                )
        );
    }

    private static CreditLeaseConfig Override(LeaseOverride over) =>
        new() { Overrides = new Dictionary<string, LeaseOverride> { ["ct_1"] = over } };

    private static void Rejects(CreditLeaseConfig config) =>
        Assert.Throws<ArgumentException>(() => config.Validate());
}
