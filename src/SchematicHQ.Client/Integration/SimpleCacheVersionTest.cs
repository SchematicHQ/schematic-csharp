using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchematicHQ.Client;
using SchematicHQ.Client.Datastream;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client.Test.Integration
{
    class SimpleCacheVersionTest
    {
        static async Task Main(string[] args)
        {
            var logger = new ConsoleLogger();
            
            Console.WriteLine("Testing replicator cache version retrieval...");
            Console.WriteLine();

            // Test with replicator mode
            var datastreamOptions = new DatastreamOptions();
            var replicatorOptions = new ClientOptions
            {
                DatastreamOptions = datastreamOptions
            }.WithReplicatorMode("http://localhost:8091/ready");
            
            var replicatorSchematic = new Schematic("dummy-key", replicatorOptions);

            Console.WriteLine($"Replicator mode enabled: {replicatorSchematic.IsReplicatorMode()}");

            // Test with a mock company/user to force cache operations
            var company = new Dictionary<string, string> { { "id", "test-company" } };
            var user = new Dictionary<string, string> { { "id", "test-user" } };

            // Try multiple times to see if we can get cache version logs
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine($"\n--- Attempt {i} ---");
                try
                {
                    var result = await replicatorSchematic.CheckFlag("test-flag", company, user);
                    Console.WriteLine($"Flag result: {result}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                
                await Task.Delay(2000); // Wait 2 seconds between attempts
            }

            Console.WriteLine("\nTest completed.");
            
            // Keep the console open briefly
            await Task.Delay(1000);
        }
    }
}