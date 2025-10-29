using SchematicHQ.Client.Datastream;
using Schematic.RulesEngine.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SchematicHQ.Client.Test.Datastream.Mocks
{
    /// <summary>
    /// Test extension for the DatastreamClient that provides test-specific functionality
    /// </summary>
    public static class DatastreamClientTestHelpers
    {
        /// <summary>
        /// Provides a way to get a company from a MockWebSocket's cache - only for testing!
        /// </summary>
        public static Company? GetCompanyFromTestCache(MockWebSocket mockSocket, Dictionary<string, string> keys)
        {
            foreach (var key in keys)
            {
                if (mockSocket.CachedCompanies.TryGetValue(key.Value, out var companyJson))
                {
                    // Parse and return the company
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower, false) }
                    };
                    
                    return JsonSerializer.Deserialize<Company>(companyJson, options);
                }
            }
            
            return null;
        }
    }
}
