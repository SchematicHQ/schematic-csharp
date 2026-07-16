using NUnit.Framework;
using SchematicHQ.Client;
using SchematicHQ.Client.RulesEngine.Utils;

namespace SchematicHQ.Client.Test.RulesEngine
{
    /// <summary>
    /// Guards the reflection-based resolution of the datastream cache version from the
    /// Fern-generated <see cref="RulesEngineSchemaVersion"/> enum. If a future codegen changes
    /// the enum's shape so the single real value can't be found, this catches the regression
    /// (the resolver would silently fall back to "1").
    /// </summary>
    [TestFixture]
    public class SchemaVersionGeneratorTests
    {
        [Test]
        public void Resolves_Current_Fern_Schema_Version_Not_Fallback()
        {
            var version = SchemaVersionGenerator.GetGlobalSchemaVersion();

            Assert.That(version, Is.Not.Null.And.Not.Empty);
            // Not the "unexpected enum shape" fallback...
            Assert.That(version, Is.Not.EqualTo("1"));
            // ...and not the Fern placeholder member.
            Assert.That(version, Is.Not.EqualTo("placeholder-for-fern-compatibility"));
            // It must be one of the real (non-placeholder) values on the generated enum.
            Assert.That(version, Is.EqualTo(RulesEngineSchemaVersion.V5B3E7220.Value));
        }
    }
}
