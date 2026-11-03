using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Nodes;
using NUnit.Framework;
using SchematicHQ.Client.ContextSignatures;

namespace SchematicHQ.Client.Test.ContextSignatures
{
    [TestFixture]
    public class ContextSignatureTests
    {
        private static readonly Dictionary<string, string> Company = new() { ["id"] = "comp_1" };

        // context-signing.json is copied verbatim from schematic-api's
        // api/lib/contextsig/testdata/context-signing.json, which the API generates from its
        // verifier and checks in its own tests. Don't edit it here.
        private static JsonNode LoadVectors()
        {
            var path = Path.Combine(
                TestContext.CurrentContext.TestDirectory,
                "ContextSignatures",
                "testdata",
                "context-signing.json");
            return JsonNode.Parse(File.ReadAllText(path))!;
        }

        private static IEnumerable<TestCaseData> Vectors()
        {
            foreach (var vector in LoadVectors()["vectors"]!.AsArray())
            {
                yield return new TestCaseData(vector!.ToJsonString()).SetName(vector["name"]!.GetValue<string>());
            }
        }

        private static Dictionary<string, string>? Keys(JsonNode? node)
        {
            return node?.AsObject().ToDictionary(kv => kv.Key, kv => kv.Value!.GetValue<string>());
        }

        private static (long Iat, long Exp) Window(string signature)
        {
            var parts = signature.Split('.');
            Assert.That(parts, Has.Length.EqualTo(3));
            return (long.Parse(parts[0]), long.Parse(parts[1]));
        }

        [Test]
        public void MaxLifetime_MatchesTheApi()
        {
            var max = LoadVectors()["max_lifetime_seconds"]!.GetValue<long>();
            Assert.That((long)ContextSignature.MaxLifetime.TotalSeconds, Is.EqualTo(max));
        }

        [TestCaseSource(nameof(Vectors))]
        public void SigningVector(string json)
        {
            var vector = JsonNode.Parse(json)!;
            var company = Keys(vector["context"]!["company"]);
            var user = Keys(vector["context"]!["user"]);

            Assert.That(ContextSignature.Canonicalize(company, user), Is.EqualTo(vector["canonical"]!.GetValue<string>()));
            Assert.That(
                ContextSignature.SignAt(
                    vector["secret"]!.GetValue<string>(),
                    company,
                    user,
                    vector["iat"]!.GetValue<long>(),
                    vector["exp"]!.GetValue<long>()),
                Is.EqualTo(vector["signature"]!.GetValue<string>()));
        }

        [Test]
        public void Sign_UsesTheDefaultLifetimeFromNow()
        {
            var before = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var (iat, exp) = Window(ContextSignature.Sign("secret", Company, null));

            Assert.That(iat, Is.GreaterThanOrEqualTo(before));
            Assert.That(iat, Is.LessThanOrEqualTo(DateTimeOffset.UtcNow.ToUnixTimeSeconds()));
            Assert.That(exp - iat, Is.EqualTo((long)ContextSignature.DefaultLifetime.TotalSeconds));
        }

        [Test]
        public void Sign_UsesTheTimeAndLifetimeGiven()
        {
            var signature = ContextSignature.Sign("secret", Company, null, new ContextSignatureOptions
            {
                IssuedAt = DateTimeOffset.FromUnixTimeMilliseconds(1_700_000_000_500),
                Lifetime = TimeSpan.FromMinutes(5),
            });

            Assert.That(signature, Is.EqualTo(ContextSignature.SignAt("secret", Company, null, 1_700_000_000, 1_700_000_300)));
        }

        [TestCase(0)]
        [TestCase(-60)]
        [TestCase(1.5)]
        [TestCase(3601)]
        [TestCase(86400)]
        public void Sign_RefusesALifetimeTheApiWouldReject(double seconds)
        {
            var options = new ContextSignatureOptions { Lifetime = TimeSpan.FromSeconds(seconds) };
            Assert.Throws<ContextSignatureException>(() => ContextSignature.Sign("secret", Company, null, options));
        }

        [Test]
        public void Sign_RefusesAMissingSecret()
        {
            Assert.Throws<ContextSignatureException>(() => ContextSignature.Sign("", Company, null));
        }

        [Test]
        public void Sign_RefusesANullValue()
        {
            var company = new Dictionary<string, string> { ["id"] = null! };
            Assert.Throws<ContextSignatureException>(() => ContextSignature.Sign("secret", company, null));
        }

        [Test]
        public void Canonicalize_LeavesOutANullCompanyOrUser()
        {
            Assert.That(ContextSignature.Canonicalize(null, null), Is.EqualTo("{}"));
        }

        [Test]
        public void Canonicalize_SignsALoneSurrogateAsReplacementCharacter()
        {
            Assert.That(
                ContextSignature.Canonicalize(new Dictionary<string, string> { ["name"] = "a\ud800b" }, null),
                Is.EqualTo("{\"company\":{\"name\":\"a\ufffdb\"}}"));
            Assert.That(
                ContextSignature.Canonicalize(new Dictionary<string, string> { ["name"] = "a\udc00" }, null),
                Is.EqualTo("{\"company\":{\"name\":\"a\ufffd\"}}"));
        }

        [Test]
        public void Canonicalize_KeepsAValidSurrogatePair()
        {
            Assert.That(
                ContextSignature.Canonicalize(new Dictionary<string, string> { ["name"] = "\ud83d\ude00" }, null),
                Is.EqualTo("{\"company\":{\"name\":\"\ud83d\ude00\"}}"));
        }
    }
}
