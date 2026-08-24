using System.Linq;
using System.Reflection;
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
        private const string FernPlaceholderValue = "placeholder-for-fern-compatibility";

        [Test]
        public void Resolves_Current_Fern_Schema_Version_Not_Fallback()
        {
            var version = SchemaVersionGenerator.GetGlobalSchemaVersion();

            Assert.That(version, Is.Not.Null.And.Not.Empty);
            // Not the "unexpected enum shape" fallback...
            Assert.That(version, Is.Not.EqualTo("1"));
            // ...and not the Fern placeholder member.
            Assert.That(version, Is.Not.EqualTo(FernPlaceholderValue));
            // It must be the one real (non-placeholder) value on the generated enum. Resolved
            // from the nested Values constants rather than a direct symbol reference, because
            // codegen encodes the schema hash into the member name (e.g. V5B3E7220), which
            // changes on every schema bump.
            Assert.That(version, Is.EqualTo(SingleRealGeneratedValue()));
        }

        private static string SingleRealGeneratedValue()
        {
            var values = typeof(RulesEngineSchemaVersion.Values)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(f => f.IsLiteral && f.FieldType == typeof(string))
                .Select(f => (string)f.GetRawConstantValue()!)
                .Where(v => !string.IsNullOrEmpty(v) && v != FernPlaceholderValue)
                .Distinct()
                .ToList();

            Assert.That(
                values,
                Has.Count.EqualTo(1),
                "Expected exactly one real value on the generated RulesEngineSchemaVersion enum."
            );
            return values[0];
        }
    }
}
