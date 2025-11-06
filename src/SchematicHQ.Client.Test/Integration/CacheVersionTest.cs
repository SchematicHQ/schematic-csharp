using System;
using System.Threading.Tasks;
using SchematicHQ.Client;
using SchematicHQ.Client.Datastream;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client.Test.Integration
{
    class CacheVersionTest
    {
        static async Task Main(string[] args)
        {
            var logger = new ConsoleLogger();
            
            Console.WriteLine("Testing replicator cache version integration...");
            Console.WriteLine();

            // Test 1: Without replicator mode (should use SDK version)
            Console.WriteLine("=== Test 1: SDK Mode ===");
            var normalOptions = new ClientOptions();
            var normalSchematic = new Schematic("dummy-key", normalOptions);
            
            try
            {
                var result1 = await normalSchematic.CheckFlag("test-flag");
                Console.WriteLine($"Flag result: {result1}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Expected error in SDK mode: {ex.Message}");
            }

            Console.WriteLine();

            // Test 2: With replicator mode (should attempt to use replicator version)
            Console.WriteLine("=== Test 2: Replicator Mode ===");
            var datastreamOptions = new DatastreamOptions();
            var replicatorOptions = new ClientOptions
            {
                DatastreamOptions = datastreamOptions
            }.WithReplicatorMode("http://localhost:8091/ready");
            
            var replicatorSchematic = new Schematic("dummy-key", replicatorOptions);

            Console.WriteLine($"Replicator mode enabled: {replicatorSchematic.IsReplicatorMode()}");

            // Give it a moment to perform the initial health check
            Console.WriteLine("Waiting 3 seconds for initial health check...");
            await Task.Delay(3000);

            try
            {
                var result2 = await replicatorSchematic.CheckFlag("test-flag");
                Console.WriteLine($"Flag result: {result2}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in replicator mode: {ex.Message}");
            }

            Console.WriteLine();
            Console.WriteLine("Test completed. Check logs above to see which cache version was used.");
            
            // Keep the console open
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }

    public class ConsoleLogger : ISchematicLogger
    {
        public void Debug(string message, params object[] args)
        {
            Console.WriteLine($"[DEBUG] {string.Format(message, args)}");
        }

        public void Info(string message, params object[] args)
        {
            Console.WriteLine($"[INFO] {string.Format(message, args)}");
        }

        public void Warn(string message, params object[] args)
        {
            Console.WriteLine($"[WARN] {string.Format(message, args)}");
        }

        public void Error(string message, params object[] args)
        {
            Console.WriteLine($"[ERROR] {string.Format(message, args)}");
        }
    }
}