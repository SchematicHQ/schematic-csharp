using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchematicHQ.Client;

namespace SchematicHQ.Examples
{
    /// <summary>
    /// Example demonstrating how to use the C# Schematic client in replicator mode
    /// </summary>
    public class ReplicatorModeExample
    {
        public static async Task Main(string[] args)
        {
            // Configure the client with replicator mode
            var options = new ClientOptions()
                .WithReplicatorMode("http://localhost:8080/health")  // Health check URL for your replicator
                .WithLocalCache(capacity: 10000, ttl: TimeSpan.FromHours(1)); // Use local cache with 1-hour TTL

            // Initialize the Schematic client
            var schematic = new Schematic("your-api-key", options);

            try
            {
                // Check replicator health status
                if (schematic.IsReplicatorMode())
                {
                    Console.WriteLine($"Replicator mode enabled. Health status: {(schematic.IsReplicatorHealthy() ? "Healthy" : "Unhealthy")}");
                }

                // Check a feature flag - this will use the datastream client with cached data
                var company = new Dictionary<string, string> { { "id", "company-123" } };
                var user = new Dictionary<string, string> { { "id", "user-456" } };

                bool flagValue = await schematic.CheckFlag("my-feature-flag", company, user);
                Console.WriteLine($"Feature flag value: {flagValue}");

                // The client will automatically:
                // 1. Use the datastream client to evaluate flags against cached company/user data
                // 2. Monitor the replicator health via the health URL in the background
                // 3. In replicator mode, only use cached data (no WebSocket fetching)
                // 4. Evaluate flags using the rules engine with available cached data
                // 5. Log health status changes and fall back to API if replicator is unhealthy and datastream fails
            }
            finally
            {
                // Clean up resources
                await schematic.Shutdown();
            }
        }
    }
}