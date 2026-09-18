using NUnit.Framework;
using SchematicHQ.Client.Leases;

namespace SchematicHQ.Client.Test.Credits.Leases;

/// <summary>
/// The bounds a caller-supplied quantity has to clear before anything rounds it
/// onto an integer.
/// </summary>
[TestFixture]
public class LeaseQuantityBoundsTests
{
    [Test]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(0.5)]
    [TestCase(LeaseQuantity.MaxQuantity)]
    public void An_Ordinary_Quantity_Is_Accepted(double value) =>
        Assert.That(LeaseQuantity.IsValid(value), Is.True);

    [Test]
    [TestCase(-1)]
    [TestCase(double.NaN)]
    [TestCase(double.PositiveInfinity)]
    [TestCase(double.MaxValue)]
    [TestCase(1e300)]
    public void A_Quantity_That_Cannot_Round_Onto_An_Integer_Is_Refused(double value) =>
        Assert.That(LeaseQuantity.IsValid(value), Is.False);

    [Test]
    public void The_Largest_Accepted_Quantity_Still_Rounds_To_Itself()
    {
        Assert.That(
            ReservationTrack.SettleQuantity(LeaseQuantity.MaxQuantity),
            Is.EqualTo(9007199254740991)
        );
        Assert.That(
            LeasePreflight.PreflightQuantity(LeaseQuantity.MaxQuantity),
            Is.EqualTo(9007199254740991)
        );

        // What the bound is there to stop. This runtime saturates the cast; on
        // the .NET Framework target it wraps to a negative number instead.
        // Either way an unbounded quantity bills nonsense rather than being
        // refused, which is why the check happens before the cast.
        Assert.That((long)Math.Ceiling(double.MaxValue), Is.EqualTo(long.MaxValue));
    }

    [Test]
    public void An_Out_Of_Range_Usage_Builds_No_Preflight()
    {
        Assert.That(LeasePreflight.Build(new CheckOptions { Usage = double.MaxValue }), Is.Null);
    }
}
