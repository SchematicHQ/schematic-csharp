using NUnit.Framework;
using RulesEngine.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SchematicHQ.Client.Test.Datastream;

public class TestDeserialization
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestDeserializeFlag()
    {
        var json = File.ReadAllText(@"/Users/chrisbrady/workspace/schematic/schematic-csharp/src/SchematicHQ.Client.Test/Datastream/flags.json");

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            // This REPLACES the existing converter with one that has the right settings
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower, false) }
        };

        var flag = System.Text.Json.JsonSerializer.Deserialize<List<Flag>>(json, options);

        Assert.That(flag != null, "Deserialized flag should not be null");
        Assert.Pass();
    }
}
