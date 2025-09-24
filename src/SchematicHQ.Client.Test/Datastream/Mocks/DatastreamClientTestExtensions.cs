using System.Text.Json;
using SchematicHQ.Client.Datastream;

namespace SchematicHQ.Client.Test.Datastream.Mocks
{
    /// <summary>
    /// Extension methods for testing the DatastreamClient
    /// </summary>
    public static class DatastreamClientTestExtensions
    {
        /// <summary>
        /// Injects a company directly into the DatastreamClient's cache for testing purposes
        /// </summary>
        public static void InjectCompanyIntoCache(this DatastreamClient client, string companyJson, MockWebSocket mockSocket)
        {
            // This is called by tests to directly inject a company into the test mocks
            
            // Parse the company from JSON to extract key information
            using (var jsonDoc = JsonDocument.Parse(companyJson))
            {
                var root = jsonDoc.RootElement;
                
                // Store the company JSON in the mock socket's cache for test retrieval
                var id = root.GetProperty("id").GetString() ?? throw new Exception("Company ID is required");
                
                // Add to the cache
                mockSocket.CachedCompanies[id] = companyJson;
                
                // If there are keys specified, also cache by those
                if (root.TryGetProperty("keys", out var keysElement))
                {
                    foreach (var keyProp in keysElement.EnumerateObject())
                    {
                        string keyName = keyProp.Name;
                        string? keyValue = keyProp.Value.GetString();
                        
                        if (!string.IsNullOrEmpty(keyValue))
                        {
                            mockSocket.CachedCompanies[keyValue] = companyJson;
                        }
                    }
                }
            }
        }
    }
}
